using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileHelpers;

namespace PaymentAutomationLC.Models
{
    [DelimitedRecord(",")]
    public class Article
    {
        public int ID { get; set; }
        public string Writer { get; set; }
        [FieldConverter(ConverterKind.Date, "MM-dd-yyyy")]
        public DateTime DateWritten { get; set; }
        public string ArticleTitle { get; set; }
        public int PageViews { get; set; }
        public Payment Payment { get; set; }
        public int PaymentID { get; set; }
    }
}
