using Milky.DataAccess.Repository.IRepository;
using Milky.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public UnitOfWork(ApplicationDbContext _context)
        {
            this._context = _context;
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
