using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TComputerShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }
    }
}
