using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.ViewModels
{
    public class NewPaymentViewModel
    {
        // HOLD: need to figure out new payment form dropdown menus first
        [Required]
        [Display(Name = "Month and year for payment: (Please format as 'July 2020')")]
        public string MonthYear { get; set; }
        public IFormFile File { get; set; }
        public NewPaymentViewModel() 
        {
            
        }
    }
}
