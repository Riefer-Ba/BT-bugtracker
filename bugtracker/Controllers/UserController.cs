using bugtracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bugtracker.Controllers
{
    public class UserController : Controller
    {
        private UserManager<IdentityUser> userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User,Admin")]

        public async Task<IActionResult> UserOverview(string Id) // this is managing user over the roleController, consider new Controller
        {
            var user = await userManager.FindByIdAsync(Id);

            if(user == null)
            {
                user = await userManager.FindByNameAsync(Id);
            }

            ViewData["Message"] = $" View User - {user.UserName}";

            if (user == null)
            {
                return View("NotFound");
            }

            ViewBag.CurrentUser = user;

            return View("UserOverview");
        }
    }
}
