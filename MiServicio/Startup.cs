using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MiServicio
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation($"Configuring Pipeline for Environment: {env.EnvironmentName}");
            app.Use(async (context, next) =>
            {
                logger.LogInformation($"context.Request.Host: {context.Request.Host}");
                logger.LogInformation($"context.Request.Path: {context.Request.Path}");
                logger.LogInformation($"context.Request.Scheme: {context.Request.Scheme}");
                logger.LogInformation($"context.Request.PathBase: {context.Request.PathBase}");
                await next.Invoke();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            logger.LogInformation($"Pipeline configured");

        }
    }
}
