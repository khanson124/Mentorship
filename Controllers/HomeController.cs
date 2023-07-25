using Mentorship.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Controllers
{
    public class HomeController : Controller
    {
        private readonly MentorShipDbContext _db;

        public HomeController(MentorShipDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            var home = _db.Users.ToList();
            return View(home);
        }
    }
}
