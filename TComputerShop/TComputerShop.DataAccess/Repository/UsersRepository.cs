using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TComputerShop.Data;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository
{ 
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _db;

        public UsersRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<ApplicationUser> GetAll()
        {
            IQueryable<ApplicationUser> query;

            query = _db.ApplicationUser;

            return query.ToList();
        }

        public ApplicationUser GetFirstOrDefault(string id)
        {
            ApplicationUser user = new ApplicationUser();

            user = _db.ApplicationUser.Find(id);

            return user;
        }

        public void Update(ApplicationUser User)
        {
            var user = _db.ApplicationUser.Find(User.Id);

            user.Name = User.Name;
            user.Email = User.Email;
            user.City = User.City;
            user.Address = User.Address;
            user.PostalCode = User.PostalCode;

            _db.ApplicationUser.Update(user);
            _db.SaveChanges();
        }
    }
}
