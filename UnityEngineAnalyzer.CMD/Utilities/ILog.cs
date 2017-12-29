namespace UnityEngineAnalyzer.CMD.Utilities
{
    public interface ILog
    {
        void Info(string log);
        void Warning(string warning);
        void Error(string error);
    }
}
