using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Milky.DataAccess.Repository.IRepository;
using Milky.DataAcess.Data;
using Milky.Models.Models;

namespace MilkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            var categoryList = _unitOfWork.Category.GetAll();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order Cannot Exactly match the name");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            _unitOfWork.Category.Add(category);
            _unitOfWork.Commit();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order Cannot Exactly match the name");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            _unitOfWork.Category.update(category);
            _unitOfWork.Commit();
            TempData["success"] = "Category Updated Successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _unitOfWork.Category.Delete(category);
            _unitOfWork.Commit();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }


}
