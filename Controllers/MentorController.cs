using Mentorship.Data;
using Mentorship.Data.Services;
using Mentorship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Controllers
{
    public class MentorController : Controller
    {
        private readonly IMentorService _service;

        public MentorController(IMentorService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var mentors = await _service.GetAllMentors();
            return View(mentors);
        }

        //Get: Mentors/Create
        public IActionResult Create()
        {
            return View();
        }
        /*       [HttpPost]
               public async Task<IActionResult> Create(Mentor mentor)
               {
                   if (!ModelState.IsValid)
                   {
                       return View(mentor);
                   }

                   // Assuming _db is your DbContext
                   _service.Mentors.Add(mentor);
                   await _db.SaveChangesAsync();

                   return RedirectToAction(nameof(Index));
               }*/


    }
}
