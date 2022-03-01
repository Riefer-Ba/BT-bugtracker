using bugtracker.Data;
using bugtracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bugtracker.Controllers
{   

    public class BugController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext _database;

        public BugController(ApplicationDbContext database, UserManager<IdentityUser> userManager)
        {
            _database = database;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //getting Data from BugDatabase (Backend) to display on the website via ViewBag
            var bugs = _database.BugDatabase.ToList();
            ViewBag.CurrentBugs = bugs;

            return View();
        }

        [HttpGet]
        public IActionResult ClosedBugs()
        {
            var bugs = _database.BugDatabase.ToList();
            ViewBag.CurrentBugs = bugs;

            return View();
        }

        [HttpGet]
        public IActionResult BugHistory()
        {
            return View();
        }


        [Authorize (Roles = "User,Admin")]
        public IActionResult CreateBug(int id)
        {
            //create new Bug
            if (id == 0)
            {
                return View("BugVerwaltung");
            }

            var bug = _database.BugDatabase.Find(id);
           
            //bug not found, because off Link manipulation
            if (bug == null)
            {
                return View("NotFound");
            }

            //edit existing bug
            return View("BugVerwaltung",bug);
        }

        [HttpPost] 
        public IActionResult BugVerwaltung(Bug bug)
        {
            if (bug.Status == null) bug.Status = "open";
            if (bug.Priority == null) bug.Priority = "low";
            if (bug.Category == null) bug.Category = "other";
            if (bug.Details == null) bug.Details = "no additional information was given";

            _database.BugDatabase.Update(bug);
            _database.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize (Roles = "Admin")]
        public IActionResult DeleteBug(Bug bug)
        {
            _database.BugDatabase.Remove(bug);
            _database.SaveChanges();
            return RedirectToAction("Index"); //return View doesnt render the page, any changes made would not be shown!
        }

        public IActionResult DetailedBugOverview(int id)
        {
            var bug = _database.BugDatabase.Find(id);
            ViewData["Message"] = $" View bug: {id}" ;

            if (bug == null)
            {
                return View("NotFound");
            }

            ViewBag.CurrentBug = bug;
            return View("DetailedBugOverview", bug);
        }

        [HttpPost]
        public IActionResult AssignUserToBug(int id)
        {
            var bug = _database.BugDatabase.Find(id);
            bug.AssignedToUser = User.Identity?.Name;
            bug.Status = "in work";

            _database.BugDatabase.Update(bug);
            _database.SaveChanges();

            return RedirectToAction("DetailedBugOverview", bug); //, id?
        }


    }
}
