using LearnApiUsingMiddleware.ActionFilters;
using LearnApiUsingMiddleware.Demo;
using LearnApiUsingMiddleware.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;

namespace LearnApiUsingMiddleware
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LearnApiUsingMiddleware", Version = "v1" });
            });
            // services.AddSingleton<IPrinter, Printer>();


            services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TestDb")));

            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ValidateEntityExistAttribute<Movie>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LearnApiUsingMiddleware v1"));

            }

            // middleware
            /*app.Run(MyMiddleware);*/
            // app.Run(async (context) => await context.Response.WriteAsync("Hello world guys"));

            // Middleware use and run
            /*app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello World from Middleware 1st");

                await next();
            });*/
            /*app.UseMiddleware();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World from Middleware 2st");
            });*/



            // app.UseWelcomePage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "movies",
                    pattern: "api/{controller=Movies}/{id?}"
                    );
            });
            // app.UseExtensions();
            // app.UseMiddleware<CustomMiddleware>();
        }

        private async Task MyMiddleware(HttpContext context)
        {
            await context.Response.WriteAsync("Hello World");
        }

    }
}
