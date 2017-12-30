using Zenject;

namespace UnityEngineAnalyzer.CMD.Installers
{
    public class ClientInstaller : Installer<ClientInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UnityEngineAnalyzerClient>().AsSingle();
        }
    }
}
