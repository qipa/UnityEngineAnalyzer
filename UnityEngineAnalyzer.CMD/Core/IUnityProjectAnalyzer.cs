using System.Collections.Immutable;

namespace UnityEngineAnalyzer.CMD.Core
{
    public interface IUnityProjectAnalyzer
    {
        ImmutableArray<SimpleDiagnostic> Analyze(Options options);
    }
}
