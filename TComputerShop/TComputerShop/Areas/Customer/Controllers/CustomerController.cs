using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;
using TComputerShop.Models.ViewModels;

namespace TComputerShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CustomerController : Controller
    {
        private readonly IUsersRepository _iUser;
        private readonly IOrderHeaderRepository _iOrderH;
        private readonly IOrderDetailsRepository _iOrderD;
        private readonly UserManager<IdentityUser> _userManager;
        private UsersVM usersVM;

        public CustomerController(IUsersRepository iUser,
                                  UserManager<IdentityUser> userManager,
                                  IOrderHeaderRepository iOrderH,
                                  IOrderDetailsRepository iOrderD)
        {
            _iUser = iUser;
            _userManager = userManager;
            _iOrderH = iOrderH;
            _iOrderD = iOrderD;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var userid = _userManager.GetUserId(this.User);

            usersVM = new UsersVM
            {
                User = _iUser.GetFirstOrDefault(userid)
            };

            return View(usersVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ApplicationUser User)
        {
            if(ModelState.IsValid)
            {
                _iUser.Update(User);
                TempData["SuccessMessage"] = "Account Details Updated";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Error Account Details Not Updated";
            return View();
        }

        [HttpGet]
        public IActionResult Orders()
        {
            var userid = _userManager.GetUserId(this.User);

            var user = _iUser.GetFirstOrDefault(userid);

            OrderVM orderVM = new OrderVM
            {
                Orders = _iOrderH.GetByUserId(userid),
                User = user
            };

            return View(orderVM);
        }

        public IActionResult OrderDetails(int id)
        {
            OrderVM orderVM = new OrderVM
            {
                OrderDetails = _iOrderD.GetOrderDetails(id)
            };

            return View(orderVM);
        }
    }
}
