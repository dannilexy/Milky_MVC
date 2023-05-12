using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkyRazor.Data;
using MilkyRazor.Models;

namespace MilkyRazor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Category> Categories { get; set; }
        public IndexModel(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public void OnGet()
        {
            Categories = _context.Categories.ToList();
        }
    }
}
