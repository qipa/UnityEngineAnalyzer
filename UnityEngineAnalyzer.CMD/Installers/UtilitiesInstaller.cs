using UnityEngineAnalyzer.CMD.Utilities;
using Zenject;

namespace UnityEngineAnalyzer.CMD.Installers
{
    public class UtilitiesInstaller : Installer<UtilitiesInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IFileUtility>().To<FileUtility>().AsSingle();
            Container.Bind<IDirectoryUtility>().To<DirectoryUtility>().AsSingle();
            Container.Bind<ILog>().To<SystemConsoleLog>().AsSingle();
        }
    }
}
