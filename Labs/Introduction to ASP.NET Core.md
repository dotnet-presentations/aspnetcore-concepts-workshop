
## Create a new Console project, configure it to use the unstable packages feed, and upgrade it to RC2

1. Open Visual Studio 2015 Update 2
1. Create a new ASP.NET Core application:
  1. File -> New -> .NET Core -> ASP.NET Core Web Application (.NET Core)

## Running the application under IIS

1. The application should be setup to run under IIS by default.
1. Run the application and navigate to the root. It should show the hello world middleware.

## Running the application on Kestrel directly

1. Change the Debug drop down in the toolbar to the application name as shown below.
  
  ![image](https://cloud.githubusercontent.com/assets/95136/15806049/abf005b6-2b3a-11e6-8fb4-ca75c9f68913.png)

1. Run the application and navigate to the root. It should show the hello world middleware.
1. Change the port to `8081` by adding a call to `UseUrls` in the `Startup.cs`:

   ```
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:8081")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
            host.Run();

        }
    }
   ```
1. Navigate to the project properties (by right clicking on the project, and selection `Properties`)
1. Go to the `Debug` tab and change `Launch URL` to `http://localhost:8081`

   ![image](https://cloud.githubusercontent.com/assets/95136/15806095/157c4c32-2b3c-11e6-91db-b231aa113c31.png)

1. Run the application and navigate to the root. It should show the hello world middleware running on port 8081.

## Serving static files

1. Add the `Microsoft.AspNet.StaticFiles` package to `project.json`:

  ```JSON
  "dependencies": {
    "Microsoft.NETCore.App": {
      "version": "1.0.0-rc2-3002702",
      "type": "platform"
    },
    "Microsoft.AspNetCore.Server.IISIntegration": "1.0.0-rc2-final",
    "Microsoft.AspNetCore.Server.Kestrel": "1.0.0-rc2-final",
    "Microsoft.AspNetCore.StaticFiles": "1.0.0-rc2-final"
  },
  ```
1. Go to `Startup.cs` in the `Configure` method and add `UseStaticFiles` before the hello world middleware:

  ```C#
  public void Configure(IApplicationBuilder app)
  {
      app.UseStaticFiles();

      app.Run(async context =>
      {
          await context.Response.WriteAsync("Hello World");
      });
  }
  ```
  
1. Create a folder called `wwwroot` in the project folder.
1. Create a file called `index.html` with the following contents in the `wwwroot` folder:

  ```
  <!DOCTYPE html>
  <html>
  <head>
      <meta charset="utf-8" />
      <title></title>
  </head>
  <body>
      <h1>Hello from ASP.NET Core!</h1> 
  </body>
  </html>
  ```

1. Run the application and navigate to the root. It should show the hello world middleware.
1. Navigate to `index.html` and it should show the static page in `wwwroot`.

## Adding default document support

1. Change the static files middleware in `Startup.cs` from `app.UseStaticFiles()` to `app.UseFileServer()`.
1. Run the application. The default page `index.html` should show when navigating to the root of the site.

## Changing environments

1. The default environment in visual studio is development. In the property pages you can see this is specified by the environment variables section:

  ![image](https://cloud.githubusercontent.com/assets/95136/15806164/a57a79a2-2b3d-11e6-9551-9e106036e0c0.png)

1. Add some code to the `Configure` method in `Startup.cs` to take in `IHostingEnvironment` and print out the environment name:

  ```C#
    public void Configure(IApplicationBuilder app, IHostingEnvironment environment)
    {
        app.UseStaticFiles();

        app.Run(async (context) =>
        {
            await context.Response.WriteAsync($"Hello World! {environment.EnvironmentName}");
        });
    }
  ```
1. Run the application and it should print out `Hello World! Development`.
1. Change the application to run in the `Production` environment by changing the `ASPNETCORE_ENVIRONMENT` environment variable on the `Debug` property page:
 
  ![image](https://cloud.githubusercontent.com/assets/95136/15806196/9b52efee-2b3e-11e6-851b-35765d5b2a4d.png)

1. 1. Run the application and it should print out `Hello World! Production`.
