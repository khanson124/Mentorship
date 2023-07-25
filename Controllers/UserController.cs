using Mentorship.Data;
using Mentorship.Data.Services;
using Mentorship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Mentorship.Controllers
{
    public class UserController : Controller
    {
        private readonly MentorShipDbContext _db;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<UserController> _logger;

        public UserController(MentorShipDbContext context, IWebHostEnvironment hostEnvironment, IUserService userService, ILogger<UserController> logger)
        {
            _db = context;
            _hostEnvironment = hostEnvironment;
            _userService = userService;
            _logger = logger;

        }
        public IActionResult Index()
        {
            var user = _db.Users.ToList();
            return View(user);
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(IFormFile ProfilePicture, User user)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"UserId before adding user: {user.UserId}"); // log UserId before adding user
                if (ProfilePicture != null && ProfilePicture.Length > 0)
                {
                    // Create a new file name (guid + original extension)
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ProfilePicture.FileName)}";

                    // Full file path
                    var filePath = Path.Combine(_hostEnvironment.WebRootPath, "uploads", fileName);

                    // Save file to path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProfilePicture.CopyToAsync(stream);
                    }

                    // Update the user's profile picture path
                    user.ProfilePicture = "/uploads/" + fileName;
                }

                // Assign the current date to the user's RegistrationDate
                user.RegistrationDate = DateTime.UtcNow;

                // Add the user to the database using your service
                _userService.Add(user);

                // Redirect the user to a page after successfully creating a new User. You can adjust this as needed.
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // If model state is invalid, return the user back to the form
                return View(user);
            }
        }


    }
}
