using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;
using UnityEngineAnalyzer.CMD.Core;
using UnityEngineAnalyzer.CMD.Installers;
using UnityEngineAnalyzer.CMD.Utilities;
using Zenject;

namespace UnityEngineAnalyzer.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new DiContainer();
            UtilitiesInstaller.Install(container);
            CoreInstaller.Install(container);
            var log = container.Resolve<ILog>();

            var mockOptions = new Options()
            {
                ProjectDirectoryPath = "MyPROJECTDIRECTORY"
            };

            var analyzer = container.Resolve<IUnityProjectAnalyzer>();
            var analyzerResults = analyzer.Analyze(mockOptions);

            //Log short info to console
            foreach (var result in analyzerResults)
            {
                string resultLine = System.IO.Path.GetFileName(result.FileName) + ":" + result.LineNumber + ". " + result.Message;
                switch(result.Severity)
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
    }
}
