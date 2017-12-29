using UnityEngineAnalyzer.CMD.Utilities;

namespace UnityEngineAnalyzer.CMD.Core
{
    public class UnityProjectAnalyzer : IUnityProjectAnalyzer
    {
        IDirectoryUtility _directoryUtility;
        IFileUtility _fileUtility;
        UnityProjectInfoCollector _projectInfoCollector;

        public UnityProjectAnalyzer(IDirectoryUtility directoryUtility, IFileUtility fileUtility, UnityProjectInfoCollector projectInfoCollector)
        {
            if (directoryUtility == null)
            {
                throw new System.ArgumentNullException("directoryUtility");
            }
            else if (fileUtility == null)
            {
                throw new System.ArgumentNullException("fileUtility");
            }
            else if (projectInfoCollector == null)
            {
                throw new System.ArgumentNullException("projectInfoCollector");
            }
            _directoryUtility = directoryUtility;
            _fileUtility = fileUtility;
            _projectInfoCollector = projectInfoCollector;
        }

        /// <inheritdoc />
        public void Analyze(Options options)
        {
            if (options == null)
            {
                throw new System.ArgumentNullException("options");
            }
            
            if(!_directoryUtility.Exists(options.ProjectDirectoryPath))
            {
                throw new System.InvalidOperationException("Directory does not exist! " + options.ProjectDirectoryPath);
            }

            var projectInfo = _projectInfoCollector.Collect(options.ProjectDirectoryPath);
            System.Console.WriteLine("Project Information:" + projectInfo);
            foreach(var projectFilePath in projectInfo.CSProjFilePaths)
            {
                System.Console.WriteLine("CSProj File:" + projectFilePath);
            }
        }
    }
}
