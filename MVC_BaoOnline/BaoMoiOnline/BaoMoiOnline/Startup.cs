using BaoMoiOnline.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace BaoMoiOnline
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
            var stringConnectdb = Configuration.GetConnectionString("dbBaoOnline"); // lấy kết nối từ connect string
            services.AddDbContext<BaoOnlineContext>(options => options.UseSqlServer(Configuration.GetConnectionString("dbBaoMoiOnline"))); // kết nối
            services.AddSession(); // tạo ra 1 session để login
            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
            services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddAuthentication("CookieAuthentication")
                    .AddCookie("CookieAuthentication", config => {

                        config.Cookie.Name = "UserLoginCookie";
                        config.ExpireTimeSpan = TimeSpan.FromDays(1);
                        config.LoginPath = "/dang-nhap.html";
                        config.LogoutPath = "/dang-xuat.html";
                        config.AccessDeniedPath = "/404.html";
                    });
            services.ConfigureApplicationCookie(option => {

                // Cookie setting, only this changes expiration
                option.Cookie.HttpOnly = true;
                option.Cookie.Expiration = TimeSpan.FromDays(150);
                option.ExpireTimeSpan = TimeSpan.FromDays(150);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession(); // <= login

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            // who are you ?
            app.UseAuthentication();

            app.UseAuthorization();
            // are you allowed?

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
