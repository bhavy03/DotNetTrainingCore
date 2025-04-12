using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace ConsoleToWeb
{
    public class CustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Custom Middleware Incoming Request \n");
            await next(context);
            await context.Response.WriteAsync("Custom Middleware Outgoing Response \n");
        }
    }
}
