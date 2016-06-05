# Introduction to the .NET Core SDK

## Install the .NET Core SDK
1. Go to https://dot.net and follow the instructions to download and install the .NET Core SDK for your OS

## Create and run your first application
1. Open a command prompt
1. Make a new directory to put your application in and change to it

   ```
   mkdir MyNewApp
   cd MyNewApp
   ```
1. Create a new application by typing `dotnet new`
1. Restore the application's dependencies by typing `dotnet restore`
1. Run the application by typing `dotnet run`
1. Open the `Program.cs` file and change the greeting message
1. Run the application again using `dotnet run` and note the message about the application being re-built

## Run the application output directly
1. `dotnet run` checks the project source every time to determine if a re-build is necessary and as such is intended for active development scenarios.
1. Run the application output directly by typing `dotnet ./Debug/netcoreapp1.0/MyNewApp.dll`
