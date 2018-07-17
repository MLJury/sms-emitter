using SmsService.Api;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Asm = System.Reflection.Assembly;
using Config = System.Configuration.ConfigurationManager;

namespace SmsService
{
    class Service : ServiceBase
    {
        public Service()
        {
            ServiceName = "KamaSmsService";
        }

        private string title => $"______________{ServiceName}______________";
        private string GetHostAddress(string[] args)
        {
            var defaultConfig = System.Configuration.ConfigurationManager.AppSettings["SmsServiceHost"];

            Host = args.Any() ? args[0]
                : !string.IsNullOrWhiteSpace(defaultConfig) ? defaultConfig
                : "http://localhost:5015";

            return Host;
        }

        private void StartServer(string uri)
        {
            Console.WriteLine("Starting web Server...");
            WebApp.Start<Startup>(uri);
            Console.WriteLine($"service running on '{this.Host}'.\nPress Shift+F10 to stop service...");
        }

        protected override void OnStart(string[] args)
        {
            StartServer(GetHostAddress(args));
            if (Config.AppSettings["ServerType"] == "Internet")
            {
                var senderService = IOC.Activator.Container.Resolve<Core.Service.ISendService>();
                var result = senderService.LoadAsync().GetAwaiter().GetResult().Success;
                if (result)
                {
                    //var processResult = Task.Run(() => senderService.ProcessAsync());
                    //processResult.Start();
                    senderService.ProcessAsync().GetAwaiter().GetResult();
                }
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
        }

        public string Host { get; private set; }

        public void Start(string[] args)
            => OnStart(args);

        //public void Stop()
        //    => OnStop();
    }
}
