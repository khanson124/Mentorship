using Mentorship.Data;
using Mentorship.Data.Services;
using Mentorship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Data.Entity;


namespace Mentorship.Controllers
{
    public class MentorController : Controller
    {
        private readonly MentorshipDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<UserController> _logger;
        private readonly IMentorService _service;

        public MentorController(IMentorService service, MentorshipDbContext context, IWebHostEnvironment hostEnvironment, ILogger<UserController> logger)
        {
            _service = service;
            _db = context;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var mentors = await _service.GetAllMentors();
            return View(mentors);
        }

        // GET: /Mentor/CreateMentor
        public IActionResult CreateMentor()
        {
            return View();
        }

        // POST: /Mentor/CreateMentor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMentor([Bind("bio,area_of_expertise,hourly_rate,availability,user_id")] Mentor mentor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Add(mentor);
                    _db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // handle error
                    Console.WriteLine(ex.Message);
                }
            }
            return View(mentor);
        }


    }
}
