using System.Collections.Immutable;
using UnityEngineAnalyzer.CMD.Core;

namespace UnityEngineAnalyzer.CMD.Reporting
{
    public interface IAnalyzerExporter
    {
        string DefaultFileEnding { get; }
        byte[] Export(ImmutableArray<SimpleDiagnostic> diagnosticResults);
    }
}
