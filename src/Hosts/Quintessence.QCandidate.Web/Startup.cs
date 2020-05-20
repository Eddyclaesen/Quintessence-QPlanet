using Kenze.Infrastructure;
using Kenze.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quintessence.QCandidate.Filters.Actions;
using Quintessence.QCandidate.Logic.Queries;
using Quintessence.QCandidate.Models;

namespace Quintessence.QCandidate
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
            services
                .AddApplicationInsightsTelemetry()
                .AddControllers(options =>
                {
                    options.Filters.Add<SerilogControllerLoggingFilter>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddMvcCore()
                .AddRazorViewEngine();
            services.AddMediatR(typeof(GetAssessmentByCandidateIdAndDateQueryHandler).Assembly);
            services.AddScoped<IDbConnectionFactory>(_ =>
                new SqlDbConnectionFactory(Configuration.GetConnectionString("QPlanet")));

            services.Configure<Settings>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            // Uncomment the following line(s) if you require authentication or authorization.
            // Keep in mind that this call needs to be done AFTER UseRouting
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
