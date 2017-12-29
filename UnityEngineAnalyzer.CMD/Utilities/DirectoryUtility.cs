using System.IO;
using System.Collections.Generic;

namespace UnityEngineAnalyzer.CMD.Utilities
{
    public class DirectoryUtility : IDirectoryUtility
    {
        /// <inheritdoc />
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        /// <inheritdoc />
        public string[] GetFiles(string path, string searchPattern = "*")
        {
            if(!Exists(path))
            {
                throw new System.InvalidOperationException("Cannot get files from: " + path + ". Directory does not exist!");
            }
            return Directory.GetFiles(path, searchPattern);
        }
    }
}
