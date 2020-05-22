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
                Payment payment = Payment.RetrieveExistingPaymentOrReturnNew(context, newPaymentViewModel);    
                context.Payments.Add(payment);
                context.SaveChanges();
                
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

        public IActionResult AddProfile()
        {
            PaymentProfileViewModel paymentProfileViewModel = new PaymentProfileViewModel();
            return View(paymentProfileViewModel);
        }

        [HttpPost]
        public IActionResult AddProfile(PaymentProfileViewModel paymentProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                PaymentProfile newPaymentProfile = new PaymentProfile{
                    Name = paymentProfileViewModel.Name,
                    PayPerArticle = paymentProfileViewModel.PayPerArticle,
                    ArticleBonus = paymentProfileViewModel.ArticleBonus,
                    MinimumPVForBonus = paymentProfileViewModel.MinimumPVForBonus
                };
                context.PaymentProfiles.Add(newPaymentProfile);
                context.SaveChanges();

                return Redirect("/Payment/Profiles");
            }
            else
            {
                return View(paymentProfileViewModel);
            }
        }

        public IActionResult EditProfile(int id)
        {
            PaymentProfile profileToEdit = context.PaymentProfiles.Single(p => p.ID == id);
            PaymentProfileViewModel paymentProfileViewModel = new PaymentProfileViewModel
            {
                Name = profileToEdit.Name,
                PayPerArticle = profileToEdit.PayPerArticle,
                ArticleBonus = profileToEdit.ArticleBonus,
                MinimumPVForBonus = profileToEdit.MinimumPVForBonus,
                PaymentProfileID = id
            };
            return View(paymentProfileViewModel);
        }

        [HttpPost]
        public IActionResult EditProfile(PaymentProfileViewModel paymentProfileViewModel)
        {
            if(ModelState.IsValid)
            {
                PaymentProfile profileToEdit = context.PaymentProfiles.Single(p => p.ID == paymentProfileViewModel.PaymentProfileID);
                profileToEdit.Name = paymentProfileViewModel.Name;
                profileToEdit.PayPerArticle = paymentProfileViewModel.PayPerArticle;
                profileToEdit.ArticleBonus = paymentProfileViewModel.ArticleBonus;
                profileToEdit.MinimumPVForBonus = paymentProfileViewModel.MinimumPVForBonus;

                context.SaveChanges();
                return Redirect("/Payment/Profiles");
            } 
            else
            {
                return View(paymentProfileViewModel);
            }
        }
    }
}
