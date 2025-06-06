using CRUD_YouTube.DataAccess.Repository.IRepository;
using CRUD_YouTube.Models;
using CRUD_YouTube.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CRUD_YouTube.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;
        public ProductController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Product> ProductList = _db.Product.GetAll().ToList();
            return View(ProductList);
        }

        // update+insert
        public IActionResult Upsert(int? id)  
        {
            // Projection in ef-core

            /**
            IEnumerable<SelectListItem> CategoryList = _db.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            */
            //01.
            //ViewBag.CategoryList = CategoryList;

            //02.
            //ViewData["CategoryList"] = CategoryList;

            //03. 
            ProductVM productVM = new()
            {
                CategoryList = _db.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if(id==null || id==0)     
            {
                // for insert
                return View(productVM);

            }
            else                     
            {
                // for update
                productVM.Product = _db.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _db.Product.Add(productVM.Product);
                _db.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                // For not showing ugly exception message when validation failed, it will stay on the same page.
                productVM.CategoryList = _db.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }  

        [ActionName("Delete")]
        public IActionResult DeleteProduct(int? id)
        {
            Product? obj = _db.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Product.Remove(obj);
            _db.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
