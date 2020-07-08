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
    public class UsersController : Controller
    {
        private readonly IUsersRepository _iUsers;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private UsersVM usersVM;

        public UsersController(IUsersRepository iUsers,
                               UserManager<IdentityUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _iUsers = iUsers;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            usersVM = new UsersVM
            {
                Users = _iUsers.GetAll()
            };

            return View(usersVM);
        }

        public async Task<IActionResult> Details(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            bool admin;
            bool locked;

            if( await _userManager.IsInRoleAsync(user, "ADMIN"))
            {
                admin = true;
            }
            else
            {
                admin = false;
            }

            if(await _userManager.IsLockedOutAsync(user))
            {
                locked = true;
            }
            else
            {
                locked = false;
            }

            usersVM = new UsersVM
            {
                User = _iUsers.GetFirstOrDefault(id),
                Admin = admin,
                IsLocked = locked
            };

            return  View(usersVM);
        }

        public async Task<IActionResult> Lock(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);

            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

            return RedirectToAction("Details", "Users", new { id = user.Id });
        }

        public async Task<IActionResult> Unlock(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);

            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now);

            return RedirectToAction("Details", "Users", new { id = user.Id });
        }

        public async Task<IActionResult> MakeAdmin(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);

            await _userManager.AddToRoleAsync(user, "ADMIN");

            return RedirectToAction("Details", "Users", new { id = user.Id });
        }
        public async Task<IActionResult> RemoveAdmin(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);

            await _userManager.RemoveFromRoleAsync(user, "ADMIN");

            return RedirectToAction("Details", "Users", new { id = user.Id });
        }
    }
}
