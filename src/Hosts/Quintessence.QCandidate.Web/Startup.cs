using Kenze.Infrastructure;
using Kenze.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
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

            services.AddMvcCore(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddRazorViewEngine();
            services.AddMediatR(typeof(GetAssessmentByCandidateIdAndDateQueryHandler).Assembly);
            services.AddScoped<IDbConnectionFactory>(_ =>
                new SqlDbConnectionFactory(Configuration.GetConnectionString("QPlanet")));

            services.Configure<Settings>(Configuration);

            services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
                .AddAzureADB2C(options => Configuration.Bind("AzureAdB2C", options));
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

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Assessments}/{action=Get}");
                endpoints.MapRazorPages();
            });

        }
    }
}
