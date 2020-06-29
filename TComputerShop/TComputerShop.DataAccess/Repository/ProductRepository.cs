using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TComputerShop.Data;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

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
            IQueryable<Product> query = _db.Product.Where(p => p.DailyDeal)
                                                    .Include(c => c.Category);

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

        private void BadRequest()
        {
            throw new NotImplementedException();
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

        public List<Product> Get(Expression<Func<Product, bool>> filter = null, string includeProperties = null)
        {

            IQueryable<Product> query = dbSet;

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
    }
}
