using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;
using TComputerShop.Models.ViewModels;

namespace TComputerShop.Controllers
{
    public class ProductController : Controller
    {

        private readonly ICategoryRepository _iCatRepo;
        private readonly IProductRepository _iProdRepo;

        [BindProperty]
       public ProductVM ProdVM { get; set; }




        public ProductController(ICategoryRepository iCatRepo)
        {
            _iCatRepo = iCatRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(int? id) 
        {
            ProdVM = new ProductVM()
            {
                Product = new Models.Product(),
                CategoryList = _iCatRepo.GetCategoryListForDropdown()
            };
            return View(ProdVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add()
        {
            if(ModelState.IsValid)
            {
                _iProdRepo.Add(ProdVM.Product);
                RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
