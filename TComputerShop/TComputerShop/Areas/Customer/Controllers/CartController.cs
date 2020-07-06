using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TComputerShop.DataAccess.Helpers;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;
using TComputerShop.Models.ViewModels;

namespace TComputerShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IProductRepository _iProd;
        private readonly IOrderHeaderRepository _iOrderH;
        private readonly IOrderDetailsRepository _iOrderD;
        private readonly UserManager<IdentityUser> _userManager;
        private cartVM CartVM;


        public CartController(IProductRepository iProd,
                              IOrderHeaderRepository iOrderH,
                              IOrderDetailsRepository iOrderD,
                              UserManager<IdentityUser> userManager)
        {
            _iProd = iProd;
            _iOrderH = iOrderH;
            _iOrderD = iOrderD;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            CartVM = new cartVM()
            {
                Items = cart
            };

            return View(CartVM);
        }
        public IActionResult Add(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = _iProd.GetFirstOrDefault(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = _iProd.GetFirstOrDefault(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult CheckOut()
        {
            var userId = _userManager.GetUserId(this.User);

            if (userId != null)
            {
                List<Item> cart = new List<Item>();
                cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

                cartVM CartVM = new cartVM
                {
                    Items = cart
                };

                return View(CartVM);
            }

            return LocalRedirect("/Identity/Account/Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(cartVM CartVM)
        {

            if (ModelState.IsValid)
            {

                List<Item> cart = new List<Item>();
                cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

                CartVM.OrderHeader.OrderDate = DateTime.Now;
                CartVM.OrderHeader.Status = "Submitted";

                foreach (var item in cart)
                {
                    CartVM.OrderHeader.OrderTotal += (item.Product.Price * item.Quantity);
                }
                var userId = _userManager.GetUserId(this.User);

                CartVM.OrderHeader.UserId = userId;
                _iOrderH.Add(CartVM.OrderHeader);

                foreach (var item in cart)
                {
                    OrderDetails orderDetails = new OrderDetails
                    {
                        OrderHeaderId = CartVM.OrderHeader.Id,
                        ProductId = item.Product.Id,
                        ProductQuantity = item.Quantity,
                        ProductName = item.Product.Name,
                        ProductPrice = (item.Product.Price * item.Quantity)
                    };
                    _iOrderD.Add(orderDetails);
                }
                cart.Clear();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                return RedirectToAction("OrderConfirmation", "Cart", new { id = CartVM.OrderHeader.Id });

            }
            return View(nameof(Index));
        }

        public IActionResult OrderConfirmation(int id)

        {

            return View(id);

        }

        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
