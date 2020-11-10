using Kenze.Infrastructure;
using Kenze.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quintessence.QCandidate.Configuration;
using Quintessence.QCandidate.Filters.Actions;
using Quintessence.QCandidate.Logic.Queries;
using System.Globalization;
using System.Reflection;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Quintessence.QCandidate.Core.Domain;
using Quintessence.QCandidate.Infrastructure.EntityFrameworkCore.Commands;
using Quintessence.QCandidate.Logic.Commands;

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
                .AddRazorViewEngine()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix/*, opts => { opts.ResourcesPath = "Resources"; }*/);

            
            services.AddMediatR(typeof(GetAssessmentByCandidateIdAndDateAndLanguageQueryHandler).Assembly);
            services.AddMediatR(typeof(ChangeMemosOrderCommandHandler).Assembly );
            services.AddMediatR(typeof(CreateMemoProgramComponentCommandHandler).Assembly);
            services.AddMediatR(typeof(UpdateCalendarDayCommandHandler).Assembly);



            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddTransient<IPrincipalProvider, PrincipalProvider>();

            services.AddScoped<IDbConnectionFactory>(_ =>
                new SqlDbConnectionFactory(Configuration.GetConnectionString("QPlanet")));

            services.AddDbContext<DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("QPlanet")));

            services.AddScoped<QCandidateUnitOfWork>();
            services.AddScoped(typeof(IMemoProgramComponentRepository), typeof(MemoProgramComponentRepository));

            services.Configure<Settings>(Configuration);
            services.Configure<AzureAdB2CSettings>(Configuration.GetSection("AzureAdB2C"));

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

            //var localizationOption = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            var supportedCultures = new[]
            {
                new CultureInfo("nl"),
                new CultureInfo("en"),
                new CultureInfo("fr")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("nl"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

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
            });

        }
    }
}
