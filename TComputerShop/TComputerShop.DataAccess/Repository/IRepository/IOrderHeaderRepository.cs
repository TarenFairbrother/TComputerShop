using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository
    {
        public void Add(OrderHeader OrderHeader);

        public List<OrderHeader> Get(Expression<Func<OrderHeader, bool>> filter = null, string includeProperties = null);

        public List<OrderHeader> GetAll();

    }
}
