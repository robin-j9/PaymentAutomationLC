using Microsoft.AspNetCore.Identity;
using PaymentAutomationLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.ViewModels
{
    public class ViewUsersViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public List<IList<string>> Roles { get; set; }
    }
}
