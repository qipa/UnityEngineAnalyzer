namespace UnityEngineAnalyzer.CMD.Utilities
{
    public interface IFileUtility
    {
        /// <summary>
        /// Returns if the file exists or not
        /// </summary>
        /// <param name="path">The full path to the file</param>
        /// <returns>If the file exists or not</returns>
        bool Exists(string path);
    }
}
