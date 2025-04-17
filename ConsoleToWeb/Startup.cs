using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Runtime.Intrinsics.X86;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting.Server;
using System.Net;
using Microsoft.VisualBasic;
using System.Buffers.Text;
using System.IO;

namespace ConsoleToWeb
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<CustomMiddleware>();

            //services.AddSingleton(new Service1());
            //services.AddSingleton(new Service2());

            //The service instances aren't created by the service container.
            //The framework doesn't dispose of the services automatically.
            //The developer is responsible for disposing the services.
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    await context.Response.WriteAsync("Getting Response from 1st Middleware \n");
            //});

            //custom middleware integration
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Use Middleware Incoming Request \n");
            //    await next();
            //    await context.Response.WriteAsync("Use Middleware Outgoing Response \n");
            //});
            //app.UseMiddleware<CustomMiddleware>();
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Response from Run Middleware");
            //});



            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Use Middleware1 Incoming Request \n");
            //    await next();
            //    await context.Response.WriteAsync("Use Middleware1 Outgoing Response \n");
            //});
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Use Middleware2 Incoming Request \n");
            //    await next();
            //    await context.Response.WriteAsync("Use Middleware2 Outgoing Response \n");
            //});
            //app.Run(async context => {
            //    await context.Response.WriteAsync("Run Middleware3 Request Handled and Response Generated\n");
            //});
            //this is to understand flow of middlewares



            //If you want to insert some specific middleware logic for some specific URL,
            //then you can do the same using the Map Extension Method
            //The Map method Branches the request pipeline based on matches of the given request path.
            //If the request path starts with the given path, the branch is executed else the Middleware simply ignored.
            //the map extension method Returns: The Microsoft.AspNetCore.Builder.IApplicationBuilder instance.
            //app.Map("/testmap", MapCustomMiddleware);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello From ASP.NET Core Web API");
                });
                endpoints.MapGet("/Resource1", async context =>
                {
                    await context.Response.WriteAsync("Hello From ASP.NET Core Web API Resource1");
                });
                //Maps simple GET endpoints(no controller)
                //Great for small, quick APIs or test responses

                endpoints.MapControllers();
                //Maps controller endpoints(via[Route], [HttpGet], etc.)
                //Used when working with full Web APIs using Controllers

            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Response from Run Middleware");
            });


            //app.Run() is a terminal middleware
            //When a request comes in, and this app.Run(...) is reached,
            //ASP.NET Core executes it and stops.
            //No other middleware(like routing or controllers) will be executed after it.
            //You typically only use app.Run(...):
            //At the very end of your pipeline
            //As a fallback or final handler
            //Think of it as a fallback handler — like a default: case in a switch statement.
        }
        private void MapCustomMiddleware(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Specific URL Logic Middleware using Map Method \n");
                await next();
            });
        }
    }
}


//The ConfigureService method is used to configure all the services that you want to use in this application.
//The ConfigureService method takes IServiceCollection as an input parameter.
//The IServiceCollection interface belongs to Microsoft.Extensions.DependencyInjection namespace.

//The Configure method is used to configure the HTTP request processing pipeline of the application.
//In other words, we can say that it will configure all the middleware that you want to use in your application.
//The Configure method takes two parameters i.e. IApplicationBuilder and IWebHostEnvironment instance.
//The IApplicationBuilder interface belongs to Microsoft.AspNetCore.Builder namespace and
//IWebHostEnvironment interface belong to Microsoft.AspNetCore.Hosting namespace.
//The IApplicationBuilder Defines a class that provides the mechanisms to configure an application’s request pipeline.
//On the other hand, the IWebHostEnvironment Provides information about the web hosting environment an application is running in.

//By calling app.UseRouting() method we are just enabling the routing for our application
//So, we need to tell the mapping between a URL and a resource.
//And we can do the same by using the UseEndpoints middleware.
//Inside the UseEndpoints method, we need to call the MapGet method.
//And inside the MapGet method, we need to pass the URL

//The Controller class in ASP.NET Core Web API must have a “Controller” suffix.
//The Controller class must be inherited from the ControllerBase class.
//The Controller class that we used in ASP.NET Core MVC has support Views, ViewBag, ViewData, TempData, etc.
//But here in ASP.NET Core Web API, we don’t require such concepts.
//So, we skip controller class and use the ControllerBase class.
//The point that you need to remember is the ControllerBase class is also serves as the base class for the Controller class.
//That means behind the scene the Controller class is inherited from the ControllerBase class.
//So, if you are using the Controller class in your ASP.NET Core MVC application,
//it means you are also using ControllerBase class. But in Web API, we simply need to use the ControllerBase class.
//If server-side rendering of views is needed, use the Controller class instead of the ControllerBase class.
//The Controller class is inherited from the ControllerBase class.
//No View Support in controllerBase class

//Basically, the mapping between the URL and resource (controller action method) is nothing but the concept of Routing. 
//Routing in ASP.NET Core is a concept that is used to map the incoming HTTP requests to specific controller actions in our application.
//When a request comes in, the Routing Engine inspects the URL (and possibly other data like HTTP method, Route Data, etc.)
//and determines which action method of which controller should handle it. 

//When the server receives the request, the URL is parsed into its components to determine the resource being requested by the client.
//This will be done using the Routing Middleware component. For example, the URL https://example.com/api/customers/5 is broken down into:
//Scheme: https(protocol used).
//Host: example.com(the server or domain).
//Path: / api / customers / 5(identifies the resource or endpoint).
//Query String: Additional parameters if provided (e.g., ?search=abc).

//The routing engine in ASP.NET Core examines the parsed URL and tries to match it against the application’s route table.
//A Route Table is a collection of route patterns defined in the application via the attribute or conventional routing.
//Each route pattern specifies:
//Controller name
//Action method name
//HTTP method(s)
//Route parameters
//Defaults and constraints

//ASP.NET Core supports two primary routing strategies:
//Convention - Based Routing: Configured globally, typically in the Program.cs file.
//Attribute - Based Routing: Configured using attributes applied directly to controllers and action methods.