using Linty.Analyzers.ForEachInUpdate;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngineAnalyzer.CMD.Utilities;

namespace UnityEngineAnalyzer.CMD.Core
{
    public class CSProjAnalyzer : ICSProjAnalyzer
    {
        IFileUtility _fileUtility;
        public CSProjAnalyzer(IFileUtility fileUtility)
        {
            if(fileUtility == null)
            {
                throw new ArgumentNullException("fileUtility");
            }
            _fileUtility = fileUtility;
        }

        public async Task<ImmutableArray<SimpleDiagnostic>> LoadAndAnalyzeAsync(string csProjFilePath)
        {
            if(string.IsNullOrEmpty(csProjFilePath))
            {
                throw new ArgumentNullException("csProjFilePath");
            }
            else if(!_fileUtility.Exists(csProjFilePath))
            {
                throw new InvalidOperationException(".csproj [" + csProjFilePath + "] does not exist.");
            }

            var workspace = MSBuildWorkspace.Create();

            var project = await workspace.OpenProjectAsync(csProjFilePath, CancellationToken.None);

            var analyzers = GetAnalyzers();
            var compilation = await project.GetCompilationAsync();
            var diagnosticResults = await compilation.WithAnalyzers(analyzers).GetAnalyzerDiagnosticsAsync();

            Console.WriteLine("HEYHEY I'm Done!:" + diagnosticResults);
            foreach(var result in diagnosticResults)
            {
                Console.WriteLine(result.Descriptor.Title + ". Location:" + result.Location.ToString());
            }
            return await Task<ImmutableArray<SimpleDiagnostic>>.Run(() => ConvertDiagnosticResults(diagnosticResults));
        }


        ImmutableArray<SimpleDiagnostic> ConvertDiagnosticResults(ImmutableArray<Diagnostic> diagnosticResults)
        {
            var listBuilder = ImmutableArray.CreateBuilder<SimpleDiagnostic>();
            foreach (var result in diagnosticResults)
            {
                SimpleDiagnostic.Convert(result);
            }
            return listBuilder.ToImmutable();
        }

        //TODO:: You pass in a set of DiagnosticAnalyzers instead of searching the assembly here
        private ImmutableArray<DiagnosticAnalyzer> GetAnalyzers()
        {
            var listBuilder = ImmutableArray.CreateBuilder<DiagnosticAnalyzer>();

            var assembly = typeof(DoNotUseForEachInUpdate).Assembly;
            var allTypes = assembly.DefinedTypes;

            foreach (var type in allTypes)
            {
                if (type.IsSubclassOf(typeof(DiagnosticAnalyzer)) && !type.IsAbstract)
                {
                    var instance = Activator.CreateInstance(type) as DiagnosticAnalyzer;
                    listBuilder.Add(instance);
                }
            }

            var analyzers = listBuilder.ToImmutable();
            return analyzers;
        }
    }
}
