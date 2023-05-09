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

        public async Task<IActionResult> Create()
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
            if (!ModelState.IsValid) {
                return View();
            }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
