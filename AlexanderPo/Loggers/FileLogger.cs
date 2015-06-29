using System;
using System.IO;
using System.Threading.Tasks;
using AlexanderPo.Configuration;
using Any.Logs;

namespace AlexanderPo.Loggers
{
    public class FileLogger : LoggerBase
    {
        private static readonly object _fileLoker = new object();

        public FileLogger()
        {
            if (!Directory.Exists(AppConfig.LogRoot))
            {
                Directory.CreateDirectory(AppConfig.LogRoot);
            }
        }

        public Task WriteAsync(string message)
        {
            return Task.Run(() => AddToFile(message));
        }

        private static void AddToFile(string message)
        {
            lock (_fileLoker)
            {
                File.AppendAllText(Path.Combine(AppConfig.LogRoot, String.Format("log-{0}.txt", DateTime.Now.ToString("yyyy.MM.dd"))),
                    String.Format("{0}{1}{0}==================================================={0}", Environment.NewLine, message));
            }
        }

    }
}