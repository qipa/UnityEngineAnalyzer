using System.IO;

namespace UnityEngineAnalyzer.CMD.Utilities
{
    public class FileUtility : IFileUtility
    {
        /// <inheritdoc />
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}
