using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDotNetExtensions
{
    public static class Logger
    {
        public static event EventHandler<LogEventArgs> LogEvent;
        public static event EventHandler<LogEventArgs> ErrorEvent;
        public static event EventHandler<ExceptionEventArgs> ExceptionEvent;

        const string HEADER_SEPARATOR = " |~| ";

        public static void Log(object sender, string log)
        {
            Log(sender, sender.GetType().ToString(), log);
        }

        public static void Error(object sender, string log)
        {
            Error(sender, sender.GetType().ToString(), log);
        }

        public static void Exception(object sender, string log, Exception e)
        {
            Exception(sender, sender.GetType().ToString(), log, e);
        }

        public static void Log(IIdentified sender, string log)
        {
            if (!sender.ShouldLog()) return;

            string header = sender.GenerateName();
            Log(sender, header, log);
        }

        public static void Error(IIdentified sender, string log)
        {
            if (!sender.ShouldLogErrors()) return;

            string header = sender.GenerateName();
            Error(sender, header, log);
        }

        public static void Exception(IIdentified sender, string log, Exception e)
        {
            string header = sender.GenerateName();
            Exception(sender, header, log, e);
        }

        public static void Log(object sender, string header, string devLog)
        {
            string log = $"{header}{HEADER_SEPARATOR}{devLog}";
            Console.WriteLine(log);
            LogEvent(sender, new LogEventArgs(log));
        }

        public static void Error(object sender, string header, string devLog)
        {
            string log = $"{header}{HEADER_SEPARATOR}{devLog}";
            Console.Error.WriteLine(log);
            ErrorEvent(sender, new LogEventArgs(log));
        }

        public static void Exception(object sender, string header, string devLog, Exception e)
        {
            string log = $"{header}{HEADER_SEPARATOR}Exception thrown! Log: {devLog}\n{e}";
            Console.Error.WriteLine(log);
            ExceptionEvent(sender, new ExceptionEventArgs(log, e));
        }
    }

    public class LogEventArgs : EventArgs
    {
        public string Log { get; private set; }

        public LogEventArgs(string log) { this.Log = log; }
    }

    public class ExceptionEventArgs : LogEventArgs
    {
        public Type ExceptionType;
        public ExceptionEventArgs(string log, Exception e) : base(log)
        {
            ExceptionType = e.GetType();
        }
    }
}
