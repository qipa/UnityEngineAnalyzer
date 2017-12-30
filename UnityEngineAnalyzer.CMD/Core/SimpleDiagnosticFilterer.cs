using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UnityEngineAnalyzer.CMD.Core
{
    public class SimpleDiagnosticFilterer
    {
        public ImmutableArray<SimpleDiagnostic> GetFilteredList(ImmutableArray<SimpleDiagnostic> diagnostics, string[] excludePathPatterns)
        {
            var listBuilder = ImmutableArray.CreateBuilder<SimpleDiagnostic>();
            foreach (var diagnostic in diagnostics)
            {
                bool foundExcludeMatch = false;
                foreach (var excludePattern in excludePathPatterns)
                {
                    if (Regex.Match(diagnostic.FilePath, excludePattern).Success)
                    {
                        foundExcludeMatch = true;
                        break;
                    }
                }

                if (!foundExcludeMatch) 
                {
                    listBuilder.Add(diagnostic);
                }
            }
            return listBuilder.ToImmutable();
        }
    }
}
