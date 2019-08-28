using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo_NET_CORE_API
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

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Linear middleware data passing

            // Using IHostingEnvironment service instance to access launchSettings.json JSON format environment variable data
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions dvp = new DeveloperExceptionPageOptions {
                    SourceCodeLineCount = 50
                };

                // For development error debugging
                app.UseDeveloperExceptionPage(dvp);
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

           
            app.UseFileServer();

            app.UseHttpsRedirection();

            // We are able to serve static files from wwwroot folder
            app.UseStaticFiles();

            // Servce static file like js, css, image, among other in asset folder of angular app
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            /*
              Let asp.net core know which directory you want to run your angular app, what dist folder when 
              runnning in production mode and which command to run angular app in dev mode
            */
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                // Modify this in the launchSettings.json
                /*
                 * 
                "profiles": {
                "IIS Express": {
                  "commandName": "IISExpress",
                  "launchBrowser": true,
                  "environmentVariables": {
                    "ASPNETCORE_ENVIRONMENT": "Development" <--
                  }
                }, 
                */
            if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            /* app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Environment is " + env.EnvironmentName);
            }
            ) */
        }
    }
}
