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

        /// <summary>
        /// Reads the full content of a file at a given path.
        /// </summary>
        /// <param name="path">The full path to the file</param>
        /// <returns>The text content of the file</returns>
        string ReadAllText(string path);

        /// <summary>
        /// Writes a byte array to a given file path
        /// </summary>
        /// <param name="path">The path to write the file to</param>
        /// <param name="contents">the contents of the file</param>
        void WriteAllBytes(string path, byte[] contents);
    }
}
