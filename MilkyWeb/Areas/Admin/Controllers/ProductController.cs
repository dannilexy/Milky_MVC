using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Milky.DataAccess.Repository.IRepository;
using Milky.Models.Models;
using Milky.Models.ViewModel;

namespace MilkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
           
            var products = unitOfWork.Product.GetAll(include: "Category");
            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            //ViewBag.CategoryList = CategoryList;
            var productVM = new ProductVM
            {
                Product = new Product(),
                CategoryList = CategoryList
            };
            if (id != null && id != 0)
            {
                var prod = unitOfWork.Product.GetFirstOrDefault(x => x.Id == id, include: "Category");
                productVM.Product = prod;
            }
            
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
              
                IEnumerable<SelectListItem> CategoryList = unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
               // ViewBag.CategoryList = CategoryList;
                var productVm = new ProductVM
                {
                    Product = new Product(),
                    CategoryList = CategoryList
                };
                return View(productVm);

            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    //Delete the Old image
                    var OldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.Trim('\\'));
                    if (System.IO.File.Exists(OldImagePath))
                    {
                        System.IO.File.Delete(OldImagePath);
                    }
                }

                using (var filestream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                {
                    file.CopyTo(filestream);
                }
                productVM.Product.ImageUrl = @"\images\product\" + filename;
            }

            if (productVM.Product.Id > 0)
            {
                
                unitOfWork.Product.update(productVM.Product);
                unitOfWork.Commit();
                TempData["Success"] = "Product updated successfully";
                return RedirectToAction(nameof(Index));
            }
            unitOfWork.Product.Add(productVM.Product);
            unitOfWork.Commit();
            TempData["Success"] = "Product Created successfully";
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
        #region Api
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = unitOfWork.Product.GetAll(include: "Category");
            return Json(new {data = products });
        }
        #endregion

    }
}
