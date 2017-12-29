using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using UnityEngineAnalyzer.CMD.Utilities;

namespace UnityEngineAnalyzer.CMD.Core
{
    public class UnityProjectAnalyzer : IUnityProjectAnalyzer
    {
        ILog _log;
        IDirectoryUtility _directoryUtility;
        IFileUtility _fileUtility;
        ICSProjAnalyzer _csprojAnalyzer;
        UnityProjectInfoCollector _projectInfoCollector;

        public UnityProjectAnalyzer(ILog log,
                                    IDirectoryUtility directoryUtility, 
                                    IFileUtility fileUtility,
                                    ICSProjAnalyzer csprojAnalyzer,
                                    UnityProjectInfoCollector projectInfoCollector)
        {
            if (log == null)
            {
                throw new System.ArgumentNullException("log");
            }
            else if (directoryUtility == null)
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
            _log = log;
            _directoryUtility = directoryUtility;
            _fileUtility = fileUtility;
            _csprojAnalyzer = csprojAnalyzer;
            _projectInfoCollector = projectInfoCollector;
        }

        /// <inheritdoc />
        ImmutableArray<SimpleDiagnostic> IUnityProjectAnalyzer.Analyze(Options options)
        {
            if (options == null)
            {
                throw new System.ArgumentNullException("options");
            }

            if (!_directoryUtility.Exists(options.ProjectDirectoryPath))
            {
                throw new System.InvalidOperationException("Directory does not exist! " + options.ProjectDirectoryPath);
            }

            var projectInfo = _projectInfoCollector.Collect(options.ProjectDirectoryPath);
            _log.Info("Analyzing Unity Project:" + projectInfo);
            var waitObjects = new List<Task<ImmutableArray<SimpleDiagnostic>>>();
            foreach (var projectFilePath in projectInfo.CSProjFilePaths)
            {
                waitObjects.Add(_csprojAnalyzer.LoadAndAnalyzeAsync(projectFilePath));
            }
            Task.WaitAll(waitObjects.ToArray());

            var listBuilder = ImmutableArray.CreateBuilder<SimpleDiagnostic>();
            foreach (var waitObject in waitObjects)
            {
                var results = waitObject.Result;
                listBuilder.AddRange(waitObject.Result);
            }
            listBuilder.Sort((a, b) =>
            {
                var severity = a.Severity.CompareTo(b.Severity);
                if(severity == 0)
                {
                    return a.Id.CompareTo(b.Id);
                }
                return severity;
            });
            return listBuilder.ToImmutable();
        }
    }
}
