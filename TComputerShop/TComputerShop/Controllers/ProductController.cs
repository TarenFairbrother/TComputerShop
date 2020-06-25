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
        private ProductVM ProdVM;
        private ProdIndexVM ProdIndexVM;




        public ProductController(ICategoryRepository iCatRepo, IProductRepository iProdRepo)
        {
            _iCatRepo = iCatRepo;
            _iProdRepo = iProdRepo;
        }
        public IActionResult Index()
        {
            ProdIndexVM = new ProdIndexVM()
            {
                Products = _iProdRepo.GetAll(includeProperties:"Category")
                
            };
            return View();
        }
        public IActionResult Add() 
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
        public IActionResult Add(ProductVM prodVM)
        {
            if(ModelState.IsValid)
            {
                _iProdRepo.Add(prodVM.Product);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
