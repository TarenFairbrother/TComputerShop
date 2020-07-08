using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository.IRepository
{
    public interface IOrderDetailsRepository
    {
        public void Add(OrderDetails OrderDetails);

        public List<OrderDetails> Get(Expression<Func<OrderDetails, bool>> filter = null, string includeProperties = null);

        public List<OrderDetails> GetOrderDetails(int id);
    }
}
