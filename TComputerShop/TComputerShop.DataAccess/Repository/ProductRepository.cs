using System;
using System.Collections.Generic;
using System.Text;
using TComputerShop.Data;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(Product product)
        {
            _db.Product.Add(product);
            _db.SaveChanges();
        }
    }
}
