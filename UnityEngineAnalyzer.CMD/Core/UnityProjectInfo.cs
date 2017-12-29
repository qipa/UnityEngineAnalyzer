namespace UnityEngineAnalyzer.CMD.Core
{
    /// <summary>
    /// Data class containing information about a unity project
    /// </summary>
    public class UnityProjectInfo
    {
        public string ProjectDirectoryPath { get; set; }
        public string[] CSProjFilePaths { get; set; }
        public UnityVersion? UnityVersion { get; set; }

        public override string ToString()
        {
            return string.Format("[ProjectDirectoryPath:{0} CSProjFilesPathsCount:{1} UnityVersion:{2}]", ProjectDirectoryPath, (CSProjFilePaths != null ? CSProjFilePaths.Length : 0), UnityVersion);
        }
    }
}
