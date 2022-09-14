using BulkyBook.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        //Get
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        // to create an category and post it in the database
        public IActionResult Create(Category obj)
        {
            // to check that the fields have not the same information
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The displayorder cannot exactly match the name.");
            }
            //to check if all the field are true
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(obj);
        }
        //GET

        //to edit one or more items that we have in the database

        public IActionResult Edit(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDb = _db.Categories.Find(id);
            //var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFromDbSingel = _db.Categories.SingleOrDefault(c => c.Id == id);
            //var categoryFromDbSingel = _db.Categories.SingleOrDefault(c => c.Id == id);
            return View(categoryFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        // to Edit an category and post it in the database
        public IActionResult Edit(Category obj)
        {
            // to check that the fields have not the same information
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The displayorder cannot exactly match the name.");
            }
            //to check if all the field are true
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(obj);

        }
    }
}
