using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mentorship.Data;
using Mentorship.Models;
using Mentorship.Data.Services;

namespace Mentorship.Controllers
{
    public class MentorMenteeAssignmentsController : Controller
    {
        private readonly MentorshipDbContext _db;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<UserController> _logger;

        public MentorMenteeAssignmentsController(MentorshipDbContext context, IWebHostEnvironment hostEnvironment, IUserService userService, ILogger<UserController> logger)
        {
            _db = context;
        }

        // GET: MentorMenteeAssignments
        public async Task<IActionResult> Index()
        {
            var mentorshipDbContext = _db.MentorMenteeAssignments.Include(m => m.Mentee).Include(m => m.Mentor);
            return View(await mentorshipDbContext.ToListAsync());
        }

        // GET: MentorMenteeAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.MentorMenteeAssignments == null)
            {
                return NotFound();
            }

            var mentorMenteeAssignment = await _db.MentorMenteeAssignments
                .Include(m => m.Mentee)
                .Include(m => m.Mentor)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (mentorMenteeAssignment == null)
            {
                return NotFound();
            }

            return View(mentorMenteeAssignment);
        }

        // GET: MentorMenteeAssignments/Create
        public IActionResult Create()
        {
            ViewData["MenteeId"] = new SelectList(_db.Mentees, "MenteeId", "MenteeId");
            ViewData["MentorId"] = new SelectList(_db.Mentors, "MentorId", "MentorId");
            return View();
        }

        // POST: MentorMenteeAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssignmentId,MentorId,MenteeId,AssignedDate")] MentorMenteeAssignment mentorMenteeAssignment)
        {
            if (ModelState.IsValid)
            {
                _db.Add(mentorMenteeAssignment);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenteeId"] = new SelectList(_db.Mentees, "MenteeId", "MenteeId", mentorMenteeAssignment.MenteeId);
            ViewData["MentorId"] = new SelectList(_db.Mentors, "MentorId", "MentorId", mentorMenteeAssignment.MentorId);
            return View(mentorMenteeAssignment);
        }

        // GET: MentorMenteeAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.MentorMenteeAssignments == null)
            {
                return NotFound();
            }

            var mentorMenteeAssignment = await _db.MentorMenteeAssignments.FindAsync(id);
            if (mentorMenteeAssignment == null)
            {
                return NotFound();
            }
            ViewData["MenteeId"] = new SelectList(_db.Mentees, "MenteeId", "MenteeId", mentorMenteeAssignment.MenteeId);
            ViewData["MentorId"] = new SelectList(_db.Mentors, "MentorId", "MentorId", mentorMenteeAssignment.MentorId);
            return View(mentorMenteeAssignment);
        }

        // POST: MentorMenteeAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssignmentId,MentorId,MenteeId,AssignedDate")] MentorMenteeAssignment mentorMenteeAssignment)
        {
            if (id != mentorMenteeAssignment.AssignmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(mentorMenteeAssignment);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MentorMenteeAssignmentExists(mentorMenteeAssignment.AssignmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenteeId"] = new SelectList(_db.Mentees, "MenteeId", "MenteeId", mentorMenteeAssignment.MenteeId);
            ViewData["MentorId"] = new SelectList(_db.Mentors, "MentorId", "MentorId", mentorMenteeAssignment.MentorId);
            return View(mentorMenteeAssignment);
        }

        // GET: MentorMenteeAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.MentorMenteeAssignments == null)
            {
                return NotFound();
            }

            var mentorMenteeAssignment = await _db.MentorMenteeAssignments
                .Include(m => m.Mentee)
                .Include(m => m.Mentor)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (mentorMenteeAssignment == null)
            {
                return NotFound();
            }

            return View(mentorMenteeAssignment);
        }

        // POST: MentorMenteeAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.MentorMenteeAssignments == null)
            {
                return Problem("Entity set 'MentorshipDbContext.MentorMenteeAssignments'  is null.");
            }
            var mentorMenteeAssignment = await _db.MentorMenteeAssignments.FindAsync(id);
            if (mentorMenteeAssignment != null)
            {
                _db.MentorMenteeAssignments.Remove(mentorMenteeAssignment);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MentorMenteeAssignmentExists(int id)
        {
          return (_db.MentorMenteeAssignments?.Any(e => e.AssignmentId == id)).GetValueOrDefault();
        }
    }
}
