using CRUD_YouTube.DataAccess.Repository.IRepository;
using CRUD_YouTube.Models;
using CRUD_YouTube.Models.ViewModels;
using CRUD_YouTube.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CRUD_YouTube.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;

        // for accessing wwwroot folder.
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> ProductList = _db.Product.GetAll(includeProperties:"Category").ToList();
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
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // delete old image
                        var oldImagePath = Path.Combine(wwwRootPath,productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if(productVM.Product.Id == 0)
                {
                    _db.Product.Add(productVM.Product);
                }
                else
                {
                    _db.Product.Update(productVM.Product);
                }
                
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

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> ProductList = _db.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data =  ProductList});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var prodToBeDeleted = _db.Product.Get(u=>u.Id==id);
            if(prodToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, prodToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _db.Product.Remove(prodToBeDeleted);
            _db.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
