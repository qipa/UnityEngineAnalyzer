namespace UnityEngineAnalyzer.CMD.Utilities
{
    public interface IDirectoryUtility
    {
        /// <summary>
        /// Returns if the directory exists or not
        /// </summary>
        /// <param name="path">The full path to the directory</param>
        /// <returns>If the directory exists or not</returns>
        bool Exists(string path);

        /// <summary>
        /// Returns all the files in a directory 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern">(Optional) The regex match pattern in which files to include. Defaults to "*"</param>
        /// <returns>The files found in the directory. Throws an exception if the directory does not exist</returns>
        string[] GetFiles(string path, string searchPattern = "*");
    }
}
