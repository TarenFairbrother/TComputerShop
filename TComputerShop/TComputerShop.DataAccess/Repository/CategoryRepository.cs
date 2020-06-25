using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(Category category)
        {
            _db.Add(category);
            _db.SaveChanges();
        }

        public Category Get(int id)
        {
            Category category = new Category();

            category = _db.Category.Find(id);

            return category;
        }

        public IEnumerable<Category> GetAll() => _db.Category;

        public IEnumerable<SelectListItem> GetCategoryListForDropdown()
        {
            return _db.Category.Select(i => new SelectListItem()
            {
                Text = i.CategoryName,
                Value = i.Id.ToString()
            }); ;
        }

        public void Remove(int id)
        {
            var objFromDb = _db.Category.Find(id);

            _db.Category.Remove(objFromDb);

            _db.SaveChanges();
        }

        public void Remove(Category category)
        {
            throw new NotImplementedException();
        }

        public void Update(Category category)
        {
            var objFromDb = _db.Category.Find(category.Id);

            objFromDb.CategoryName = category.CategoryName;

            _db.SaveChanges();
        }
    }
}
