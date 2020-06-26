using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;
using TComputerShop.Models.ViewModels;

namespace TComputerShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly ICategoryRepository _iCatRepo;
        private readonly IProductRepository _iProdRepo;
        private readonly IWebHostEnvironment _hostEnvironment;
        private ProductVM ProdVM;
        private ProdIndexVM ProdIndexVM;

        public ProductController(ICategoryRepository iCatRepo,
                                 IProductRepository iProdRepo,
                                 IWebHostEnvironment hostEnvironment)
        {
            _iCatRepo = iCatRepo;
            _iProdRepo = iProdRepo;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ProdIndexVM = new ProdIndexVM()
            {
                Products = _iProdRepo.GetAll(includeProperties:"Category")
                
            };
            return View(ProdIndexVM);
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

                string webRootPath = _hostEnvironment.WebRootPath;

                var files = HttpContext.Request.Form.Files;

                if(files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();

                    var uploads = Path.Combine(webRootPath, @"images\products");

                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    prodVM.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                _iProdRepo.Add(prodVM.Product);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    
        public IActionResult Delete(int id)
        {
            _iProdRepo.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
