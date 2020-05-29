using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentAutomationLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PaymentAutomationLC.ViewModels
{
    public class NewUserViewModel
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateAdded { get; set; }
        public int PaymentProfileID { get; set; }
        public List<SelectListItem> PaymentProfiles { get; set; } = new List<SelectListItem>();
        public string IdentityRoleID { get; set; }
        public List<SelectListItem> IdentityRoles { get; set; } = new List<SelectListItem>();
        public string OldRoleName { get; set; }

        public NewUserViewModel() { }
        public NewUserViewModel(IEnumerable<PaymentProfile> paymentProfiles, IEnumerable<IdentityRole> roles, [Optional]ApplicationUser userToEdit, [Optional]IdentityRole userToEditRole)
        {
            foreach(var profile in paymentProfiles)
            {
                PaymentProfiles.Add(new SelectListItem()
                {
                    Value = profile.ID.ToString(),
                    Text = profile.Name.ToString()
                });
            }

            foreach(var role in roles)
            {
                IdentityRoles.Add(new SelectListItem()
                {
                    Value = role.Id,
                    Text = role.Name
                });
            }

            if(userToEdit != null && userToEditRole != null)
            {
                UserID = userToEdit.Id;
                IdentityRoleID = userToEditRole.Id;
                FirstName = userToEdit.FirstName;
                LastName = userToEdit.LastName;
                Email = userToEdit.Email;
                PaymentProfileID = userToEdit.PaymentProfileID;
                OldRoleName = userToEditRole.Name;
            }
        }
    }
}
