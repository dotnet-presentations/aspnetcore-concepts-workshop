using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

class Program
{
    static void Main(string[] args)
    {
       var host = new WebHostBuilder()
           .UseKestrel()
           .Configure(app => app.Run(context => context.Response.WriteAsync("Hello World!")))
           .Build();

       host.Run();
    }
}
