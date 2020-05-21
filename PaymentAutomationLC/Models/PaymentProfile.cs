using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.Models
{
    public class PaymentProfile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double PayPerArticle { get; set; }
        public double ArticleBonus { get; set; }
        public int MinimumPVForBonus { get; set; }
    }
}
