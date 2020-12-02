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
    public class ProductController : Controller
    {
        MyContext _db;

        public ProductController(MyContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ProductVM pvm = new ProductVM()
            {
                Products = _db.Products.ToList(),
                Categories = _db.Categories.ToList()
            };
            return View(pvm);
        }

        public IActionResult AddProduct()
        {
            ProductVM pvm = new ProductVM()
            {
                Categories = _db.Categories.ToList()
            };
            return View(pvm);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult UpdateProduct(int id)
        {
            ProductVM pvm = new ProductVM()
            {
                Categories = _db.Categories.ToList(),
                Product = _db.Products.Find(id)
            };
            return View(pvm);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            Product toBeUpdated = _db.Products.Find(product.ID);
            toBeUpdated.CategoryID = product.CategoryID;
            toBeUpdated.ProductName = product.ProductName;
            toBeUpdated.UnitsInStock = product.UnitsInStock;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult DeleteProduct(int id)
        {
            _db.Products.Remove(_db.Products.Find(id));
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
