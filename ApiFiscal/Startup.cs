using ApiFiscal.Core;
using ApiFiscal.Core.Application.Afip;
using ApiFiscal.Core.Domain.Afip.Interfaces.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiFiscal
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IErrorEvents, ErrorEvents>();
            services.AddScoped<ISendApp, SendApp>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseConventionalMiddleware();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    //public static class MiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseConventionalMiddleware(
    //        this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<ConventionalMiddleware>();
    //    }
    //}

    //public class ConventionalMiddleware
    //{
    //    private readonly RequestDelegate _next;

    //    public ConventionalMiddleware(RequestDelegate next)
    //    {
    //        _next = next;
    //    }

    //    public async Task Invoke(HttpContext context)
    //    {
    //        await _next(context);
    //    }
    //}
}
