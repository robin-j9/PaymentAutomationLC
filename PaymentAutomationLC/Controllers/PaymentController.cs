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
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        // GET: /<controller>/
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            IList<Payment> payments = _context.Payments.ToList();
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
                Payment payment = Payment.RetrieveExistingPaymentOrReturnNew(_context, newPaymentViewModel);                
                IList<Article> articles = Payment.ReadFile(newPaymentViewModel.File);
                Article.AddArticlesToDatabase(articles, payment, _context);
                _context.SaveChanges();
                return Redirect("/Payment/" + payment.MonthYear + "/Articles");
            }
            return View(newPaymentViewModel);
        }

        [Authorize(Roles = "Admin")]
        [Route("/Payment/{paymentMonthYear}/Articles")]
        public IActionResult Articles(string paymentMonthYear)
        {
            Payment payment = _context.Payments.Include(p => p.Articles)
                                     .Single(p => p.MonthYear.Equals(paymentMonthYear));
            return View(payment);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CalculatePayments(int paymentId)
        {
            Payment payment = Payment.GetById(paymentId, _context);
            payment.CalculationComplete = true;
            var articlesByWriter = payment.Articles.GroupBy(a => a.Writer);

            foreach (var group in articlesByWriter)
            {
                ApplicationUserPayment userPayment = new ApplicationUserPayment(_context, group, payment);
                ApplicationUserPayment.CalculateUserPayment(userPayment, group);

                _context.ApplicationUserPayments.Add(userPayment);
            }
            _context.SaveChanges();

            return Redirect("/Payment/Summary/" + paymentId);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Summary(int id)
        {
            IList<ApplicationUserPayment> userPayments = _context.ApplicationUserPayments
                .Include(p => p.Payment)
                .Include(p => p.ApplicationUser)
                .Include(p => p.PaymentProfile)
                .Where(p => p.PaymentId.Equals(id)).ToList();

            return View(userPayments);
        }

        [Authorize(Roles = "Admin, Employee")]
        public IActionResult UserHistory(string id)
        {
            UserHistoryViewModel userHistoryViewModel = new UserHistoryViewModel(id, _context);   
            return View(userHistoryViewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Payment paymentToDelete = new Payment { Id = id };
            _context.Payments.Remove(paymentToDelete);
            _context.SaveChanges();
            return Redirect("/Payment");
        }
    }
}
