using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkyRazor.Data;
using MilkyRazor.Models;

namespace MilkyRazor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Category? Category { get; set; }
        public DeleteModel(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public void OnGet(int id)
        {
            Category = _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public IActionResult OnPost(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success"] = "Category Deleted Successfully!";
            return RedirectToPage("Index");
        }
    }
}
