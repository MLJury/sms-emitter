using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Host;
using System.Web.Http;
using Asm = System.Reflection.Assembly;
using Config = System.Configuration.ConfigurationManager;
using System.Linq;
using System.Threading;
using System.Web.Http.ExceptionHandling;
using Kama.Mefa.Azmoon.API.Auth.Filters;
using Kama.Mefa.Azmoon.API.Exceptions.Filters;
using Kama.Library.Queue;
using Kama.SmsService.Domain;

[assembly: OwinStartup(typeof(Kama.SmsService.Api.Startup))]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Kama.SmsService.Api.Startup), "Started")]

namespace Kama.SmsService.Api
{
    public class Startup
    {
        public string Host { get; private set; }

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

        public void Configuration(IAppBuilder app)
        {
            var httpConfiguarion = EnableWebApi();

            IOC.Activator.Instance.ActiveWebApi(httpConfiguarion, new Asm[] { Asm.GetExecutingAssembly() });
            var apiPath = System.IO.Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
            RegisterFilters(IOC.Activator.Container, httpConfiguarion);
            app.UseWebApi(httpConfiguarion);
            AppCore.IOC.Loader.Load(IOC.Activator.Container, System.IO.Path.Combine(apiPath, @"Libs"));
        }

        private static void RegisterFilters(AppCore.IOC.IContainer container, HttpConfiguration httpConfig)
        {
            //regsiter Filters
            container.RegisterType(typeof(ValidationFilter));
            container.RegisterType(typeof(KamaExceptionHandler));
            container.RegisterType(typeof(KamaExceptionLogger));


            //register command filters in httConfig
            httpConfig.Filters.Add(container.Resolve<ValidationFilter>());
            //httpConfig.Filters.Add(container.Resolve<KamaExceptionHandler>());
            //Register Exception Loggers and Exception Handler
            //httpConfig.Services.Replace(typeof(IExceptionHandler), container.Resolve<KamaExceptionHandler>());
            httpConfig.Services.Replace(typeof(IExceptionLogger), container.Resolve<KamaExceptionLogger>());
        }
    }
}
