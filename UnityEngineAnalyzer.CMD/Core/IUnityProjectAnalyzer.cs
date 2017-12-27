using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngineAnalyzer.CMD.Core
{
    public interface IUnityProjectAnalyzer
    {
        void Analyze(Options options);
    }
}
