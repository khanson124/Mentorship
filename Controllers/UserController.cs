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
        private readonly MentorshipDbContext _db;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<UserController> _logger;

        public UserController(MentorshipDbContext context, IWebHostEnvironment hostEnvironment, IUserService userService, ILogger<UserController> logger)
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
                _userService.AddAsync(user);

                // Redirect the user to a page after successfully creating a new User. You can adjust this as needed.
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // If model state is invalid, return the user back to the form
                return View(user);
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            //var userDetails = await _userService.GetUserByIdAsync(id);
            var userDetails = _userService.GetUserById(id);
            if (userDetails == null) return View("Not found");
            return View(userDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, IFormFile ProfilePicture, User user)
        {
            if (ModelState.IsValid)
            {
                // Fetch the user from the database
                //var dbUser = await _userService.GetUserByIdAsync(id);
                var dbUser =  _userService.GetUserById(id);
                if (dbUser == null)
                {
                    // Handle the case when the user doesn't exist in the database
                    return View("Not found");
                }

                // If a new profile picture has been uploaded, save it and update the user's ProfilePicture path
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
                    dbUser.ProfilePicture = "/uploads/" + fileName;
                }

                // Update the rest of the properties
                dbUser.Username = user.Username;
                dbUser.Password = user.Password;
                dbUser.first_name = user.first_name;
                dbUser.last_name = user.last_name;
                dbUser.Email = user.Email;
                dbUser.RegistrationDate = DateTime.UtcNow;  // Not sure if you want to update this?

                // Update the user in the database
                await _userService.UpdateAsync(id, dbUser);

                // Redirect the user to a page after successfully updating the User.
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // If model state is invalid, return the user back to the form
                return View(user);
            }
        }

        //Get: Users/Details/1
        public async Task<IActionResult> Details(int id)
        {
           // var userDetails = _userService.GetUserByIdAsync(id);
            var userDetails = _userService.GetUserById(id);
            if (userDetails == null) return View("Not found");
            return View(userDetails);
        }

        public IActionResult Delete(int id)
        {
            // Fetch the user from the database
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                // Handle the case when the user doesn't exist in the database
                return View("Not found");
            }
            // Return the 'Delete' view, which will show the user's details and ask for confirmation of the delete
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Fetch the user from the database
            var user =  _userService.GetUserById(id);
            if (user == null)
            {
                // Handle the case when the user doesn't exist in the database
                return View("Not found");
            }
            // Ask your service to delete the user
             _userService.Delete(id);

            _db.SaveChanges();
            // Redirect the user to the 'Index' page
            return RedirectToAction(nameof(Index));
        }

 


    }
}
