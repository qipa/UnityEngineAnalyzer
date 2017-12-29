using System;

namespace UnityEngineAnalyzer.CMD.Utilities
{
    public class SystemConsoleLog : ILog
    {
        public void Error(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        public void Info(string log)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(log);
            Console.ResetColor();
        }

        public void Warning(string warning)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(warning);
            Console.ResetColor();
        }
    }
}
