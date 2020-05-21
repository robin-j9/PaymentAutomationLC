using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FileHelpers;
using PaymentAutomationLC.Models;
using PaymentAutomationLC.Data;
using System.Diagnostics;
using PaymentAutomationLC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentAutomationLC.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private ApplicationDbContext context;

        public PaymentController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            NewPaymentViewModel newPaymentViewModel = new NewPaymentViewModel();
            return View(newPaymentViewModel);
        }

        [HttpPost]
        public IActionResult New(NewPaymentViewModel newPaymentViewModel)
        {
            if (ModelState.IsValid)
            {
                bool exists = context.Payments.Any(p => p.MonthYear == newPaymentViewModel.MonthYear);
                Payment payment;
                if (exists)
                {
                    payment = context.Payments.Single(p => p.MonthYear == newPaymentViewModel.MonthYear);
                }
                else
                {
                    payment = new Payment()
                    {
                        MonthYear = newPaymentViewModel.MonthYear
                    };
                    context.Payments.Add(payment);
                    context.SaveChanges();
                }
                
                IList<Article> articles = Payment.ReadFile(newPaymentViewModel.File);
                foreach(Article article in articles)
                {
                    context.Articles.Add(new Article()
                    {
                        Writer = article.Writer,
                        DateWritten = article.DateWritten,
                        ArticleTitle = article.ArticleTitle,
                        PageViews = article.PageViews,
                        Payment = payment
                    });
                }
                context.SaveChanges();
            }
            return View();
        }

        public IActionResult Profiles()
        {
            List<PaymentProfile> paymentProfiles = context.PaymentProfiles.ToList();
            return View(paymentProfiles);
        }
    }
}
