using BOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private readonly SilverJewelry2023DbContext _context;

        private AccountDAO()
        {
            _context = new SilverJewelry2023DbContext();
        }

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public async Task<BranchAccount> Login(string email, string password)
        {
            return await _context.BranchAccounts.FirstOrDefaultAsync(a => a.EmailAddress == email && a.AccountPassword == password);
        }
    }
}
