using UnityEngineAnalyzer.CMD.Reporting;
using Zenject;

namespace UnityEngineAnalyzer.CMD.Installers
{
    public class ReportingInstaller : Installer<ReportingInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HTMLAnalyzerReporter>().AsSingle();
        }
    }
}
