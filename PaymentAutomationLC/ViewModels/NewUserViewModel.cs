using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentAutomationLC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PaymentAutomationLC.ViewModels
{
    public class NewUserViewModel
    {
        public string UserId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public DateTime DateAdded { get; set; }
        [Display(Name = "Payment Profile")]
        public int PaymentProfileId { get; set; }
        public List<SelectListItem> PaymentProfiles { get; set; } = new List<SelectListItem>();
        [Display(Name = "Role")]
        public string IdentityRoleId { get; set; }
        public List<SelectListItem> IdentityRoles { get; set; } = new List<SelectListItem>();
        public string OldRoleName { get; set; }

        public NewUserViewModel() { }
        public NewUserViewModel(IEnumerable<PaymentProfile> paymentProfiles, IEnumerable<IdentityRole> roles, [Optional]ApplicationUser userToEdit, [Optional]IdentityRole userToEditRole)
        {
            PopulateDropdowns(paymentProfiles, roles, this);

            if(userToEdit != null && userToEditRole != null)
            {
                UserId = userToEdit.Id;
                IdentityRoleId = userToEditRole.Id;
                FirstName = userToEdit.FirstName;
                LastName = userToEdit.LastName;
                Email = userToEdit.Email;
                PaymentProfileId = userToEdit.PaymentProfileId;
                OldRoleName = userToEditRole.Name;
            }
        }

        public static void PopulateDropdowns(IEnumerable<PaymentProfile> paymentProfiles, IEnumerable<IdentityRole> roles, NewUserViewModel newUserViewModel)
        {
            foreach (var profile in paymentProfiles)
            {
                newUserViewModel.PaymentProfiles.Add(new SelectListItem()
                {
                    Value = profile.Id.ToString(),
                    Text = profile.Name.ToString()
                });
            }

            foreach (var role in roles)
            {
                newUserViewModel.IdentityRoles.Add(new SelectListItem()
                {
                    Value = role.Id,
                    Text = role.Name
                });
            }
        }
    }
}
