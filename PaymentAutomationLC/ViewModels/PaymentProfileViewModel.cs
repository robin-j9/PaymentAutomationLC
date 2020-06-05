using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.ViewModels
{
    public class PaymentProfileViewModel
    {
        public int PaymentProfileId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Pay Per Article")]
        public double PayPerArticle { get; set; }
        [Required]
        [Display(Name = "Article Bonus")]
        public double ArticleBonus { get; set; }
        [Required]
        [Display(Name = "Minimum PV For Bonus")]
        public int MinimumPVForBonus { get; set; }
        
        public PaymentProfileViewModel() { }

        public PaymentProfileViewModel(int id, ApplicationDbContext context)
        {
            PaymentProfile profileToEdit = context.PaymentProfiles.Single(p => p.Id == id);
            Name = profileToEdit.Name;
            PayPerArticle = profileToEdit.PayPerArticle;
            ArticleBonus = profileToEdit.ArticleBonus;
            MinimumPVForBonus = profileToEdit.MinimumPVForBonus;
            PaymentProfileId = id;
        }
    }

}
