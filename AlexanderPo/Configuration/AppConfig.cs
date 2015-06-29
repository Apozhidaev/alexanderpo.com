using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace AlexanderPo.Configuration
{
    public static class AppConfig
    {
        private static readonly string _displayName;
        private static readonly string _serviceName;
        private static readonly string[] _urls;
        private static readonly string _logRoot;
        static AppConfig()
        {
            var network = (NetworkSection)ConfigurationManager.GetSection("network");

            _displayName = "AlexanderPo";
            _serviceName = "AlexanderPo";

#if DEBUG
            _urls = new[] { AppConst.DefaultUrl };
#else
            _urls = network.Url.Split(new[] {',', ';'}, StringSplitOptions.RemoveEmptyEntries);
#endif
            _logRoot = Path.Combine("..\\..\\", AppConst.LogDirectoryName);

        }

        public static string DisplayName
        {
            get { return _displayName; }
        }

        public static string ServiceName
        {
            get { return _serviceName; }
        }

        public static string[] Urls
        {
            get { return _urls; }
        }

        public static string LogRoot
        {
            get { return _logRoot; }
        }

    }
}