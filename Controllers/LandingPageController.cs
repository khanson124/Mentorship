using Mentorship.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mentorship.Controllers
{
    public class LandingPageController: Controller
    {
        private readonly MentorshipDbContext _db;

        public LandingPageController(MentorshipDbContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {

            return View();
        }
    }

}
