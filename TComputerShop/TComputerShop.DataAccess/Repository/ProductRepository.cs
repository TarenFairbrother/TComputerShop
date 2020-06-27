using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TComputerShop.Data;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Add(Product product)
        {
            _db.Product.Add(product);
            _db.SaveChanges();
        }

        public List<Product> GetAll(string category = null)
        {
            if (category != null)
            {
                var product = _db.Product
                                 .Include(category)
                                 .ToList();

                return product.ToList();
            }

            return _db.Product.ToList();
        }

        public List<Product> GetDailyDeals()
        {
            IQueryable<Product> query = _db.Product.Where(p => p.DailyDeal);

            return query.ToList();
        }

        public Product GetFirstOrDefault(int id)
        {

            Product product = new Product();

            product = _db.Product.FirstOrDefault(p => p.Id == id);

            return product;
        }

        public void Remove(int id)
        {
            var objFromDb = _db.Product.Find(id);

            _db.Product.Remove(objFromDb);

            _db.SaveChanges();
        }

        public void Update(Product product)
        {
            var objFromDb = _db.Product.FirstOrDefault(p => p.Id == product.Id);

            objFromDb.Name = product.Name;
            objFromDb.Description = product.Description;
            objFromDb.Price = product.Price;
            objFromDb.Quantity = product.Quantity;
            objFromDb.ImageUrl = product.ImageUrl;
            objFromDb.CategoryId = product.CategoryId;
            objFromDb.DailyDeal = product.DailyDeal;

            _db.SaveChanges();
        }
    }
}
