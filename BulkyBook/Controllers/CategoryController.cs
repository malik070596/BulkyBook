﻿using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;
        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }
        //Get
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.GetAll();
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
                _db.Add(obj);
                _db.Save();
                //the message of the creating is successed
                TempData["success"] = "Category created Successfully";
                return RedirectToAction("Index");

            }
            return View(obj);
        }
        //GET

        //to edit one or more items that we have in the database

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDb = _db.Categories.Find(id);
            var category = _db.GetFirstOrDefault(u =>u.Id==id);
            //var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFromDbSingel = _db.Categories.SingleOrDefault(c => c.Id == id);
            //var categoryFromDbSingel = _db.Categories.SingleOrDefault(c => c.Id == id);
            return View(category);
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
                //to update the data in the database and savechanges its to save it
                _db.Update(obj);
                _db.Save();
                TempData["success"] = "Category updated Successfully";
                return RedirectToAction("Index");

            }
            return View(obj);

        }

        //GET
        //to edit one or more items that we have in the database
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var category = _db.GetFirstOrDefault(u => u.Id == id);

            return View(category);
        }

        //Post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // to Delete an category and post it in the database
        public IActionResult DeletePost(int? id)
        {
            //take the id from the data base and if there is no id we will get nothing notfound
            var obj = _db.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            //(Remove) to Delete the data in the database and (savechanges) its to save it
            _db.Remove(obj);
            _db.Save();
            TempData["success"] = "Category deleted Successfully";
            return RedirectToAction("Index");

        }
    }

}
