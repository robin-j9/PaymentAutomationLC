using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.Models;
using PaymentAutomationLC.ViewModels;

namespace PaymentAutomationLC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public UserController(UserManager<ApplicationUser> userManager, 
                              ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            context = dbContext;
        }
        public async Task<IActionResult> IndexAsync()
        {
            IList<string> roles;
            List<IList<string>> userRoles = new List<IList<string>>();

            // Get list of roles for each user
            foreach(var user in userManager.Users)
            {
                roles = await userManager.GetRolesAsync(user);
                userRoles.Add(roles);
            }

            ViewUsersViewModel viewUsersViewModel = new ViewUsersViewModel
            {
                Users = context.Users.Include(u => u.PaymentProfile).ToList(),
                Roles = userRoles
            };

            return View(viewUsersViewModel);
        }

        public IActionResult New()
        {
            NewUserViewModel newUserViewModel = new NewUserViewModel(context.PaymentProfiles.ToList(), context.Roles.ToList());
            return View(newUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> NewAsync(NewUserViewModel newUserViewModel)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser(newUserViewModel, context);
 
                IdentityRole roleToAdd = context.Roles.ToList().Single(r => r.Id.ToString().Equals(newUserViewModel.IdentityRoleID.ToString()));
                await userManager.AddToRoleAsync(newUser, roleToAdd.Name);

                context.Users.Add(newUser);
                context.SaveChanges();
                return Redirect("/User/Index");
            }
            return View(newUserViewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            // TODO: CLEAN UP
            ApplicationUser userToEdit = context.Users.Single(u => u.Id.Equals(id));
            IList<string> userToEditRoles = await userManager.GetRolesAsync(userToEdit);
            IdentityRole role = context.Roles.Single(r => r.Name.Equals(userToEditRoles[0]));
            NewUserViewModel editUserViewModel = new NewUserViewModel(context.PaymentProfiles.ToList(), context.Roles.ToList())
            {
                IdentityRoleID = Int32.Parse(role.Id),
                FirstName = userToEdit.FirstName,
                LastName = userToEdit.LastName,
                Email = userToEdit.Email,
                PaymentProfileID = userToEdit.PaymentProfileID
            };
            return View(editUserViewModel);
        }
    }
}
