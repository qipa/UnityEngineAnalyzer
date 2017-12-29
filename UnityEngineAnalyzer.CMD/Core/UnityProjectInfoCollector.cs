using UnityEngineAnalyzer.CMD.Utilities;

namespace UnityEngineAnalyzer.CMD.Core
{
    public class UnityProjectInfoCollector
    {
        IDirectoryUtility _directoryUtility;
        IFileUtility _fileUtility;

        public UnityProjectInfoCollector(IDirectoryUtility directoryUtility, IFileUtility fileUtility)
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

        public UnityProjectInfo Collect(string directoryPath)
        {
            var projInfo = new UnityProjectInfo();
            projInfo.ProjectDirectoryPath = directoryPath;
            projInfo.CSProjFilePaths = _directoryUtility.GetFiles(directoryPath, "*.csproj");
            projInfo.UnityVersion = UnityVersionResolver.Resolve(directoryPath, _fileUtility);
            return projInfo;
        }
    }
}
