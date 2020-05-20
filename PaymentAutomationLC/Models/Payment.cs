using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public DateTime DatePosted { get; set; }
        public IEnumerable<Article> Articles { get; set; } 
    }
}
