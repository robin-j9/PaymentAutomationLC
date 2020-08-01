using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PaymentAutomationLC.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string MonthYear { get; private set; }
        public bool CalculationComplete { get; set; }
        public IList<Article> Articles { get; set; }
        public IList<ApplicationUserPayment> ApplicationUserPayments { get; set; }

        public static IList<Article> ReadFile(IFormFile file)
        {
            var articles = new List<Article>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var headerLine = reader.ReadLine();
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var data = line.Split(new[] { ',' });
                    var article = new Article()
                    {
                        Writer = data[1],
                        DateWritten = Convert.ToDateTime(data[2]),
                        ArticleTitle = data[3],
                        PageViews = Convert.ToInt32(data[4])
                    };
                    articles.Add(article);
                }
            }
            return articles;
        }

        public static Payment RetrieveExistingPaymentOrReturnNew(ApplicationDbContext context, NewPaymentViewModel newPaymentViewModel)
        {
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            string month = mfi.GetMonthName(Int32.Parse(newPaymentViewModel.Month)).ToString();
            string monthYear = month + " " + newPaymentViewModel.Year.ToString();

            bool exists = context.Payments.Any(p => p.MonthYear == monthYear);
            Payment payment;
            if (exists)
            {
                payment = context.Payments.Single(p => p.MonthYear == monthYear);
            }
            else
            {
                payment = new Payment()
                {
                    MonthYear = monthYear
                };
                context.Payments.Add(payment);
            }
            return payment;
        }

        public static Payment GetById(int paymentId, ApplicationDbContext context)
        {
            return context.Payments.Include(p => p.Articles)
                                     .Single(p => p.Id.Equals(paymentId));
        }

        public void AddArticlesToDatabase(IList<Article> articles, ApplicationDbContext context)
        {
            foreach (Article article in articles)
            {
                article.Payment = this;
                context.Articles.Add(article);
            }
        }

    }
}
