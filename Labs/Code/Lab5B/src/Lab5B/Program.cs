using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Lab5B
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:8081")
                .UseStartup<Startup>();
    }
}
