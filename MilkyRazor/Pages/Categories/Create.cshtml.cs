using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkyRazor.Data;
using MilkyRazor.Models;

namespace MilkyRazor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["success"] = "Category Created Successfully!";
            return RedirectToPage("Index");
        }
    }
}
