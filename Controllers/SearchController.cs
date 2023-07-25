using Mentorship.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class SearchController : Controller
{
    private readonly MentorShipDbContext _db;

    public SearchController(MentorShipDbContext context)
    {
        _db = context;
    }

    public async Task<IActionResult> Index(string q)
    {
    
        var results = await _db.Users
            .Where(t => t.first_name.Contains(q))
            .ToListAsync();

        return View(results);
    }
}
