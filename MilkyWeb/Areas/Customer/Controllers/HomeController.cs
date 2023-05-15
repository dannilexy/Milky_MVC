using Microsoft.AspNetCore.Mvc;
using Milky.DataAccess.Repository.IRepository;
using MilkyWeb.Models;
using System.Diagnostics;

namespace MilkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork _unitOfWork)
        {
            _logger = logger;
            this._unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            var productList = _unitOfWork.Product.GetAll(include: "Category");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(x=>x.Id == id, include: "Category");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}