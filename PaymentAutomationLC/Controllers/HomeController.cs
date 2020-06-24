using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentAutomationLC.Data;
using PaymentAutomationLC.Models;
using PaymentAutomationLC.ViewModels;

namespace PaymentAutomationLC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            context = dbContext;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = context.Users.Single(u => User.Identity.Name.Equals(u.UserName));
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel(user, userRoles);
            return View(homeIndexViewModel);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
