using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVCIntro.Models.Context;
using CoreMVCIntro.Models.Entities;
using CoreMVCIntro.VMClasses;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVCIntro.Controllers
{
    public class CategoryController : Controller
    {
        //SOLID => Interface Segregation ve Dependency Inversion   Interface Segregation İlgili görevlerin sorumluluklarına uygun bicimde ayrın interfaceler icerisinde bulunmasıdır...Dependency Inversion ise bir yapının herhangi bir yere olan bagımlılıgın gevsek tutulmasıdır...Dependency Inversion prensibini uygulamak icin Dependency Injection dedigimiz tasarım paternini kullanırız...Bu patern istedigimiz an istedigimiz şekilde istedigimiz sorumlulugun hemen o an icin enjekte edilmesini saglayan bir tasarım paternidir...Dependency Injection en rahat Interface yapısıyla kullanılır(Böyle istedigimiz an sorumlulugu degiştirebiliriz) 

        //Artık Startup'ta yaptıgımız ayarlamalar sayesinde projenizin herhangi bir yerinde bir herhangi bir metotta MyContext tipinde bir parametre görüldügü anda otomatik olarak singletonpattern uygulanarak ram'deki yapı getirilecek...


        //Sizin _ViewImports dosyanız View'larınız kullanacagı ortak namespace alanlarını belirlediginiz bir dosyadır...


        //.Net Core, MVC Helper'larinizi korumasının yanı sıra size daha kolay ve daha performanslı bir yapı da sunar...Bunlara Tag Helper'lar denir..Tag Helper'lar normal HTML tagler'inin icerisine yazılan attribute'lardır....Kullanablimek icin namespace'leri gereklidir(Zaten _ViewImports'unuzda vardır)


        //Projenizi watch run olarak izleyebilmek adına projenizin klasorune gidip cmd ekranına girerek ilgili terminale dotnet watch run komutunu yazmanız gerekir. Böylece projenize yaptıklarını kaydettikten sonra sayfanızı refresh ederek degişiklikleri gözlemleyebilirsiniz...
        MyContext _db;

        public CategoryController(MyContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            CategoryVM cvm = new CategoryVM
            {
                Categories = _db.Categories.ToList()
            };
            return View(cvm);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category) //eger bir yapının icerisindeki property ismiyle buradaki parametre ismi tutuyorsa Bind attribute'una gerek yoktur...
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateCategory(int id)
        {
            CategoryVM cvm = new CategoryVM()
            {
                Category = _db.Categories.Find(id)
            };

            return View(cvm);
        }


        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            Category toBeUpdated = _db.Categories.Find(category.ID);
            toBeUpdated.CategoryName = category.CategoryName;
            toBeUpdated.Description = category.Description;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int id)
        {
            _db.Categories.Remove(_db.Categories.Find(id));
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
       
    }
}
