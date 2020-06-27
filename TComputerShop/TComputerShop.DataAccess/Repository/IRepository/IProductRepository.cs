using System;
using System.Collections.Generic;
using System.Text;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        public void Add(Product product);

        public void Remove(int id);

        public List<Product> GetAll(string category = null);

        public Product GetFirstOrDefault(int id);

        public void Update(Product product);

        public List<Product> GetDailyDeals();

    }
}
