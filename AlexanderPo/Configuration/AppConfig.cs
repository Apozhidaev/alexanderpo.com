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
        private static readonly string _defaultMediaType;
        private static readonly Dictionary<string, string> _mediaTypes = new Dictionary<string, string>();
        static AppConfig()
        {
            var network = (NetworkSection)ConfigurationManager.GetSection("network");

            _displayName = "AlexanderPo";
            _serviceName = "AlexanderPo";

            _urls = network.Url.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            _logRoot = Path.Combine("..\\..\\", "Logs");

            var content = (ContentSection)ConfigurationManager.GetSection("content");
            _defaultMediaType = content.DefaultMediaType;
            foreach (MediaTypeElement mediaType in content.MediaTypes)
            {
                _mediaTypes.Add(mediaType.Extension, mediaType.Value);
            }

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

        public static string GetMediaType(string extension)
        {
            if (_mediaTypes.ContainsKey(extension))
            {
                return _mediaTypes[extension];
            }
            return _defaultMediaType;
        }

    }
}