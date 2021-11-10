using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProducts.Models;
using System.Data.Entity;
using System.ComponentModel;

namespace WebProducts.Controllers
{
    public class ProductController : Controller
    {
        ProductDBContext context = new ProductDBContext();
        // GET: Product
        public ActionResult Index(string category, string name)
        {
            int cat = string.IsNullOrEmpty(category) ? 0 : 2;
            int nam = string.IsNullOrEmpty(name) ? 0 : 1;
            int val = cat | nam;
            switch (val)
            {
                case 0:
                    return View(context.Products.ToList());
                case 1:
                    return View((from product in context.Products
                                 where product.ProductName == name
                                 select product).ToList<Product>());
                case 2:
                    return View((from product in context.Products
                                 where product.Category == category
                                 select product).ToList<Product>());
                case 3:
                    return View((from product in context.Products
                                 where product.Category == category &&
                                 product.ProductName == name
                                 select product).ToList<Product>());
                default:
                    return View();
                    
            }            
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            Product product = context.Products.Find(id);

            if (product != null)
            {
                return View("Detail", product);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            Product product = new Product();
            return View("Create", product);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", product);
        }
        public ActionResult Edit(int id)
        {
            Product product = context.Products.Find(id);
            if (product != null)
            {
                return View("Edit", product);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", product);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product product = context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View("Delete", product);
        }
        [HttpPost]  
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }       
    }
}