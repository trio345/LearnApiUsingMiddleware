using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LearnApiUsingMiddleware.Demo
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IPrinter _printer;

        public CustomMiddleware(RequestDelegate next, IPrinter printer)
        {
            this.next = next;
            this._printer = printer;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync("<br>This is from middleware context");
            await next(httpContext);
            _printer.Print();
        }
    }
}
