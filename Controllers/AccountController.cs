using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> Logout()
        {
           // await HttpContext.SignOutAsync(); // Sign out of the local authentication scheme
           // await HttpContext.SignOutAsync("OpenIdConnect"); // Sign out of the Microsoft authentication scheme if using OpenID Connect
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "LandingPage");
        }
    }
}
