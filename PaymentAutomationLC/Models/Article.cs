using System;

namespace PaymentAutomationLC.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Writer { get; set; }
        public DateTime DateWritten { get; set; }
        public string ArticleTitle { get; set; }
        public int PageViews { get; set; }
        public Payment Payment { get; set; }
        public int PaymentId { get; set; }
        
        public Article() { }
    }
}
