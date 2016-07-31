using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace AttendeeList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseKestrel()
                            .UseIISIntegration()
                            .UseStartup<Startup>()
                            .Build();

            host.Run();
        }
    }
}
