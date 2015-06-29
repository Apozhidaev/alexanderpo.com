using System;
using System.Text;
using Any.Logs;

namespace AlexanderPo.Loggers
{
    public static class LogExtensions
    {

        public static void Error(this Log log, Exception e, string summary)
        {
            var message = String.Format("{1}{0}{2}", Environment.NewLine, summary, e.GetFullMessage());
            log.WriteAsync<FileLogger>(logger => logger.WriteAsync(message));
        }

        private static string GetFullMessage(this Exception exception)
        {
            var fullMessage = new StringBuilder();
            var aggr = exception as AggregateException;
            if (aggr != null)
            {
                foreach (Exception innerException in aggr.InnerExceptions)
                {
                    fullMessage.Append(innerException.GetFullMessage());
                }
            }
            else
            {
                while (exception != null)
                {
                    fullMessage.Append(String.Format("{2}---{0}---{2}{1}{2}", exception.Message, exception.StackTrace,
                        Environment.NewLine));
                    exception = exception.InnerException;
                }
            }
            return fullMessage.ToString();
        }
    }
}