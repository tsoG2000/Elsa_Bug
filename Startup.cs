using Elsa.Providers.Workflows;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Storage.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.IISIntegration;
using ITS.Callback.Web.Elsa.Workflows;

namespace ITS.Callback.Web
{
    public class Startup
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IWebHostEnvironment WebHostEnvironment { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebHostEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            var elsaSection = Configuration.GetSection("Elsa");

            services
                .AddElsa(elsa => elsa
                    .UseEntityFrameworkPersistence(ef => ef.UseSqlServer(elsaSection.GetSection("SqlConnection").Value))
                    .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                    .AddConsoleActivities()
                    .AddJavaScriptActivities()
                    .AddActivitiesFrom(Assembly.GetExecutingAssembly())
                    .AddWorkflowsFrom<TestIfBug>()
                    );
                    //.UseHangfireDispatchers());


            // Elsa API endpoints.
            services.AddElsaApiEndpoints();

            // Elsa Swagger
            services.AddElsaSwagger(c =>
            {
            });

            // For Dashboard.
            services.AddRazorPages();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "ITS.Callback.Web v1");
                //c.RoutePrefix = string.Empty; // Does not work with redirect
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseHttpActivities();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // For Dashboard.
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
