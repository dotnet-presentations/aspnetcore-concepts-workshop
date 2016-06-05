
## Create a new Console project, configure it to use the unstable packages feed, and upgrade it to RC2

1. Open Visual Studio 2015 Update 2
1. Create a new ASP.NET Core application:
  1. File -> New -> .NET Core -> ASP.NET Core Web Application (.NET Core)

## Serving static files

1. Add the `Microsoft.AspNet.StaticFiles` package to `project.json`:

  ```JSON
  "dependencies": {
    "Microsoft.AspNet.Server.Kestrel": "1.0.0-*",
    "Microsoft.AspNet.StaticFiles": "1.0.0-*"
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
      <h1>Hello from ASP.NET 5!</h1> 
  </body>
  </html>
  ```

1. Run the application and navigate to the root. It should show the hello world middleware.
1. Navigate to `index.html` and it should show the static page in `wwwroot`.

## Adding default document support

1. Change the static files middleware in `Startup.cs` from `app.UseStaticFiles()` to `app.UseFileServer()`.
2. Run the application. The default page `index.html` should show when navigating to the root of the site.
