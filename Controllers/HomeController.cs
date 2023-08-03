using System.Security.Claims;
using Mentorship.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Controllers
{
    public class HomeController : Controller
    {
        private readonly MentorshipDbContext _db;

        public HomeController(MentorshipDbContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            var home = _db.Users.ToList();

            // Retrieve the user's name from the claims
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value ?? User.Identity.Name;

            // Add the user's name to the ViewBag (or you can create a ViewModel to pass the data)
            ViewBag.UserName = nameClaim;

            return View(home);
        }
    }
}
