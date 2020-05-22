using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.Models;
using PaymentAutomationLC.ViewModels;

namespace PaymentAutomationLC.Controllers
{
    public class PaymentProfileController : Controller
    {
        private ApplicationDbContext context;

        public PaymentProfileController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<PaymentProfile> paymentProfiles = context.PaymentProfiles.ToList();
            return View(paymentProfiles);
        }

        public IActionResult New()
        {
            PaymentProfileViewModel paymentProfileViewModel = new PaymentProfileViewModel();
            return View(paymentProfileViewModel);
        }

        [HttpPost]
        public IActionResult New(PaymentProfileViewModel paymentProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                PaymentProfile newPaymentProfile = new PaymentProfile
                {
                    Name = paymentProfileViewModel.Name,
                    PayPerArticle = paymentProfileViewModel.PayPerArticle,
                    ArticleBonus = paymentProfileViewModel.ArticleBonus,
                    MinimumPVForBonus = paymentProfileViewModel.MinimumPVForBonus
                };
                context.PaymentProfiles.Add(newPaymentProfile);
                context.SaveChanges();

                return Redirect("/PaymentProfile/Index");
            }
            else
            {
                return View(paymentProfileViewModel);
            }
        }

        public IActionResult Edit(int id)
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
        public IActionResult Edit(PaymentProfileViewModel paymentProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                PaymentProfile profileToEdit = context.PaymentProfiles.Single(p => p.ID == paymentProfileViewModel.PaymentProfileID);
                profileToEdit.Name = paymentProfileViewModel.Name;
                profileToEdit.PayPerArticle = paymentProfileViewModel.PayPerArticle;
                profileToEdit.ArticleBonus = paymentProfileViewModel.ArticleBonus;
                profileToEdit.MinimumPVForBonus = paymentProfileViewModel.MinimumPVForBonus;

                context.SaveChanges();
                return Redirect("/PaymentProfile/Index");
            }
            else
            {
                return View(paymentProfileViewModel);
            }
        }
    }
}