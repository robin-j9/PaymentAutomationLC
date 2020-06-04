using Microsoft.EntityFrameworkCore;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.ViewModels
{
    public class UserHistoryViewModel
    {
        public IList<ApplicationUserPayment> UserPayments { get; set; } = new List<ApplicationUserPayment>();
        public ApplicationUser User { get; set; }

        public UserHistoryViewModel() { }

        public UserHistoryViewModel(string id, ApplicationDbContext context)
        {
            UserPayments = context.ApplicationUserPayments
                .Include(p => p.Payment)
                .Include(p => p.ApplicationUser)
                .Include(p => p.PaymentProfile)
                .Where(p => p.ApplicationUserId.Equals(id)).ToList();
            User = context.Users.Single(u => u.Id.Equals(id));
        }
    }
}
