using UnityEngineAnalyzer.CMD.Core;
using Zenject;

namespace UnityEngineAnalyzer.CMD.Installers
{
    public class CoreInstaller : Installer<CoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IUnityProjectAnalyzer>().To<UnityProjectAnalyzer>().AsSingle();
        }
    }
}
