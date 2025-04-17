namespace ConsoleToWeb
{
    public class DependencyInjection
    {
        public void DI()
        {
            var services = new ServiceCollection();

            // Registering the service with different lifetimes:
            // Uncomment only ONE at a time to see the difference

            // services.AddTransient<IMyService, MyService>(); // Always new
            services.AddScoped<IMyService, MyService>();      // New per scope
            // services.AddSingleton<IMyService, MyService>(); // Single forever

            var serviceProvider = services.BuildServiceProvider();

            Console.WriteLine("------ Scope 1 ------");
            using (var scope1 = serviceProvider.CreateScope())
            {
                var service1a = scope1.ServiceProvider.GetService<IMyService>();
                var service1b = scope1.ServiceProvider.GetService<IMyService>();
                Console.WriteLine($"service1a ID: {service1a.GetOperationID()}");
                Console.WriteLine($"service1b ID: {service1b.GetOperationID()}");
            }

            Console.WriteLine("------ Scope 2 ------");
            using (var scope2 = serviceProvider.CreateScope())
            {
                var service2a = scope2.ServiceProvider.GetService<IMyService>();
                var service2b = scope2.ServiceProvider.GetService<IMyService>();
                Console.WriteLine($"service2a ID: {service2a.GetOperationID()}");
                Console.WriteLine($"service2b ID: {service2b.GetOperationID()}");
            }
        }
    }
}


//for addtransient
//New instance created with ID: a1
//New instance created with ID: a2
//New instance created with ID: b1
//New instance created with ID: b2

//AddScoped
//New instance created with ID: a1
//service1a ID: a1
//service1b ID: a1
//New instance created with ID: b1
//service2a ID: b1
//service2b ID: b1

//AddSingleton
//New instance created with ID: x1
//service1a ID: x1
//service1b ID: x1
//service2a ID: x1
//service2b ID: x1