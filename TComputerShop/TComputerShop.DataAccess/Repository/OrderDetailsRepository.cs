using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<OrderDetails> dbSet;

        public OrderDetailsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(OrderDetails OrderDetails)
        {
            _db.OrderDetails.Add(OrderDetails);
            _db.SaveChanges();
        }

        public List<OrderDetails> Get(Expression<Func<OrderDetails, bool>> filter = null, string includeProperties = null)
        {

            IQueryable<OrderDetails> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);

            }

            if (includeProperties != null)

            {
                query = query.Include(includeProperties);

            }
            return query.ToList();
        }

        public List<OrderDetails> GetOrderDetails(int id)
        {
            IQueryable<OrderDetails> orders;

            orders = _db.OrderDetails.Where(o => o.OrderHeaderId == id);

            return orders.ToList();
        }
    }
}
