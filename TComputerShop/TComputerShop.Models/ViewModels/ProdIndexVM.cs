using System;
using System.Collections.Generic;
using System.Text;

namespace TComputerShop.Models.ViewModels
{
    public class ProdIndexVM
    {
        public IEnumerable<Product> Products { get; set; }

        public Category Category { get; set; }
    }
}
