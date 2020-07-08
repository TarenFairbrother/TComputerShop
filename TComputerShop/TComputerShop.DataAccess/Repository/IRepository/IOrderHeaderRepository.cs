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

        public List<OrderHeader> GetByUserId(string userId);

        public List<OrderHeader> GetAll();

    }
}
