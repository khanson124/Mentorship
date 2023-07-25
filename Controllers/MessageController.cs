using Mentorship.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Controllers
{
    public class MessageController : Controller
    {
        private readonly MentorShipDbContext _db;

        public MessageController(MentorShipDbContext context)
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
