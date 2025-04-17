using AssetAPI;
using AssetAPI.Entity;
using AssetAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;
using System.Text;

namespace AssetAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(Options =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Enter Your JWT Access token",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                Options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
                Options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {jwtSecurityScheme,Array.Empty<string>() }
                });
            });
            services.AddEndpointsApiExplorer();

            //services.AddDbContext<AssetContext>(options =>
            //    options.UseSqlServer("Server=(localdb)\\MSSqlLocalDB;Database=AssetsDB;Trusted_Connection=True;"));
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AssetContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<IAssetRepository<Book>, BookRepository>();
            services.AddScoped<IAssetRepository<SoftwareLicense>, SoftwareRepository>();
            services.AddScoped<IAssetRepository<Hardware>, HardwareRepository>();
            services.AddScoped<IAssetRepository<Customer>, CustomerRepository>();
            services.AddScoped<IAssigningRepository, AssigningRepository>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = _configuration["JwtConfig:Issuer"],
                    ValidAudience = _configuration["JwtConfig:Audience"],
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]))
                }; ;


            });
            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello From ASP.NET Core Web API");
                });
                endpoints.MapControllers();
            });

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Please enter a valid API endpoint");
            //});
        }
    }
}

//AddAuthentication(...)
//You are telling ASP.NET Core
//I want to use JWT Bearer Authentication by default for all incoming requests unless told otherwise.
//DefaultAuthenticateScheme
//This is the first checkpoint to identify how to check ID cards. We say: "Check ID using JWT."
//DefaultChallengeScheme
//👉 When authentication fails, this tells ASP.NET what to do. Since we use JWT, it says, "Challenge with JWT mechanism."
//DefaultScheme
//👉 A fallback scheme for other scenarios — also set to JWT for simplicity.
//Analogy: These are your building’s gatekeepers who only know how to read a specific kind of ID card (JWT).

//AddJwtBearer(...)
//You’re configuring how the system should validate the JWT tokens
//RequireHttpsMetadata = false
//👉 By default, JWT bearer requires HTTPS to be enabled.
//❗ You can disable it only for development.
//✔️ In production, always keep it true for security.
//SaveToken = true
//👉 This tells ASP.NET to store the token in the current HTTP context once it is validated.
//✅ So you can access it later using HttpContext.GetTokenAsync(...).
//Analogy: The guard at the gate remembers the ID after it’s checked.
//TokenValidationParameters
//This tells the JWT system how to validate the incoming tokens.
//Analogy:
//The guard checks:
//Who issued this ID(issuer) ?
//Is it for this building(audience) ?
//Is it expired(lifetime) ?
//Is it fake(signature) ?