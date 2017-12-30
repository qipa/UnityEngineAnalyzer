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
        static void Main(string[] args)
        {
            var container = new DiContainer();
            UtilitiesInstaller.Install(container);
            CoreInstaller.Install(container);
            ReportingInstaller.Install(container);
            ClientInstaller.Install(container);

            //TODO:: Create a launch options that can be converted into options
            var options = new Options()
            {
                ProjectDirectoryPath = "MYPROJECTDIRECTORY",
                ExcludePathPatterns = new string[] {
                    @"[\""\'\\/]\b(ThirdParty)\b[\""\'\\/]",
                    @"[\""\'\\/]\b(2DxFX)\b[\""\'\\/]",
                }
            };

            var client = container.Resolve<UnityEngineAnalyzerClient>();
            client.AnalyzeProject(options);
        }
    }
}
