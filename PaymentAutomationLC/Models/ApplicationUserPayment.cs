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
        public string ApplicationUserId { get; private set; }
        public ApplicationUser ApplicationUser { get; private set; }

        public int PaymentId { get; private set; }
        public Payment Payment { get; private set; }

        public PaymentProfile PaymentProfile { get; private set; }
        public int NumArticlesWithoutBonus { get; private set; }
        public int NumArticlesWithBonus { get; private set; }

        public double TotalPayment { get; private set; }

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
