namespace UMLToMVCConverter.Common
{
    using System;
    using UMLToMVCConverter.Interfaces;

    public class Logger : ILogger
    {
        public void LogInfo(string log)
        {
            Console.WriteLine($@"INFO: {log}");
        }
    }
}