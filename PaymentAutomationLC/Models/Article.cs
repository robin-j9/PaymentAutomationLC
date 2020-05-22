using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileHelpers;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.ViewModels;

namespace PaymentAutomationLC.Models
{
    public class Article
    {
        public int ID { get; set; }
        public string Writer { get; set; }
        public DateTime DateWritten { get; set; }
        public string ArticleTitle { get; set; }
        public int PageViews { get; set; }
        public Payment Payment { get; set; }
        public int PaymentID { get; set; }
        
        public Article() { }

        public static void AddArticlesToDatabase(IList<Article> articles, Payment payment, ApplicationDbContext context)
        {
            foreach (Article article in articles)
            {
                article.Payment = payment;
                context.Articles.Add(article);
            }
        }
    }
}
