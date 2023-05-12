using Milky.DataAccess.Repository.IRepository;
using Milky.DataAcess.Data;
using Milky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milky.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext _context) : base(_context) 
        {
            this._context = _context;
        }
      

        public void update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
