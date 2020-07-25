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
        public int PaymentProfileId { get; set; }
        public PaymentProfile PaymentProfile { get; set; }
        public IList<ApplicationUserPayment> ApplicationUserPayments { get; set; }

        public ApplicationUser() { }

        public ApplicationUser(NewUserViewModel newUserViewModel, ApplicationDbContext context)
        {
            FirstName = newUserViewModel.FirstName;
            LastName = newUserViewModel.LastName;
            Email = newUserViewModel.Email;
            DateAdded = newUserViewModel.DateAdded;
            PaymentProfile = context.PaymentProfiles.Single(p => p.Id.Equals(newUserViewModel.PaymentProfileId));
        }

        public static void EditUser(ApplicationUser userToEdit, NewUserViewModel editUserViewModel)
        {
            userToEdit.FirstName = editUserViewModel.FirstName;
            userToEdit.LastName = editUserViewModel.LastName;
            userToEdit.Email = editUserViewModel.Email;
            userToEdit.PaymentProfileId = editUserViewModel.PaymentProfileId;

            userToEdit.UserName = editUserViewModel.Email.ToLower();
            userToEdit.NormalizedUserName = editUserViewModel.Email.ToUpper();
            userToEdit.NormalizedEmail = editUserViewModel.Email.ToUpper();
        }
    }
}
