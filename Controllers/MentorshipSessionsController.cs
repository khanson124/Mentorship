using Mentorship.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Controllers
{
    public class MentorshipSessionsController : Controller
    {
        private readonly MentorshipDbContext _db;

        public MentorshipSessionsController(MentorshipDbContext context)
        {
            _db = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var mentorshipSessions = await _db.MentorshipSessions
            .Include(s => s.Mentor)
                .ThenInclude(m => m.User)
            .Include(s => s.Mentee)
                .ThenInclude(m => m.User)
            .ToListAsync();


            return View(mentorshipSessions);
        }
    }
}
