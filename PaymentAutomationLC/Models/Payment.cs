using FileHelpers;
using Microsoft.AspNetCore.Http;
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
        public PaymentSettings PaymentSettings { get; set; }

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
    }
}
