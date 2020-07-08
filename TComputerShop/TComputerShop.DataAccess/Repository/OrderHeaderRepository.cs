using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TComputerShop.Data;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository
{
    public class OrderHeaderRepository : IOrderHeaderRepository
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<OrderHeader> dbSet;

        public OrderHeaderRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(OrderHeader OrderHeader)
        {
            _db.OrderHeader.Add(OrderHeader);
            _db.SaveChanges();
        }

        public List<OrderHeader> GetByUserId(string userId)
        {

            IQueryable<OrderHeader> query = _db.OrderHeader.Where(o => o.UserId == userId);

            
            return query.ToList();
        }

        public List<OrderHeader> GetAll()
        {
            return _db.OrderHeader.ToList();
        }
    }
}
