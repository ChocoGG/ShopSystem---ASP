using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopSystem.Core.Models;
using ShopSystem.WebUI.Controllers;
using MyShop.DataAccess.InMemory;

namespace ShopSystem.WebUI.Controllers
{
  
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            Product product = new Product();

            return View(product);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                //  Add Product
                context.Insert(product);
                //  Save List
                product.PriceHistory.Add(product.Price);
                product.priceHistoryTimeStamp.Add(DateTime.Now.TimeOfDay.ToString());

                context.Commit();
                
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string ID)
        {
            Product product = context.Find(ID);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string ID)
        {
            Product productToEdit = context.Find(ID);
            if(productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                productToEdit.PriceHistory.Add(productToEdit.Price);
                product.priceHistoryTimeStamp.Add(DateTime.Now.TimeOfDay.ToString());
                context.Commit();
               return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string ID)
        {
            Product productToDelete = context.Find(ID);
            if(productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string ID)
        {
            Product productToDelete = context.Find(ID);

            if(productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(ID);
                context.Commit();
                return RedirectToAction("index");
            }

        }
        public ActionResult Details(string ID)
        {
            Product productToSeeDetail = context.Find(ID);
            return View(productToSeeDetail);
        }
    }
}