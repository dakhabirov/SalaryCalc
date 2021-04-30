using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SalaryCalc.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            if (context.Response.StatusCode == 404)
                await context.Response.WriteAsync("Ooops... Page not found");
        }
    }
}
