using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.ViewModels
{
    public class PaymentProfileViewModel
    {
        public int PaymentProfileID { get; set; }
        public string Name { get; set; }
        public double PayPerArticle { get; set; }
        public double ArticleBonus { get; set; }
        public int MinimumPVForBonus { get; set; }
        
        public PaymentProfileViewModel() { }

        public PaymentProfileViewModel(int id, ApplicationDbContext context)
        {
            PaymentProfile profileToEdit = context.PaymentProfiles.Single(p => p.ID == id);
            Name = profileToEdit.Name;
            PayPerArticle = profileToEdit.PayPerArticle;
            ArticleBonus = profileToEdit.ArticleBonus;
            MinimumPVForBonus = profileToEdit.MinimumPVForBonus;
            PaymentProfileID = id;
        }
    }

}
