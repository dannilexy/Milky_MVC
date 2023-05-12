using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MilkyRazor.Data;
using MilkyRazor.Models;

namespace MilkyRazor.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Category? Category { get; set; }
        public EditModel(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public void OnGet(int id)
        {
            Category = _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public IActionResult OnPost(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            TempData["success"] = "Category Updated Successfully!";
            return RedirectToPage("Index");
        }
    }
}
