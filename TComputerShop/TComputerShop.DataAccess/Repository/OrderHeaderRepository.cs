using System;
using System.Collections.Generic;
using System.Text;
using TComputerShop.Data;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository
{
    public class OrderHeaderRepository : IOrderHeaderRepository
    {

        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(OrderHeader OrderHeader)
        {
            _db.OrderHeader.Add(OrderHeader);
            _db.SaveChanges();
        }
    }
}
