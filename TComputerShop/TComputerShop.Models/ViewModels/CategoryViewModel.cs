using System;
using System.Collections.Generic;
using System.Text;

namespace TComputerShop.Models.ViewModels
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> CategoryList { get; set; }

        public Category Category { get; set; }
    }
}
