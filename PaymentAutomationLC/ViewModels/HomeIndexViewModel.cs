using PaymentAutomationLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.ViewModels
{
    public class HomeIndexViewModel
    {
        public ApplicationUser User { get; set; }
        public string Role { get; set; }

        public HomeIndexViewModel() { }
        public HomeIndexViewModel(ApplicationUser user, IList<string> roles)
        {
            User = user;
            Role = roles[0];
        }
    }
}
