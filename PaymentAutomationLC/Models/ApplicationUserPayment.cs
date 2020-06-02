using Microsoft.EntityFrameworkCore;
using PaymentAutomationLC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.Models
{
    public class ApplicationUserPayment
    {
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int PaymentID { get; set; }
        public Payment Payment { get; set; }

        public PaymentProfile PaymentProfile { get; set; }
        public int NumArticlesWithoutBonus { get; set; }
        public int NumArticlesWithBonus { get; set; }

        public double TotalPayment { get; set; }

        public ApplicationUserPayment() { }

        public ApplicationUserPayment(ApplicationDbContext context, IGrouping<string, Article> group, Payment payment)
        {
            ApplicationUser = context.Users.Include(p => p.PaymentProfile)
                    .Single(u => (u.FirstName + " " + u.LastName).Equals(group.Key));
            Payment = payment;
            PaymentProfile = ApplicationUser.PaymentProfile;
        }

        public static void CalculateUserPayment(ApplicationUserPayment userPayment, IGrouping<string, Article> group)
        {
            foreach (var article in group)
            {
                userPayment.TotalPayment += userPayment.PaymentProfile.PayPerArticle;
                if (article.PageViews >= userPayment.PaymentProfile.MinimumPVForBonus)
                {
                    userPayment.TotalPayment += userPayment.PaymentProfile.ArticleBonus;
                    userPayment.NumArticlesWithBonus++;
                }
                else userPayment.NumArticlesWithoutBonus++;
            }
        }
    }
}
