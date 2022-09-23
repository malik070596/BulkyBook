using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Tasks;
using System.Drawing.Imaging;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //Get
        public IActionResult Index()
        {

            return View();
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
            Company company = new();

            if (id == null || id == 0)
            {
                //Create Product
                #region
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                //ViewBag.CoverTypleList = CoverTypeList;
                #endregion
                return View(company);
            }
            else
            {
                //Update product
                company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }


        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        // to Edit an category and post it in the database
        public IActionResult Upsert(Company obj, IFormFile? file)
        {

            //to check if all the field are true
            if (ModelState.IsValid)
            {

                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company created Successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Company updated successfully";

                }
                //to update the data in the database and savechanges its to save it

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
                return View(obj);

            }


            #region API CALLS
            [HttpGet]
            public IActionResult GetAll()
            {
            var companyList = _unitOfWork.Company.GetAll();
                return Json(new { data = companyList });
            }
            //Post
            [HttpDelete]
            // to Delete an category and post it in the database
            public IActionResult Delete(int? id)
            {
                //take the id from the data base and if there is no id we will get nothing notfound
                var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                if (obj == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }
                
                //(Remove) to Delete the data in the database and (savechanges) its to save it
                _unitOfWork.Company.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete Successful" });

            }
            #endregion
        }

    }
