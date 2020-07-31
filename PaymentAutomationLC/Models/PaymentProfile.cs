using PaymentAutomationLC.Data;
using PaymentAutomationLC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.Models
{
    public class PaymentProfile
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public double PayPerArticle { get; private set; }
        public double ArticleBonus { get; private set; }
        public int MinimumPVForBonus { get; private set; }

        public PaymentProfile() { }

        public PaymentProfile(PaymentProfileViewModel paymentProfileViewModel)
        {
            Name = paymentProfileViewModel.Name;
            PayPerArticle = paymentProfileViewModel.PayPerArticle;
            ArticleBonus = paymentProfileViewModel.ArticleBonus;
            MinimumPVForBonus = paymentProfileViewModel.MinimumPVForBonus;
        }

        public static void RetrieveAndEditPaymentProfile(PaymentProfileViewModel paymentProfileViewModel, ApplicationDbContext context)
        {
            PaymentProfile profileToEdit = context.PaymentProfiles.Single(p => p.Id == paymentProfileViewModel.PaymentProfileId);
            profileToEdit.Name = paymentProfileViewModel.Name;
            profileToEdit.PayPerArticle = paymentProfileViewModel.PayPerArticle;
            profileToEdit.ArticleBonus = paymentProfileViewModel.ArticleBonus;
            profileToEdit.MinimumPVForBonus = paymentProfileViewModel.MinimumPVForBonus;
        }
    }
}
