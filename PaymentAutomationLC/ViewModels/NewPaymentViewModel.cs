using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.ViewModels
{
    public class NewPaymentViewModel
    {
        // HOLD: need to figure out new payment form dropdown menus first
        [Required(ErrorMessage = "You must enter a payment name (payment month and year).")]
        [Display(Name = "Month and year for payment: (Please format as 'July 2020')")]
        public string MonthYear { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public List<SelectListItem> Months { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Years { get; set; } = new List<SelectListItem>();
       
        [Required]
        public IFormFile File { get; set; }
        
        public NewPaymentViewModel() 
        {
            Month = DateTime.Now.Month.ToString();

            for (var i = 1; i <= 12; i++)
            {
                var months = DateTimeFormatInfo.InvariantInfo.MonthNames;
                Months.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = months[i-1].ToString()
                });
            }

            for (var i = DateTime.Now.Year; i >= 2015; i--)
            {
                Years.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }
        }

    }
}
