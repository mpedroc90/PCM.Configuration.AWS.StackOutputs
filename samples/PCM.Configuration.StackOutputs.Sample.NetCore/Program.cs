using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PCM.Configuration.Extentions.StackOutputs;


namespace PCM.Configuration.StackOutputs.Sample.NetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hosting, config) =>
            {
                config.AddStackConfig("My_fancy_stack_name"); 
            })
            
                .UseStartup<Startup>()
                .Build();
    }
}
