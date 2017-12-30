using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngineAnalyzer.CMD.Core
{
    /// <summary>
    /// Data class containing options for the IUnityProjectAnalyzer
    /// </summary>
    public class Options
    {
        string _projectName = null;
        public string ProjectDirectoryPath { get; set; }
        public string[] ExcludePathPatterns { get; set; }
        public string ProjectName 
        {
            get
            {
                if(string.IsNullOrEmpty(_projectName))
                {
                    _projectName = new System.IO.DirectoryInfo(ProjectDirectoryPath).Name;
                }
                return _projectName;
            }
        }
    }
}
