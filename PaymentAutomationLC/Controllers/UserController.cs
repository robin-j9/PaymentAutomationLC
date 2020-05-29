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
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, 
                              ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
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
                if(roles.Count == 0)
                {
                    userRoles.Add(new List<string>() { "N/A" }); ;
                }
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
            ApplicationUser userToEdit = await userManager.FindByIdAsync(id);
            IList<string> userToEditRoles = await userManager.GetRolesAsync(userToEdit);
            IdentityRole userToEditRole;
            bool noRole = userToEditRoles.Count == 0;
            if(noRole)
            {
                userToEditRole = await roleManager.FindByNameAsync("Employee");
            }
            else
            {
                userToEditRole = await roleManager.FindByNameAsync(userToEditRoles[0]);
            }
            NewUserViewModel editUserViewModel = new NewUserViewModel(context.PaymentProfiles.ToList(), context.Roles.ToList(), userToEdit, userToEditRole);
            if (noRole) editUserViewModel.OldRoleName = "N/A";
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userToEdit = await userManager.FindByIdAsync(editUserViewModel.UserID);
                IdentityRole newRole = await roleManager.FindByIdAsync(editUserViewModel.IdentityRoleID);
                if (editUserViewModel.OldRoleName == "N/A")
                {
                    await userManager.AddToRoleAsync(userToEdit, newRole.Name);
                }
                if (newRole.Name != editUserViewModel.OldRoleName && editUserViewModel.OldRoleName != "N/A")
                {
                    await userManager.RemoveFromRoleAsync(userToEdit, editUserViewModel.OldRoleName);
                }

                ApplicationUser.EditUser(userToEdit, editUserViewModel);
                await userManager.UpdateAsync(userToEdit);
                return Redirect("/User/Index");
            }
            return View(editUserViewModel);
        }
    }
}
