using System;
using System.Collections.Generic;
using System.Text;
using TComputerShop.Data;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(Orders Order)
        {
            _db.Orders.Add(Order);
            _db.SaveChanges();
        }
    }
}
