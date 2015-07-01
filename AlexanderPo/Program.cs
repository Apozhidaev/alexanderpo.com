using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexanderPo.Configuration;
using Topshelf;

namespace AlexanderPo
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<AnyApp>(s =>
                {
                    s.ConstructUsing(name => new AnyApp());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Site of Alexander Pozhidaev");
                x.SetDisplayName(AppConfig.DisplayName);
                x.SetServiceName(AppConfig.ServiceName);
            });
        }
    }
}
