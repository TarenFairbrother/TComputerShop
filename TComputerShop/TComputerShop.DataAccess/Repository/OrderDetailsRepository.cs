using System;
using System.Collections.Generic;
using System.Text;
using TComputerShop.Data;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(OrderDetails OrderDetails)
        {
            _db.OrderDetails.Add(OrderDetails);
            _db.SaveChanges();
        }
    }
}
