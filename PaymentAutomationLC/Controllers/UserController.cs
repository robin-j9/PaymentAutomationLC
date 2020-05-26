using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(UserManager<ApplicationUser> userManager, 
                              RoleManager<IdentityRole> roleManager,
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

            foreach(var user in userManager.Users)
            {
                roles = await userManager.GetRolesAsync(user);
                userRoles.Add(roles);
            }

            ViewUsersViewModel viewUsersViewModel = new ViewUsersViewModel
            {
                Users = userManager.Users,
                Roles = userRoles
            };

            return View(viewUsersViewModel);
        }

        public IActionResult New()
        {
            NewUserViewModel newUserViewModel = new NewUserViewModel(context.PaymentProfiles.ToList(), roleManager.Roles);
            return View(newUserViewModel);
        }

        [HttpPost]
        public Task<IActionResult> NewAsync(NewUserViewModel newUserViewModel)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser
                {
                    FirstName = newUserViewModel.FirstName,
                    LastName = newUserViewModel.LastName,
                    Email = newUserViewModel.Email,
                    DateAdded = newUserViewModel.DateAdded,
                    PaymentProfile = context.PaymentProfiles.Single(p => p.ID == newUserViewModel.PaymentProfileID)
                };
                context.Users.Add(newUser);
                context.SaveChanges();
                Task<IdentityRole> roleToAdd = roleManager.FindByIdAsync(newUserViewModel.IdentityRoleID.ToString());
                userManager.AddToRoleAsync(newUser, roleToAdd.ToString());
            }
            return NewAsync(newUserViewModel);
        }
    }
}