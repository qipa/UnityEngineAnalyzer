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

            var mockOptions = new Options()
            {
                ProjectDirectoryPath = "MYPROJECTDIR"
            };

            var analyzer = container.Resolve<IUnityProjectAnalyzer>();
            var analyzerResults = analyzer.Analyze(mockOptions);

            foreach (var result in analyzerResults)
            {
               Console.WriteLine(result);
            }
        }
    }
}
