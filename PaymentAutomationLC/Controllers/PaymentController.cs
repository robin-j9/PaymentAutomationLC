using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentAutomationLC.Models;
using PaymentAutomationLC.Data;
using System.Diagnostics;
using PaymentAutomationLC.ViewModels;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentAutomationLC.Controllers
{
    public class PaymentController : Controller
    {
        private ApplicationDbContext context;

        public PaymentController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            IList<Payment> payments = context.Payments.ToList();
            return View(payments);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult New()
        {
            NewPaymentViewModel newPaymentViewModel = new NewPaymentViewModel();
            return View(newPaymentViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult New(NewPaymentViewModel newPaymentViewModel)
        {
            if (ModelState.IsValid)
            {
                Payment payment = Payment.RetrieveExistingPaymentOrReturnNew(context, newPaymentViewModel);
                //TODO: DEAD CODE?
                context.SaveChanges();
                
                IList<Article> articles = Payment.ReadFile(newPaymentViewModel.File);
                Article.AddArticlesToDatabase(articles, payment, context);
                context.SaveChanges();
                return Redirect("/Payment/" + payment.MonthYear + "/Articles");
            }
            return View(newPaymentViewModel);
        }

        [Authorize(Roles = "Admin")]
        [Route("/Payment/{paymentMonthYear}/Articles")]
        public IActionResult Articles(string paymentMonthYear)
        {
            Payment payment = context.Payments.Include(p => p.Articles)
                                     .Single(p => p.MonthYear.Equals(paymentMonthYear));
            return View(payment);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CalculatePayments(int paymentId)
        {
            Payment payment = Payment.GetById(paymentId, context);
            payment.CalculationComplete = true;
            var articlesByWriter = payment.Articles.GroupBy(a => a.Writer);

            foreach (var group in articlesByWriter)
            {
                ApplicationUserPayment userPayment = new ApplicationUserPayment(context, group, payment);
                ApplicationUserPayment.CalculateUserPayment(userPayment, group);

                context.ApplicationUserPayments.Add(userPayment);
            }
            context.SaveChanges();

            return Redirect("/Payment/Summary/" + paymentId);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Summary(int id)
        {
            IList<ApplicationUserPayment> userPayments = context.ApplicationUserPayments
                .Include(p => p.Payment)
                .Include(p => p.ApplicationUser)
                .Include(p => p.PaymentProfile)
                .Where(p => p.PaymentId.Equals(id)).ToList();

            return View(userPayments);
        }

        [Authorize(Roles = "Admin, Employee")]
        public IActionResult UserHistory(string id)
        {
            UserHistoryViewModel userHistoryViewModel = new UserHistoryViewModel(id, context);   
            return View(userHistoryViewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Payment paymentToDelete = new Payment { Id = id };
            context.Payments.Remove(paymentToDelete);
            context.SaveChanges();
            return Redirect("/Payment");
        }
    }
}
