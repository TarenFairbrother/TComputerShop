using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models.ViewModels;

namespace TComputerShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {

        private ProdIndexVM ProdIndexVM;
        private readonly IProductRepository _iProdRepo;
        private HomeVM homeVM;

        public ProductController(IProductRepository iProdRepo)
        {
            _iProdRepo = iProdRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetCategory(string category)
        {
            homeVM = new HomeVM
            {
                Products = _iProdRepo.Get(c => c.Category.CategoryName == category,"Category"),
                Category = category
            };

            return View(homeVM);
        }
    }
}
