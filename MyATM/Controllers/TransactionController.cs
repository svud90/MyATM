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
                transaction.CheckingAccountId = db.CheckingAccounts.FirstOrDefault(x => x.ApplicationUserId == applicationUserId).Id;

                db.Transactions.Add(transaction);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Withdrawal(Transaction transaction)
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
