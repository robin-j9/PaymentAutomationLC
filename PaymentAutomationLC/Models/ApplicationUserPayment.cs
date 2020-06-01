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
        public double TotalPayment { get; set; }
    }
}
