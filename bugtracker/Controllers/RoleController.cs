using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bugtracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace bugtracker.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            ViewBag.CurrentRoles = roles;
            return View(roles);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RoleVerwaltung(Role role) //create new Role
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = role.RoleName
            };
            IdentityResult result =  await roleManager.CreateAsync(identityRole);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(role);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost] //wieso userroles
        public async Task<IActionResult> GiveRoleToUser(List<UserRoles> model,string Id) //Parameter name "Id" has to be identical to asp-route-"Id", otherwise null
        {
            var role = await roleManager.FindByIdAsync(Id); 

            for (int i=0; i< model.Count ; i++ )
            {
                IdentityResult result = null;
                var user = await userManager.FindByIdAsync(model[i].UserId);

                if (model[i].Selected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!(model[i].Selected) && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                
                if (result.Succeeded) //why this
                {
                    if( i< model.Count - 1)
                    {
                        continue;
                    }
                    else return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index"); //wieso view -> null exc?
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditUsers(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            var users = userManager.Users;
            var model = new List<UserRoles>();

            ViewBag.CurrentRole = role;
            ViewData["Message"] = $"Edit {role.Name} Role";
            foreach (var user in userManager.Users)
            {
                var userRoles = new UserRoles
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoles.Selected = true;
                }
                else
                {
                    userRoles.Selected = false;
                }
                model.Add(userRoles);
            }
            return View(model);
        }

    }
}