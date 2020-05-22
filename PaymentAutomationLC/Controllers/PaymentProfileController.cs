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
                PaymentProfile newPaymentProfile = new PaymentProfile(paymentProfileViewModel);
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
            PaymentProfileViewModel paymentProfileViewModel = new PaymentProfileViewModel(id, context);
            return View(paymentProfileViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PaymentProfileViewModel paymentProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                PaymentProfile.RetrieveAndEditPaymentProfile(paymentProfileViewModel, context);
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