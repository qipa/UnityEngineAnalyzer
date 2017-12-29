using Microsoft.CodeAnalysis;

namespace UnityEngineAnalyzer.CMD.Core
{
    public class SimpleDiagnostic
    {
        public enum SimpleSeverity
        {
            Hidden = 0,
            Info = 1,
            Warning = 2,
            Error = 3
        }

        public string Id { get; set; }
        public string Message { get; set; }
        public string FilePath { get; set; }
        public int LineNumber { get; set; }
        public int CharacterPosition { get; set; }
        public SimpleSeverity Severity { get; set; }
        public UnityVersionSpan VersionSpan { get; set; }

        public override string ToString()
        {
            var versionSpan = VersionSpan.First.ToString() + " - " + VersionSpan.Last.ToString();
            return string.Format("Id:{0}, Message:{1}, FileName:{2}, LineNumber:{3}, CharacterPosition:{4}, Severity{5}, UnityVersionSpan:{6}", Id, Message, FilePath, LineNumber, CharacterPosition, Severity, versionSpan);
        }

        public static SimpleDiagnostic Convert(Diagnostic diagnostic)
        {
            var location = diagnostic.Location;
            var lineNumber = 0;
            var characterPosition = 0;
            var fileName = string.Empty;

            if (location != Location.None)
            {
                var locationSpan = location.SourceSpan;
                var lineSpan = location.SourceTree.GetLineSpan(locationSpan);
                lineNumber = lineSpan.StartLinePosition.Line;
                characterPosition = lineSpan.StartLinePosition.Character;
                fileName = location.SourceTree?.FilePath;
            }

            return new SimpleDiagnostic
            {
                Id = diagnostic.Id,
                Message = diagnostic.GetMessage(),
                FilePath = fileName,
                LineNumber = lineNumber,
                CharacterPosition = characterPosition,
                Severity = (SimpleSeverity)diagnostic.Severity,
                VersionSpan = Linty.Analyzers.DiagnosticDescriptors.GetVersion(diagnostic.Descriptor)
            };
        }
    }
}
