﻿using System;
using System.Collections.Immutable;
using System.Text;
using UnityEngineAnalyzer.CMD.Core;
using Newtonsoft.Json;
using UnityEngineAnalyzer.CMD.Utilities;

namespace UnityEngineAnalyzer.CMD.Reporting
{
    public class HTMLAnalyzerReporter : IAnalyzerReporter
    {
        const string HTMLTemplateRelativePath = "UnityReportTemplate.html";
        const string ReplaceIdentifier = "[UNITYENGINEANALYZER_INSERTJSON_HERE]";

        [System.Serializable]
        class HTMLReportData
        {
            public string ProjectName;
            public ImmutableArray<SimpleDiagnostic> Diagnostics;
            public DateTime Date;
        }

        IFileUtility _fileUtility;
        string _htmlTemplateContents;

        public string DefaultFileEnding
        {
            get
            {
                return "html";
            }
        }

        public string HTMLTemplateContents
        {
            get
            {
                if(string.IsNullOrEmpty(_htmlTemplateContents))
                {
                    _htmlTemplateContents = _fileUtility.ReadAllText(HTMLTemplateRelativePath);
                }
                return _htmlTemplateContents;
            }
        }

        public HTMLAnalyzerReporter(IFileUtility fileUtility)
        {
            if(fileUtility == null)
            {
                throw new ArgumentNullException("fileUtility");
            }
            _fileUtility = fileUtility;
        }

        public byte[] BuildReportData(ImmutableArray<SimpleDiagnostic> diagnosticResults, Options options)
        {
            string fullhtml = HTMLTemplateContents;
            fullhtml = fullhtml.Replace(ReplaceIdentifier, BuildJSONReportData(diagnosticResults, options));
            return Encoding.UTF8.GetBytes(fullhtml);
        }

        string BuildJSONReportData(ImmutableArray<SimpleDiagnostic> diagnosticResults, Options options)
        {
            return JsonConvert.SerializeObject(new HTMLReportData()
            {
                ProjectName = options.ProjectName,
                Diagnostics = diagnosticResults,
                Date = DateTime.Now
            });
        }
    }
}