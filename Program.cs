using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Mentorship.Data;
using Microsoft.EntityFrameworkCore;
using Mentorship.Data.Services;
using Microsoft.Identity.Web.UI;
using System;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.DataProtection;

namespace Mentorship
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public class Startup
        {
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            /*public void ConfigureServices(IServiceCollection services)
            {
                var initialScopes = Configuration.GetValue<string>("DownstreamApi:Scopes")?.Split(' ');

                services.AddDbContext<MentorshipDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MentorshipDbConnection")));
                //Services configuration
                services.AddControllersWithViews();

           
                services.AddAuthorization();
                services.AddScoped<IMentorService, MentorsService>();
                services.AddScoped<IUserService, UserServices>();
                services.AddMicrosoftIdentityWebAppAuthentication(Configuration);
                services.AddControllersWithViews();

                services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"))
                    .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
                        .AddMicrosoftGraph(Configuration.GetSection("DownstreamApi"))
                        .AddInMemoryTokenCaches();

                services.AddControllersWithViews(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                });
                services.AddRazorPages()
                     .AddMicrosoftIdentityUI();
            }*/
            public void ConfigureServices(IServiceCollection services)
            {
                var initialScopes = Configuration.GetValue<string>("DownstreamApi:Scopes")?.Split(' ');

                // The scope for reading user profile
                var allScopes = initialScopes.Concat(new[] { "User.Read" }).ToArray();


                services.AddDbContext<MentorshipDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MentorshipDbConnection")));

                services.AddScoped<IMentorService, MentorsService>();
                services.AddScoped<IUserService, UserServices>();

                services.AddMicrosoftIdentityWebAppAuthentication(Configuration)
                    .EnableTokenAcquisitionToCallDownstreamApi(allScopes)
                    .AddMicrosoftGraph(Configuration.GetSection("DownstreamApi"))
                    .AddInMemoryTokenCaches();

                services.AddAuthorization();

                services.AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo(@"path\to\keys"));

                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                services.AddControllersWithViews(options =>
                {
                    options.Filters.Add(new AuthorizeFilter(policy));
                });

                services.AddRazorPages()
                    .AddMicrosoftIdentityUI();
            }


            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });
            }

        }
    }
}
