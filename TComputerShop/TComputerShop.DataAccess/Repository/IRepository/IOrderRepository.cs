using System;
using System.Collections.Generic;
using System.Text;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository.IRepository
{
    public interface IOrderRepository
    {
        public void Add(Orders Order);
    }
}
