using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;
using TComputerShop.Models.ViewModels;

namespace TComputerShop.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _iCatRepo;
        private CategoryViewModel CatVM;

        public CategoryController(ICategoryRepository iCatRepo)
        {
            _iCatRepo = iCatRepo;
        }
        public IActionResult Index()
        {
            CatVM = new CategoryViewModel()
            {
                CategoryList = _iCatRepo.GetAll()
            };

            return View(CatVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Category category)
        {
            if(ModelState.IsValid)
            {
                _iCatRepo.Add(category);

                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("Error");
            return View();
        }

        public IActionResult Delete(int id)
        {
            _iCatRepo.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Category category = new Category();

            category = _iCatRepo.Get(id);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                _iCatRepo.Update(category);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
