using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;
using UnityEngineAnalyzer.CMD.Core;
using UnityEngineAnalyzer.CMD.Installers;
using UnityEngineAnalyzer.CMD.Reporting;
using UnityEngineAnalyzer.CMD.Utilities;
using Zenject;

namespace UnityEngineAnalyzer.CMD
{
    class Program
    {
        public static bool Regexp { get; private set; }

        static void Main(string[] args)
        {
            var container = new DiContainer();
            UtilitiesInstaller.Install(container);
            CoreInstaller.Install(container);
            ReportingInstaller.Install(container);
            var log = container.Resolve<ILog>();
            var directoryUtility = container.Resolve<IDirectoryUtility>();
            var fileUtility = container.Resolve<IFileUtility>();

            var mockOptions = new Options()
            {
                ProjectDirectoryPath = "MYDIRECTORY",
                ExcludePathPatterns = new string[] {
                    @"[\""\'\\/]\b(ThirdParty)\b[\""\'\\/]",
                    @"[\""\'\\/]\b(2DxFX)\b[\""\'\\/]",
                }
            };

            var analyzer = container.Resolve<IUnityProjectAnalyzer>();
            var analyzerResults = analyzer.Analyze(mockOptions);

            //Log short info to console
            foreach (var result in analyzerResults)
            {
                bool foundExcludeMatch = false;
                foreach(var excludePattern in mockOptions.ExcludePathPatterns)
                {
                    if (Regex.Match(result.FilePath, excludePattern).Success)
                    {
                        foundExcludeMatch = true;
                        break;
                    }
                }

                if (!foundExcludeMatch)
                {
                    string resultLine = "(" + result.Id + ") " + System.IO.Path.GetFileName(result.FilePath) + ":" + result.LineNumber + ". " + result.Message;
                    switch (result.Severity)
                    {
                        case SimpleDiagnostic.SimpleSeverity.Error:
                            log.Error(resultLine);
                            break;
                        case SimpleDiagnostic.SimpleSeverity.Warning:
                            log.Warning(resultLine);
                            break;
                        case SimpleDiagnostic.SimpleSeverity.Info:
                        case SimpleDiagnostic.SimpleSeverity.Hidden:
                        default:
                            log.Info(resultLine);
                            break;
                    }
                }
            }

            var exportDirectoryPath = mockOptions.ProjectDirectoryPath + "\\report";
            if(!directoryUtility.Exists(exportDirectoryPath))
            {
                directoryUtility.Create(exportDirectoryPath);
            }
            var projectFolderName = new System.IO.DirectoryInfo(mockOptions.ProjectDirectoryPath).Name;

            var exporters = container.ResolveAll<IAnalyzerReporter>();
            foreach(var exporter in exporters)
            {
                var pathToExport = exportDirectoryPath + "\\" + projectFolderName + "_report." + exporter.DefaultFileEnding;
                fileUtility.WriteAllBytes(pathToExport, exporter.BuildReportData(analyzerResults));
                log.Info("Exported report to " + pathToExport);
            }
        }
    }
}
