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
        [Range(1, 1000)]
        public double PayPerArticle { get; set; }
        [Required]
        [Range(1, 1000)]
        [Display(Name = "Article Bonus")]
        public double ArticleBonus { get; set; }
        [Required]
        [Range(10000, 1000000)]
        [Display(Name = "Min PV For Bonus")]
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
