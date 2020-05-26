using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaymentAutomationLC.Models;
using PaymentAutomationLC.ViewModels;

namespace PaymentAutomationLC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
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
    }
}