using Mentorship.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Controllers
{
    public class MenteeController : Controller
    {
        private readonly MentorshipDbContext _db;

        public MenteeController(MentorshipDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            var mentee = _db.Mentees.Include(m => m.User).ToList();
            return View(mentee);
        }
    }
}
