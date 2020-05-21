using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentAutomationLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.ViewModels
{
    public class PaymentProfileViewModel
    {
        public List<SelectListItem> PaymentProfiles { get; set; }
        public int PaymentSettingID { get; set; }
        public string Name { get; set; }
        public double PayPerArticle { get; set; }
        public double ArticleBonus { get; set; }
        public int MinimumPVForBonus { get; set; }
        
        public PaymentProfileViewModel() { }
        public PaymentProfileViewModel(IEnumerable<PaymentProfile> paymentProfiles)
        {
            PaymentProfiles = new List<SelectListItem>();

            foreach (PaymentProfile profile in paymentProfiles)
            {
                PaymentProfiles.Add(new SelectListItem
                {
                    Value = profile.ID.ToString(),
                    Text = profile.Name
                });
            }
        }
    }

}
