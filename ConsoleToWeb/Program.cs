using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace ConsoleToWeb
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            CreateHostBuilder(args).Build().Run();

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

//Think of a Host Builder as the starting point of any .NET Core app — like the "main switch" that sets up:
//App configuration(e.g.appsettings.json)
//Logging
//Dependency injection (services)
//Web server (for APIs or websites)
//It builds a "Host", which is the container that runs your app.
//The Host Builder is your "Setup Manager" — it prepares everything before you actually open the server
//It just means: “🎛️ I want a professional environment to run my app.”


//Dependency Injection is a design pattern used to give (inject) required things (dependencies)
//into a class instead of the class creating them itself.
//Imagine you are baking a cake (your class 🧁), and you need flour, eggs, and sugar (your dependencies).
//Instead of growing wheat, buying hens, and making sugar, someone else gives them to you.
//➡️ This is Dependency Injection — the ingredients (dependencies) are provided to you.

//Kestrel is the web server that runs inside your ASP.NET Core application.
//It's the thing that listens to HTTP requests and sends back responses.
//Imagine you're building a restaurant (your web app).
//You need a front door where customers (users) come in.
//👉 That front door is the Kestrel server — it accepts requests and lets them into your app.
//basically it is A lightweight web server for ASP.NET Core


//The Build Method runs the given actions to initialize the host.
//This can only be called once. It returns an initialized Microsoft.Extensions.Hosting.IHost object.

//The Run method in ASP.NET Core Application is used to complete the Middleware Execution.
//That means the Run extension method allows us to add the terminating middleware component.
//Terminating middleware means the middleware which will not call the next middleware components in the request processing pipeline.

//Middleware is a piece of code that is used in the HTTP Request Pipeline.
//Middleware examples are Routing, authentication, authorize, log, exception middleware