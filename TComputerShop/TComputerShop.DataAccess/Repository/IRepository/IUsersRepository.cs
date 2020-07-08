using System;
using System.Collections.Generic;
using System.Text;
using TComputerShop.Models;

namespace TComputerShop.DataAccess.Repository.IRepository
{
    public interface IUsersRepository
    {
        public List<ApplicationUser> GetAll();

        public ApplicationUser GetFirstOrDefault(string id);

        public void Update(ApplicationUser User);
    }
}
