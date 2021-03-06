﻿using System;
using System.Linq;
using System.ServiceProcess;
using SmsService.Core.Model;
using Microsoft.Owin.Hosting;
using SmsService.Api;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace SmsService
{
    class Program
    {
        const string title = "_________ SmsService _______________";
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

