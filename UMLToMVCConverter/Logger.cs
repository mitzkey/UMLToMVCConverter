namespace UMLToMVCConverter
{
    using System;

    public class Logger : ILogger
    {
        public void LogInfo(string log)
        {
            Console.WriteLine($@"INFO: {log}");
        }
    }
}