namespace UMLToEFConverter.Common
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