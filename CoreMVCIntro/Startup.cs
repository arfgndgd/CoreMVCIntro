using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVCIntro.Models.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreMVCIntro
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
            //Hangi servisler kullanılacak bunları ekliyoruz. Başlangıçta "services.AddControllersWithViews();" olarak gelir henüz kullanmadık. dikkat!!

            //Standart bir  Sql bağlantısı için (MyContextte optionbuilder içerisinde belirtmektense) burayı kullanırız. Diğeri esnek değil.

            //Core içerisinde SingletonPattern gömülüdür. Pool kullanmak bu görevi görür.
            //Bağlantı ayarı bu şekilde yapılır
            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection"))); 



            //***Önemli: Authentication işlemini yapabilmek için servisi burada yaratmak gerekir.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {

                options.LoginPath = "/Home/Login";

            });




            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Authenticationı Authorizationdan önce vermek önemli

            app.UseAuthorization();//Kullanıcı kim bunu algıla demektir.

            app.UseAuthorization(); //sizin yetkiniz var mı yok mu gibi durumlarda(Authorization durumlarında ) calısacak bir metottur...

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Category}/{action=Index}/{id?}");
            });
        }
    }
}
