using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyATM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MyATM.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Transaction/Deposit
        [Authorize]
        public ActionResult Deposit()
        {
            var applicationUserId = User.Identity.GetUserId();
            //var Pin = db.Users.Where(x => x.Id == applicationUserId).First().PIN;
            //var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var user = manager.FindById(applicationUserId);
            //ViewBag.Pin = user.PIN;
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
                var checkingAccount = db.CheckingAccounts.FirstOrDefault(x => x.ApplicationUserId == applicationUserId);

                transaction.CheckingAccountId = checkingAccount.Id;
                transaction.Description = DateTime.Now.ToString("G") + ": Deposit";
                checkingAccount.Balance += transaction.Amount;

                db.Transactions.Add(transaction);
                db.SaveChanges();

                return PartialView("_SuccessfulTransaction", transaction);

            }
            return View();
        }

        [HttpGet]
        public ActionResult Withdrawal()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Withdrawal(Transaction transaction)
        {
            var applicationUserId = User.Identity.GetUserId();
            var checkingAccount = db.CheckingAccounts.FirstOrDefault(x => x.ApplicationUserId == applicationUserId);
            if (checkingAccount.Balance < transaction.Amount)
                ModelState.AddModelError("Amount", "Insufficient!");
            if (ModelState.IsValid)
            {

                transaction.CheckingAccountId = checkingAccount.Id;
                transaction.Amount = -transaction.Amount;
                transaction.Description = DateTime.Now.ToString("G") + ": Withdrawal";

                checkingAccount.Balance += transaction.Amount;

                db.Transactions.Add(transaction);
                db.SaveChanges();

                transaction.Amount = -transaction.Amount;
                return PartialView("_SuccessfulTransaction", transaction);

            }
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult QuickCash100()
        {
            var applicationUserId = User.Identity.GetUserId();
            var checkingAccount = db.CheckingAccounts.FirstOrDefault(x => x.ApplicationUserId == applicationUserId);
            if (checkingAccount.Balance < 100)
                ModelState.AddModelError("Amount", "Insufficient!");
            if (ModelState.IsValid)
            {
                var transaction = new Transaction();
                transaction.CheckingAccountId = checkingAccount.Id;
                transaction.Amount = -100;
                transaction.Description = DateTime.Now.ToString("G") + ": Withdrawal";

                checkingAccount.Balance += transaction.Amount;

                db.Transactions.Add(transaction);
                db.SaveChanges();

                transaction.Amount = 100;
                return View("SuccessfulTransaction", transaction);

            }
            return View("Withdrawal");
        }

        [HttpGet]
        public ActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(TransferTransaction transferTransaction)
        {
            var applicationUserId = User.Identity.GetUserId();
            var checkingAccount = db.CheckingAccounts.FirstOrDefault(x => x.ApplicationUserId == applicationUserId);
            if (checkingAccount.Balance < transferTransaction.Amount)
                ModelState.AddModelError("Amount", "Insufficient!");
            if (ModelState.IsValid)
            {
                var transaction = new Transaction();
                transaction.CheckingAccountId = checkingAccount.Id;
                transaction.Amount = -transferTransaction.Amount;
                transaction.Description = DateTime.Now.ToString("G") + ": Transfer to Account #"+ transferTransaction.RecievingCheckingAccountNumber ;

                checkingAccount.Balance += transaction.Amount;

                db.Transactions.Add(transaction);
                db.SaveChanges();

                transaction.Amount = -transaction.Amount;
                return PartialView("_SuccessfulTransaction", transaction);

            }
            return View();
        }
    }
}
