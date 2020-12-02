using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreMVCIntro.Models;
using CoreMVCIntro.Models.Context;
using CoreMVCIntro.VMClasses;
using CoreMVCIntro.Models.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace CoreMVCIntro.Controllers
{
    //Code First icin Entity Framework Core kütüphanesini manage nuget'tan indirmeyi unutmayın..Migrations'i yapabilmek icin de özellikle EntityFramework Core tools gerekir



    public class HomeController : Controller
    {
        MyContext _db;
        public HomeController(MyContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        //.Net Core Authorization işlemleri

        //Async metotlar her zaman generic bir Task döndürmek durumundadırlar...İsterseniz döndürülen Task'i kullanın isterseniz kullanmayın ama döndürmek zorundasınız..Task class'ı asenkron metotların calısma prensipleri hakkında ayrıntılı bilgiyi tutan(Metot calısırken hata var mı,metodun bu görevi yapma sırasında kendisine eş zamanlı gelen istekler,metodun calısma durumu(success,flawed)..O yüzden normal şartlarda döndüreceginiz değeri Task'e generic olarak vermek zorundasınız...
        [HttpPost]
        public async Task<IActionResult> Login(Employee employee)
        {
            Employee loginEmployee = _db.Employees.FirstOrDefault(x => x.FirstName == employee.FirstName);
            if(loginEmployee != null)
            {
                //Claim , rol bazlı veya identity bazlı güvenlik işlemlerinden sorumlu olan bir class'tır..Siz dilerseniz birden fazla Claim nesnesi yaratıp hepsini aynı anda kullanabilirsiniz....
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role,loginEmployee.UserRole.ToString())
                };

                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login"); // burada login ismine sahip olan güvenlik durumu icin hangi güvenlik önlemlerinin calısacagını belirliyoruz

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity); //.Net Core'in icerisinde sonlanmıs olan security işlemlerinin artık tetiklenmesi lazım (yani login işleminin yapılması lazım)

                //asenkron metotlar calıstıkları zaman baska bir işlemin engellenmemesini saglayan metotlardır..

                //Eger siz bir async metot kullanıyorsanız mecburi bu metodu cagıran yapıya async keywordunu vermeniz gerekiyor...
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Product");

            }


            return View(new EmployeeVM { Employee = employee });
        }


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
