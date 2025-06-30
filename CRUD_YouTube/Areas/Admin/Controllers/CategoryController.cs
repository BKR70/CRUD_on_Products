using CRUD.DataAccess.Repository.IRepository;
using CRUD.Models;
using CRUD.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]       // It is also used before each method instead of being used globally.
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _db;
        public CategoryController(IUnitOfWork db)
        {
            _db = db;
        }

       // [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
            List<Category> CategoryList = _db.Category.GetAll().ToList();
            return View(CategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // It's custom error message. (Je field e message show hobe , error message)
                ModelState.AddModelError("name", "The displayorder cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Add(obj);
                _db.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? catfromDb = _db.Category.Get(u => u.Id == id);
            //Category? catfromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? catfromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();
            if (catfromDb == null)
            {
                return NotFound();
            }
            return View(catfromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(obj);
                _db.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        [ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)
        {
            Category? obj = _db.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Category.Remove(obj);
            _db.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
