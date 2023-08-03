using Mentorship.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Controllers
{
    public class MessageController : Controller
    {
        private readonly MentorshipDbContext _db;

        public MessageController(MentorshipDbContext context)
        {
            _db = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var messages = await _db.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .ToListAsync();

            return View(messages);
        }
    }
}
