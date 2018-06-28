using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Asm = System.Reflection.Assembly;

namespace Kama.SmsService
{
    class Server : IDisposable
    {
        public Server(string host)
        {
            _host = host;
        }

        readonly string _host;
        IDisposable _owinWebApp;

        private HttpConfiguration EnableWebApi()
        {
            var config = new HttpConfiguration();
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{vesion}/{controller}/{id}",
                new { vesion = "v1", id = RouteParameter.Optional });

            return config;
        }

        private void OnStart(IAppBuilder app)
        {
            var httpConfiguarion = EnableWebApi();

            app.UseWebApi(httpConfiguarion);

            IOC.Activator.Instance.ActiveWebApi(httpConfiguarion, new Asm[] { Asm.GetExecutingAssembly() });
            var apiPath = System.IO.Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
            AppCore.IOC.Loader.Load(IOC.Activator.Container, System.IO.Path.Combine(apiPath, @"bin"));
        }

        public void Start()
        {
            _owinWebApp = Microsoft.Owin.Hosting.WebApp.Start(_host, OnStart);
        }

        public void Dispose()
        {
            _owinWebApp.Dispose();
        }
    }
}
