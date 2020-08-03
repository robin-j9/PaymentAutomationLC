using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.Models;
using PaymentAutomationLC.ViewModels;

namespace PaymentAutomationLC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserController(UserManager<ApplicationUser> userManager, 
                              RoleManager<IdentityRole> roleManager, 
                              ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = dbContext;
        }

        public async Task<IActionResult> IndexAsync()
        {
            IList<string> roles;
            List<IList<string>> userRoles = new List<IList<string>>();

            // Get list of roles for each user
            foreach(var user in _userManager.Users)
            {
                roles = await _userManager.GetRolesAsync(user);
                if(roles.Count == 0)
                {
                    roles = new List<string>() { "N/A" }; 
                }
                userRoles.Add(roles);
            }

            ViewUsersViewModel viewUsersViewModel = new ViewUsersViewModel
            {
                Users = _context.Users.Include(u => u.PaymentProfile).ToList(),
                Roles = userRoles
            };

            return View(viewUsersViewModel);
        }

        public IActionResult New()
        {
            NewUserViewModel newUserViewModel = new NewUserViewModel(_context.PaymentProfiles.ToList(), 
                                                                     _context.Roles.ToList());
            return View(newUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> NewAsync(NewUserViewModel newUserViewModel)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser(newUserViewModel, _context);

                IdentityRole roleToAdd = await _roleManager.FindByIdAsync(newUserViewModel.IdentityRoleId);
                await _userManager.AddToRoleAsync(newUser, roleToAdd.Name);

                // TODO: Change so initial PW is not hard-coded
                await _userManager.AddPasswordAsync(newUser, "Password0!");

                _context.Users.Add(newUser);
                _context.SaveChanges();
                return Redirect("/User/Index");
            }
            NewUserViewModel.PopulateDropdowns(_context.PaymentProfiles.ToList(),
                                               _context.Roles.ToList(),
                                               newUserViewModel);
            return View(newUserViewModel);
        }

        public async Task<IActionResult> EditAsync(string id)
        {
            // TODO: CLEAN UP
            ApplicationUser userToEdit = await _userManager.FindByIdAsync(id);
            IList<string> userToEditRoles = await _userManager.GetRolesAsync(userToEdit);
            IdentityRole userToEditRole;
            bool noRole = userToEditRoles.Count == 0;

            if (noRole) 
                userToEditRole = await _roleManager.FindByNameAsync("Employee");
            else 
                userToEditRole = await _roleManager.FindByNameAsync(userToEditRoles[0]);

            NewUserViewModel editUserViewModel = new NewUserViewModel(_context.PaymentProfiles.ToList(), 
                _context.Roles.ToList(), userToEdit, userToEditRole);
            if (noRole) editUserViewModel.OldRoleName = "N/A";
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(NewUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userToEdit = await _userManager.FindByIdAsync(editUserViewModel.UserId);
                IdentityRole newRole = await _roleManager.FindByIdAsync(editUserViewModel.IdentityRoleId);

                if (newRole.Name != editUserViewModel.OldRoleName)
                    await _userManager.AddToRoleAsync(userToEdit, newRole.Name);

                if (editUserViewModel.OldRoleName != "N/A" && editUserViewModel.OldRoleName != newRole.Name)
                    await _userManager.RemoveFromRoleAsync(userToEdit, editUserViewModel.OldRoleName);

                ApplicationUser.EditUser(userToEdit, editUserViewModel);
                await _userManager.UpdateAsync(userToEdit);
                return Redirect("/User/Index");
            }
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            ApplicationUser userToDelete = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(userToDelete);
            return Redirect("/User/Index");
        }
    }
}
