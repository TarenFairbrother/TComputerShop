using System;
using System.Collections.Generic;
using System.Text;

namespace TComputerShop.Models.ViewModels
{
    public class cartVM
    {
        public List<Item> Items { get; set; }

        public Orders Order { get; set; }
    }
}
