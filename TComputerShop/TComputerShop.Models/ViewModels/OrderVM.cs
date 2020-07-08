using System;
using System.Collections.Generic;
using System.Text;

namespace TComputerShop.Models.ViewModels
{
    public class OrderVM
    {
        public List<OrderHeader> Orders { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        public ApplicationUser User { get; set; }
    }
}
