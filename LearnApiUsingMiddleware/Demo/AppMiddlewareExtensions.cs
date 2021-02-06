using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnApiUsingMiddleware.Demo
{
    public static class AppMiddlewareExtensions 
    {
        public static void UseExtensions(this IApplicationBuilder app)
        {
            // middleware 
            // app.Run(async context => await context.Response.WriteAsync("Response from first middleware"));

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("<html><body>Response from use middleware");
                await next(); // execute next middleware
                await context.Response.WriteAsync("<br>End of middleware <body><html>"); // will be execute after all midd executed
            });

            app.UseWhen(c => c.Request.Query.ContainsKey("role"), a =>
            {
                a.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync($"<br>Role is {context.Request.Query["role"]}");
                    await next();
                });
            });

            app.Map("/map", a =>
            {
                a.Map("/branch", x => x.Run(async context => await context.Response.WriteAsync("<br>New child map is here")));
                a.Run(async context =>
                {
                    await context.Response.WriteAsync("<br>This response from map");
                });
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("<br>Response from seconds middleware");
            });
        }
    }
}
