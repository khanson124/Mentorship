using Mentorship.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Controllers
{
    public class ReviewController : Controller
    {
        private readonly MentorshipDbContext _db;

        public ReviewController(MentorshipDbContext context)
        {
            _db = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var reviews = await _db.Reviews
            .Include(r => r.Mentor)
                .ThenInclude(m => m.User)
            .Include(r => r.Mentee)
                .ThenInclude(m => m.User)
            .ToListAsync();

            return View(reviews);
        }
    }
}
