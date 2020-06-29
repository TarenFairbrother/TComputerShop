using System;
using System.Collections.Generic;
using System.Text;
using TComputerShop.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq.Expressions;

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

        public List<Product> Get(Expression<Func<Product, bool>> filter = null, string includeProperties = null);

    }
}
