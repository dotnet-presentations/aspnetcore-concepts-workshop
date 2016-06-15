# Startup 

- Empty Web Application
- Hosting API `WebHostBuilder` builds the web application
- The `Startup` class is Entry point of the web application
- It's the new `web.config` (even tho there is still a `web.config` specifically for IIS)
- IIS hosting via AspNetCoreModule
- Separation between static content and application code via `webroot` aka `wwwroot` folder
- Configuration via `IConfiguration`
- `ConfigureServices` - for DI 
- `Configure` - for the ASP.NET pipeline
 - `IApplicationBuilder` stores the list of middleware in the pipeline
 - `app.UseXX` register pieces of middleware
- Configure logging providers
- `IHostingEnvironment` - Root of the world for your ASP.NET application

- .NET Core
.NET Framework

# Middleware

- A `module/handler` replacement
- Linear pipeline with ordered components
- Before/After semantics
- Russion doll model
- Dependency injection aware
  - Per request scoped services
- Modify the request/response
- Can Add/remove feature interfaces on a per request basis


#Configuration

## Configuration API

- Multiple configuration sources
 - Command line
 - Environment variables
 - In memory
 - Ini files
 - JSON files
 - XML
 - Custom sources
- Strongly typed using `ConfigurationBinder`
- Configuration change notification
- Accessing configuration in application code
- The project template `appsettings.json`
- Configuring connection strings
- Configuration in Azure Websites
- Storing secrets on developer machines
 - User secrets
 
## Options API
- `IOptions<T>`
- Option composition via `IConfigureOptions<T>`
- `IServiceCollection.Configure<T>`
- Accessing `IOptions<T>` in application code
- Options used in middleware

# Logging & Diagnostics


## Types of errors
- Startup errors
 - IIS Module misconfigured
 - IIS Module can't start ASP.NET Core app
 - ASP.NET Core app starts up and fails
- Per request errors

## Logging
- Logging providers
 - Console
 - Debug
 - EventLog
 - TraceSource
 - 3rd party
    - NLog
    - Serilog
- Default framework logging
 - Hosting
 - Entity framework
 - MVC
- Filtering logs

## Diagnostics
 - Exception handler
 - Developer Exception Page
  - Developer exceptions
 - Status code pages
 
 # Other
 - Stdout with the ASP.NET Core module
 - Event Log with ASP.NET Core module
 - Diagnostics in Azure
 

# MVC API
- Start with ASP.NET Core project and add MVC.Core
- Show basic attribute route
- Add JSON support
- Add Product model
- Add data using the EF in memory store
- Camel case properties
- Write CRUD ProductsController
    - GetAll(), Get(id), Post(product), Delete(id)
    - Handle unknown products (404)
    - Model validation for Post
    - Rich model validation with errors flowing to the client
- Add Xml support
- Produces/Consumes
- Custom input and output formatter (custom format) 
