using UnityEngineAnalyzer.CMD.Core;
using Zenject;

namespace UnityEngineAnalyzer.CMD.Installers
{
    public class CoreInstaller : Installer<CoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UnityProjectInfoCollector>().AsSingle();
            Container.Bind<IUnityProjectAnalyzer>().To<UnityProjectAnalyzer>().AsSingle();
        }
    }
}
