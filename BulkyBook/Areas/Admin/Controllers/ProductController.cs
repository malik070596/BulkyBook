using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //Get
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }

        //GET

        //to edit one or more items that we have in the database

        public IActionResult Upsert(int? id)
        {
            #region
            //Product product = new();
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });
            //IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });
            #endregion
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name.ToString()
                }),

            };


            if (id == null || id == 0)
            {
                //Create Product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                //ViewBag.CoverTypleList = CoverTypeList;
                return View(productVM);
            }
            else
            {
                //Update product

            }

            return View(productVM);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        // to Edit an category and post it in the database
        public IActionResult Upsert(Category obj, IFormFile file)
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
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
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
            var category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

            return View(category);
        }

        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // to Delete an category and post it in the database
        public IActionResult DeletePost(int? id)
        {
            //take the id from the data base and if there is no id we will get nothing notfound
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            //(Remove) to Delete the data in the database and (savechanges) its to save it
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted Successfully";
            return RedirectToAction("Index");

        }
    }

}
