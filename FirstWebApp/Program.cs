
using FirstWebApp.Repositories;
using System.Reflection;
using System.Runtime.ConstrainedExecution;

namespace FirstWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create a new builder for the web application using the command-line arguments provided.
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the dependency injection container.
            // Here, MVC controllers are being added, which handle web API requests.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            // Register the necessary services to enable API exploration capabilities
            // which Swashbuckle utilizes to generate Swagger documentation
            builder.Services.AddEndpointsApiExplorer();


            // Register the EmployeeRepository for Dependency Injection
            builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            //It means Whenever someone asks for an IEmployeeRepository, give them an instance of EmployeeRepository
            //AddSingleton → One instance for the whole app
            //AddScoped → One instance per request
            //AddTransient → New instance every time it’s needed


            // Add Swagger generator services to the services container.
            // This will be used to produce the Swagger document (OpenAPI spec) and the Swagger UI
            builder.Services.AddSwaggerGen();
            //this is customized swaggerGen
            //builder.Services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "My Custom API",
            //        Version = "v1",
            //        Description = "A Brief Description of My APIs",
            //        TermsOfService = new Uri("https://dotnettutorials.net/privacy-policy/"),
            //        Contact = new OpenApiContact
            //        {
            //            Name = "Support",
            //            Email = "support@dotnettutorials.net",
            //            Url = new Uri("https://dotnettutorials.net/contact/")
            //        },
            //        License = new OpenApiLicense
            //        {
            //            Name = "Use Under XYZ",
            //            Url = new Uri("https://dotnettutorials.net/about-us/")
            //        }
            //    });
            //});

            // Build the web application using the configurations defined above.
            var app = builder.Build();

            // Configure the HTTP request pipeline
            // Register Swagger middleware components if in development environment
            if (app.Environment.IsDevelopment())
            {
                // UseSwagger middleware to serve the generated Swagger as a JSON endpoint.
                app.UseSwagger();
                // Swagger UI fetches the Swagger JSON to generate a visual documentation of the API.
                app.UseSwaggerUI();
            }

            // Middleware to redirect HTTP requests to HTTPS.
            app.UseHttpsRedirection();

            // Middleware to enforce authorization policies on requests.
            app.UseAuthorization();

            // Map controller routes. This makes the application aware of the routes defined in the controllers.
            // This is attribute based routing
            app.MapControllers();

            //this is conventional based routing
            //app.MapControllerRoute(name: "default", pattern:"api/{controller}/{action}/{id?}");

            // Run the application and start listening for incoming HTTP requests.
            app.Run();
        }
    }
}


//Swagger UI:	Interactive web interface to explore and test your API
//SwaggerGen:	Auto-generates Swagger docs from your code
//Swashbuckle.AspNetCore:	NuGet package that brings Swagger into ASP.NET Core

//Passing resource identifiers as part of the Route Data is recommended.
//Resource identifier means the value which is going to identify a resource.
//Other optional values should be passed using the Query string parameters.