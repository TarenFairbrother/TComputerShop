using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository 
    {
        public Category Get(int id);

        public IEnumerable<Category> GetAll();

        public void Add(Category category);

        public void Remove(int id);

        public void Remove(Category category);

        public void Update(Category category);

        IEnumerable<SelectListItem> GetCategoryListForDropdown();
    }
}
