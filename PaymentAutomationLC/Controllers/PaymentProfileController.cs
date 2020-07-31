using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.Models;
using PaymentAutomationLC.ViewModels;

namespace PaymentAutomationLC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PaymentProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentProfileController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            List<PaymentProfile> paymentProfiles = _context.PaymentProfiles.ToList();
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
                _context.PaymentProfiles.Add(newPaymentProfile);
                _context.SaveChanges();
                return Redirect("/PaymentProfile/Index");
            }
            else
            {
                return View(paymentProfileViewModel);
            }
        }

        public IActionResult Edit(int id)
        {
            PaymentProfileViewModel paymentProfileViewModel = new PaymentProfileViewModel(id, _context);
            return View(paymentProfileViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PaymentProfileViewModel paymentProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                PaymentProfile.RetrieveAndEditPaymentProfile(paymentProfileViewModel, _context);
                _context.SaveChanges();
                return Redirect("/PaymentProfile/Index");
            }
            else
            {
                return View(paymentProfileViewModel);
            }
        }
    }
}