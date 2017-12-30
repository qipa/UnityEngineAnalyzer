using System.Collections.Immutable;
using UnityEngineAnalyzer.CMD.Core;

namespace UnityEngineAnalyzer.CMD.Reporting
{
    public interface IAnalyzerReporter
    {
        string DefaultFileEnding { get; }
        byte[] BuildReportData(ImmutableArray<SimpleDiagnostic> diagnosticResults, Options options);
    }
}
