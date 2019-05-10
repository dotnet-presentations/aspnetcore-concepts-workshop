using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapGet("", context => context.Response.WriteAsync("Hello from Routing!"));
            routeBuilder.MapGet("sub", context => context.Response.WriteAsync("Hello from sub!"));
            //routeBuilder.MapGet("item/{itemName}", context => context.Response.WriteAsync($"Item: {context.GetRouteValue("itemName")}"));
            routeBuilder.MapGet("item/{id:int}", context => context.Response.WriteAsync($"Item ID: {context.GetRouteValue("id")}"));

            app.UseRouter(routeBuilder.Build());
        }
    }
}
