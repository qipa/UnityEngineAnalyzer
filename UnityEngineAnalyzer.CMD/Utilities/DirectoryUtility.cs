using System.IO;

namespace UnityEngineAnalyzer.CMD.Utilities
{
    public class DirectoryUtility : IDirectoryUtility
    {
        /// <inheritdoc />
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
