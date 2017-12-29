using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using UnityEngineAnalyzer.CMD.Utilities;

namespace UnityEngineAnalyzer.CMD.Core
{
    public class UnityProjectAnalyzer : IUnityProjectAnalyzer
    {
        IDirectoryUtility _directoryUtility;
        IFileUtility _fileUtility;
        ICSProjAnalyzer _csprojAnalyzer;
        UnityProjectInfoCollector _projectInfoCollector;

        public UnityProjectAnalyzer(IDirectoryUtility directoryUtility, 
                                    IFileUtility fileUtility,
                                    ICSProjAnalyzer csprojAnalyzer,
                                    UnityProjectInfoCollector projectInfoCollector)
        {
            if (directoryUtility == null)
            {
                throw new System.ArgumentNullException("directoryUtility");
            }
            else if (fileUtility == null)
            {
                throw new System.ArgumentNullException("fileUtility");
            }
            else if (csprojAnalyzer == null)
            {
                throw new System.ArgumentNullException("csprojAnalyzer");
            }
            else if (projectInfoCollector == null)
            {
                throw new System.ArgumentNullException("projectInfoCollector");
            }
            _directoryUtility = directoryUtility;
            _fileUtility = fileUtility;
            _csprojAnalyzer = csprojAnalyzer;
            _projectInfoCollector = projectInfoCollector;
        }

        /// <inheritdoc />
        public void Analyze(Options options)
        {
            if (options == null)
            {
                throw new System.ArgumentNullException("options");
            }
            
            if(!_directoryUtility.Exists(options.ProjectDirectoryPath))
            {
                throw new System.InvalidOperationException("Directory does not exist! " + options.ProjectDirectoryPath);
            }

            var projectInfo = _projectInfoCollector.Collect(options.ProjectDirectoryPath);
            System.Console.WriteLine("Analyzing Unity Project:" + projectInfo);
            var waitObjects = new List<Task<ImmutableArray<SimpleDiagnostic>>>();
            foreach(var projectFilePath in projectInfo.CSProjFilePaths)
            {
                waitObjects.Add(_csprojAnalyzer.LoadAndAnalyzeAsync(projectFilePath));
            }
            Task.WaitAll(waitObjects.ToArray());

            foreach(var waitObject in waitObjects)
            {
                var results = waitObject.Result;
                foreach(var result in results)
                {
                    System.Console.WriteLine(result);
                }
            }
            System.Console.WriteLine("ANALYZE DONE!");
        }
    }
}
