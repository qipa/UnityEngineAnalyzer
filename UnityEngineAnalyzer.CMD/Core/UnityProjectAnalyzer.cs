using UnityEngineAnalyzer.CMD.Utilities;

namespace UnityEngineAnalyzer.CMD.Core
{
    public class UnityProjectAnalyzer : IUnityProjectAnalyzer
    {
        IDirectoryUtility _directoryUtility;
        IFileUtility _fileUtility;

        public UnityProjectAnalyzer(IDirectoryUtility directoryUtility, IFileUtility fileUtility)
        {
            if (directoryUtility == null)
            {
                throw new System.ArgumentNullException("directoryUtility");
            }
            else if (fileUtility == null)
            {
                throw new System.ArgumentNullException("fileUtility");
            }
            _directoryUtility = directoryUtility;
            _fileUtility = fileUtility;
        }

        public void Analyze(Options options)
        {
            System.Console.WriteLine("HELLO:" + options.ProjectDirectoryPath);
        }
    }
}
