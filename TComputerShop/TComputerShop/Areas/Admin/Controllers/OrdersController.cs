using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;
using TComputerShop.Models.ViewModels;

namespace TComputerShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderHeaderRepository _iOrderH;
        private readonly IOrderDetailsRepository _iOrderD;
        private OrderVM orderVM;

        public OrdersController(IOrderHeaderRepository iOrderH,
                                IOrderDetailsRepository iOrderD)
        {
            _iOrderH = iOrderH;
            _iOrderD = iOrderD;
        }
        public IActionResult Index()
        {
            orderVM = new OrderVM
            {
                Orders = _iOrderH.GetAll()
            };

            return View(orderVM);
        }

        public IActionResult Details(int id)
        {
            orderVM = new OrderVM
            {
                OrderDetails = _iOrderD.Get(o => o.OrderHeaderId == id)
            };

            return View(orderVM);
        }
    }
}
