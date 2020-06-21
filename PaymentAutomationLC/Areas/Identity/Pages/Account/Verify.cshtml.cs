using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.Models;

namespace PaymentAutomationLC.Areas.Identity.Pages.Account
{
    public class VerifyModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public VerifyModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.Users.Single(u => u.Email.Equals(Email));
                if (user != null)
                {
                    PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
                    if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, Password)
                        == PasswordVerificationResult.Success)
                    {
                        return RedirectToPage("/Account/Register", new { user.Id });
                    }
                }
            }
            return Page();
        }
    }
}
