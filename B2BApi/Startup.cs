using System;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;

using B2BApi.Extensions;
using B2BApi.Initializers;

namespace B2BApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            
            Configuration = builder.Build();
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddConfigurations(Configuration);
            services.AddMappingProfiles();
            services.AddSwagger();
            services.AddAJwtAuthentication(Configuration);
            services.AddDb(Configuration);
            
            // MARK - Hangfire
            services.AddHangfire(opt => opt.UseMemoryStorage());
            
            return services.RegisterContainer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerConfiguration();
                
                // MARK - Hangfire
                GlobalConfiguration.Configuration
                    .UseActivator(new HangfireActivator(serviceProvider));
                app.UseHangfireDashboard();
                app.UseHangfireServer();  
                
            } else {
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
            
            SeedData.EnsurePopulated(app);
        }
        
        private static string GetXmlCommentsPath()
        {
            return String.Format(@"{0}/B2BApi.xml", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}