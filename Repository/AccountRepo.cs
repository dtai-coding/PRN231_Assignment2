using BOs;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepo : IAccountRepo
    {
        public async Task<BranchAccount> Login(string email, string password)
        {
            return await AccountDAO.Instance.Login(email, password);
        }
    }
}
