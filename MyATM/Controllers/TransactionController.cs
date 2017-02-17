using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyATM.Models;
using Microsoft.AspNet.Identity;

namespace MyATM.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Transaction/Deposit
        [Authorize]
        public ActionResult Deposit()
        {
            return View();
        }

        // POST: Transaction/Deposit
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var applicationUserId = User.Identity.GetUserId();
                transaction.CheckingAccountId = db.CheckingAccounts.FirstOrDefault(x => x.ApplicationUserId == applicationUserId).Id;
                db.Transactions.Add(transaction);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");

            }
            return View();
        }

 
    }
}
