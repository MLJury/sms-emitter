using System;
using System.Linq;
using System.ServiceProcess;
using Kama.SmsService.Core.Model;
using Microsoft.Owin.Hosting;
using Kama.SmsService.Api;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace Kama.SmsService
{
    class Program
    {
        const string title = "_________ Kama SmsService _______________";
        static void Main(string[] args)
        {
            try
            {
                var service = new Service();
                if (Environment.UserInteractive)
                {
                    Console.WriteLine(title);
                    Console.WriteLine("Starting service, Please wait...");
                    service.Start(args);
                    Console.ReadKey();
                    service.Stop();
                }
                else
                    ServiceBase.Run(service);
            }
            catch (Exception e)
            {
                Console.Write($"-----------------------\n {e.Message}");
                Console.ReadKey();
            }
        }
    }
}

