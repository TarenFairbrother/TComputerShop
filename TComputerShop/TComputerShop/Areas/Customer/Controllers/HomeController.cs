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

    public class HomeController : Controller
    {

        IProductRepository _iProdRepo;

        public HomeController(IProductRepository iProdRepo)
        {
            _iProdRepo = iProdRepo;
        }

        [Area("Customer")]
        public IActionResult Index()
        {
            HomeVM hvm = new HomeVM
            {
                DailyDealProducts = _iProdRepo.GetDailyDeals()
            };
              
            return View(hvm);
        }
    }
}
