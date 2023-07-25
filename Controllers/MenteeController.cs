using Mentorship.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Controllers
{
    public class MenteeController : Controller
    {
        private readonly MentorShipDbContext _db;

        public MenteeController(MentorShipDbContext context)
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
