using Microsoft.AspNetCore.Identity;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAutomationLC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateAdded { get; set; }
        public int PaymentProfileID { get; set; }
        public PaymentProfile PaymentProfile { get; set; }
        public IList<ApplicationUserPayment> ApplicationUserPayments { get; set; }

        public ApplicationUser() { }

        public ApplicationUser(NewUserViewModel newUserViewModel, ApplicationDbContext context)
        {
            FirstName = newUserViewModel.FirstName;
            LastName = newUserViewModel.LastName;
            Email = newUserViewModel.Email;
            DateAdded = newUserViewModel.DateAdded;
            PaymentProfile = context.PaymentProfiles.Single(p => p.ID.Equals(newUserViewModel.PaymentProfileID));
        }

        public static void EditUser(ApplicationUser userToEdit, NewUserViewModel editUserViewModel)
        {
            userToEdit.FirstName = editUserViewModel.FirstName;
            userToEdit.LastName = editUserViewModel.LastName;
            userToEdit.Email = editUserViewModel.Email;
            userToEdit.PaymentProfileID = editUserViewModel.PaymentProfileID;
        }
    }
}
