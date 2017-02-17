using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyATM.Models;

namespace MyATM.Services
{
    public class CheckingAccountService
    {
        private ApplicationDbContext db;
        public CheckingAccountService(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public void CreateCheckingAccount(string firstName, string lastName, string userId, decimal iniBalance)
        {
            var accountNumber = (1 + db.CheckingAccounts.Count()).ToString().PadLeft(10, '0');
            var checkingAccount = new CheckingAccount { FirstName = firstName, LastName = lastName, AccountNumber = accountNumber, Balance = iniBalance, ApplicationUserId = userId };
            db.CheckingAccounts.Add(checkingAccount);
            db.SaveChanges();
        }
    }
}