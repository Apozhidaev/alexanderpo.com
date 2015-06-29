using System;
using System.Diagnostics;
using AlexanderPo.Configuration;
using AlexanderPo.Loggers;
using Any.Logs;
using Microsoft.Owin.Hosting;

namespace AlexanderPo
{
    public class Board
    {
        private IDisposable _webApp;

        public void Start()
        {
            Log.Initialize(new FileLogger());
            var startOptions = new StartOptions();
            foreach (var url in AppConfig.Urls)
            {
                startOptions.Urls.Add(url);
            }
            _webApp = WebApp.Start<Startup>(startOptions);
#if DEBUG
            Process.Start(@"C:\Program Files\Internet Explorer\iexplore.exe", AppConfig.Urls[0]);    
#endif
        }

        public void Stop()
        {
            if (_webApp != null)
            {
                _webApp.Dispose();
                _webApp = null;
            }
        }
    }
}