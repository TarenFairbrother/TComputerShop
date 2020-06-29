using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;
using TComputerShop.Models.ViewModels;

namespace TComputerShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        IProductRepository _iProdRepo;
        private HomeVM homeVM;

        public HomeController(IProductRepository iProdRepo)
        {
            _iProdRepo = iProdRepo;
        }

        public IActionResult Index()
        {
            homeVM = new HomeVM
            {
                Products = _iProdRepo.GetDailyDeals()
            };
              
            return View(homeVM);
        }
    }
}
