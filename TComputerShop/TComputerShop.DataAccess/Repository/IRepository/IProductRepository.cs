using System;
using System.Collections.Generic;
using System.Text;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository.IRepository
{
    public interface IProductRepository
    {
        public void Add(Product product);
    }
}
