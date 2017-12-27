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
    }
}
