using Microsoft.AspNetCore.Mvc;
using Milky.DataAccess.Repository.IRepository;
using Milky.Models.Models;

namespace MilkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var products = unitOfWork.Product.GetAll();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if(!ModelState.IsValid)
                return View(product);
            unitOfWork.Product.Add(product);
            unitOfWork.Commit();
            TempData["Success"] = "Product Created";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var product = unitOfWork.Product.GetFirstOrDefault(x=>x.Id == id);
            if(product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);
            unitOfWork.Product.update(product);
            unitOfWork.Commit();
            TempData["Success"] = "Product Updated Successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var product = unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            unitOfWork.Product.Delete(product);
            unitOfWork.Commit();
            TempData["Success"] = "Product deleted Successfully";
            return RedirectToAction(nameof(Index));
        }


    }
}
