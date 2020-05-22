using FileHelpers;
using Microsoft.AspNetCore.Http;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAutomationLC.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public string MonthYear { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public PaymentProfile PaymentProfile { get; set; }

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
            bool exists = context.Payments.Any(p => p.MonthYear == newPaymentViewModel.MonthYear);
            Payment payment;
            if (exists)
            {
                payment = context.Payments.Single(p => p.MonthYear == newPaymentViewModel.MonthYear);
            }
            else
            {
                payment = new Payment()
                {
                    MonthYear = newPaymentViewModel.MonthYear
                };
            }
            return payment;
        }
    }
}
