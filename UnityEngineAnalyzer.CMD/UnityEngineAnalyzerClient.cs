using UnityEngineAnalyzer.CMD.Core;
using UnityEngineAnalyzer.CMD.Reporting;
using UnityEngineAnalyzer.CMD.Utilities;
using Zenject;

namespace UnityEngineAnalyzer.CMD
{
    public class UnityEngineAnalyzerClient
    {
        ILog _log;
        SimpleDiagnosticFilterer _filterer;
        IUnityProjectAnalyzer _analyzer;
        IDirectoryUtility _directoryUtility;
        IFileUtility _fileUtility;
        DiContainer _container;

        public UnityEngineAnalyzerClient(ILog log, 
                                         SimpleDiagnosticFilterer filterer,
                                         IUnityProjectAnalyzer analyzer,
                                         IDirectoryUtility directoryUtility,
                                         IFileUtility fileUtility,
                                         DiContainer container)
        {
            if (log == null)
            {
                throw new System.ArgumentNullException("log");
            }
            else if (filterer == null)
            {
                throw new System.ArgumentNullException("filterer");
            }
            else if (analyzer == null)
            {
                throw new System.ArgumentNullException("analyzer");
            }
            else if (directoryUtility == null)
            {
                throw new System.ArgumentNullException("directoryUtility");
            }
            else if (fileUtility == null)
            {
                throw new System.ArgumentNullException("fileUtility");
            }
            else if (container == null)
            {
                throw new System.ArgumentNullException("container");
            }

            _log = log;
            _filterer = filterer;
            _analyzer = analyzer;
            _directoryUtility = directoryUtility;
            _fileUtility = fileUtility;
            _container = container;
        }

        public void AnalyzeProject(Options options)
        {
            var filteredResults = _filterer.GetFilteredList(_analyzer.Analyze(options), options.ExcludePathPatterns);

            //Log short info to console
            foreach (var result in filteredResults)
            {
                string resultLine = "(" + result.Id + ") " + System.IO.Path.GetFileName(result.FilePath) + ":" + result.LineNumber + ". " + result.Message;
                switch (result.Severity)
                {
                    case SimpleDiagnostic.SimpleSeverity.Error:
                        _log.Error(resultLine);
                        break;
                    case SimpleDiagnostic.SimpleSeverity.Warning:
                        _log.Warning(resultLine);
                        break;
                    case SimpleDiagnostic.SimpleSeverity.Info:
                    case SimpleDiagnostic.SimpleSeverity.Hidden:
                    default:
                        _log.Info(resultLine);
                        break;
                }
            }

            var exportDirectoryPath = options.ProjectDirectoryPath + "\\report";
            if (!_directoryUtility.Exists(exportDirectoryPath))
            {
                _directoryUtility.Create(exportDirectoryPath);
            }

            var exporters = _container.ResolveAll<IAnalyzerReporter>();
            foreach (var exporter in exporters)
            {
                var pathToExport = exportDirectoryPath + "\\" + options.ProjectName + "_report." + exporter.DefaultFileEnding;
                _fileUtility.WriteAllBytes(pathToExport, exporter.BuildReportData(filteredResults, options));
                _log.Info("Exported report to " + pathToExport);
            }
        }
    }
}
