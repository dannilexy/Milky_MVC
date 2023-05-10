using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkyWeb.Data;
using MilkyWeb.Models;

namespace MilkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IActionResult> Index()
        {
            var categoryList = await _context.Categories.ToListAsync();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order Cannot Exactly match the name");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order Cannot Exactly match the name");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category Updated Successfully";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }


}
