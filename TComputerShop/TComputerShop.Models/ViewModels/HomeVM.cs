using System;
using System.Collections.Generic;
using System.Text;

namespace TComputerShop.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; }

        public string Category { get; set; }
    }
}
