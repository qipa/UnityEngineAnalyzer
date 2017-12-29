using System.Collections.Immutable;
using System.Threading.Tasks;

namespace UnityEngineAnalyzer.CMD.Core
{
    public interface ICSProjAnalyzer
    {
        Task<ImmutableArray<SimpleDiagnostic>> LoadAndAnalyzeAsync(string csProjFilePath);
    }
}
