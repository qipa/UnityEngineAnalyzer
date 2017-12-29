using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngineAnalyzer.CMD.Utilities;

namespace UnityEngineAnalyzer.CMD.Core
{
    public static class UnityVersionResolver
    {
        public static UnityVersion? Resolve(string projectDirectoryPath, IFileUtility fileUtility)
        {
            if(string.IsNullOrEmpty(projectDirectoryPath))
            {
                throw new System.ArgumentNullException("projectDirectoryPath");
            }
            else if(fileUtility == null)
            {
                throw new System.ArgumentNullException("fileUtility");
            }

            var projectVersionFilePath = Path.Combine(projectDirectoryPath, "ProjectSettings", "ProjectVersion.txt");

            if (!fileUtility.Exists(projectVersionFilePath))
            {
                return null;
            }

            var fileContents = fileUtility.ReadAllText(projectVersionFilePath);
            return TryParseUnityVersion(fileContents);
        }

        static UnityVersion? TryParseUnityVersion(string projectVersionFileContents)
        {
            string editorText = "m_EditorVersion: ";
            var match = Regex.Match(projectVersionFileContents, editorText + "[0-9.a-z]*");

            string src = match.Value.Substring(editorText.Length);
            src = src.Replace('.', '_');
            src = src.Substring(0, src.IndexOf('_') + 2);

            var unityVersions = Enum.GetValues(typeof(UnityVersion)).Cast<UnityVersion>();
            foreach (var unityVersion in unityVersions)
            {
                if (Enum.GetName(typeof(UnityVersion), unityVersion).Contains(src))
                {
                    return unityVersion;
                }
            }

            return null;
        }
    }
}
