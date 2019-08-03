using AnyJob.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass]
    public class DefaultLogService : ILogService
    {
        public void Debug(string message, params object[] args)
        {
            WriteLog(ConsoleColor.Gray, message, args);
        }

        public void Error(string message, params object[] args)
        {
            WriteLog(ConsoleColor.Red, message, args);
        }

        public void Info(string message, params object[] args)
        {
            WriteLog(ConsoleColor.Cyan, message, args);
        }

        public void Warn(string message, params object[] args)
        {
            WriteLog(ConsoleColor.Yellow, message, args);
        }

        private void WriteLog(ConsoleColor color, string message, params object[] args)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = color;
            string line = string.Format(message, args);
            Console.WriteLine("{0:HH:mm:ss.fff} [-] {1}", DateTime.Now, line);
            Console.ForegroundColor = current;
        }
    }
}
