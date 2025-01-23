using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Palestine_News.DBEntities;
using Palestine_News.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Palestine_News.Controllers
{
    public class NewsController : Controller
    {
        private readonly PalestineNewsContext _context;

        public NewsController(PalestineNewsContext context)
        {
            _context = context;
        }

        // GET: News

      [HttpGet]
        public IActionResult AddNews1()
        {
            // Fetch categories for the dropdown
            ViewBag.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoriesId.ToString(), // Ensure this matches the property name in the model
                    Text = c.CategoriesName // Ensure this matches the property name in the model
                })
                .ToList();

            return View();
        }

       


        // POST: News/AddNews
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNews1(Palestine_News.DBEntities.News news)
        {
            if (ModelState.IsValid)
            {
                // Log the news object being submitted
                Console.WriteLine($"Title: {news.Title}");
                Console.WriteLine($"Content: {news.Content}");
                Console.WriteLine($"CategoriesId: {news.CategoriesId}");

                // Retrieve the UserId from the claims
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    // Check if the UserId exists in the Users table
                    var userExists = await _context.Users.AnyAsync(u => u.UserId == userId);
                    if (!userExists)
                    {
                        // Handle the case where the UserId does not exist
                        ModelState.AddModelError("", "User not found.");
                        return View(news);
                    }

                    news.Userid = userId; // Associate the news with the logged-in user
                }
                else
                {
                    // Handle the case where the UserId is not found in the claims
                    ModelState.AddModelError("", "User not authenticated.");
                    return View(news);
                }

                _context.News.Add(news);
                await _context.SaveChangesAsync();

                // Display success message
                ViewBag.SuccessMessage = "News created successfully!";

                // Clear the form
                ModelState.Clear();

                // Repopulate categories for the dropdown
                ViewBag.Categories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoriesId.ToString(),
                        Text = c.CategoriesName
                    })
                    .ToList();

                return View();
            }

            // If validation fails, log the errors
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Validation Error: {error.ErrorMessage}");
            }

            // Repopulate categories for the dropdown
            ViewBag.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoriesId.ToString(),
                    Text = c.CategoriesName
                })
                .ToList();

            return View(news);
        }
    

     public IActionResult Create()
        {
            // Fetch categories and pass them to the view using ViewBag
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: News/Create
       [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Content,CategoriesId")] Models.News news)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            news.Userid = int.Parse(userId);
            _context.Add(news);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Categories = _context.Categories.ToList();
        return View(news);
    }
    }
}