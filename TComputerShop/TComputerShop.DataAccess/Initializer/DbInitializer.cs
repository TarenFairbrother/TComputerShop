using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TComputerShop.Data;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Initializer
{
    public class DbInitializer : IDbInitializer
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db,
                             UserManager<IdentityUser> userManager,
                             RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public void Initialize()
        {
            try
            {
                 if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex)
            {
            }

            if (_db.Roles.Any(r => r.Name == "Admin")) return;

            _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "tarenfairbrother@gmail.com",
                Email = "tarenfairbrother@gmail.com",
                EmailConfirmed = true,
                Name = "Taren Fairbrother"
            }, "Admin123*").GetAwaiter().GetResult();

            ApplicationUser user = _db.ApplicationUser.Where(u => u.Email == "tarenfairbrother@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
        }
    }
}
