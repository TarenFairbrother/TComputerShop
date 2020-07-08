using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TComputerShop.Models.ViewModels
{
    public class UsersVM
    {
        public List<ApplicationUser> Users { get; set; }

        public ApplicationUser User { get; set; }

        public Task<IList<string>> Role { get; set; }

        public bool Admin { get; set; }

        public bool IsLocked { get; set; }
    }
}
