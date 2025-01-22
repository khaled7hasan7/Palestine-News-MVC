using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Palestine_News.DBEntities;
using System.Linq;
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

        public IActionResult AddNews()
        {

            ViewBag.CategoriesId = _context.Categories
         .Select(c => new SelectListItem
         {
             Value = c.CategoriesId.ToString(), // Ensure this matches the property name in the model
             Text = c.CategoriesName // Ensure this matches the property name in the model
         })
         .ToList();
            return View();
        }

        public IActionResult News()
        {
            var news = _context.News
                .Include(n => n.Categories)
                .Include(n => n.User)
                .ToList();

            return View(news); // Pass the list of news articles to the view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNews(Palestine_News.DBEntities.News news)
        {
            if (ModelState.IsValid)
            {
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

                // Return the same view
                return View(news);
            }

            // If validation fails, repopulate dropdowns and return to the form
            ViewBag.CategoriesId = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoriesId.ToString(),
                    Text = c.CategoriesName
                })
                .ToList();

            return View(news);
        }


        // POST: News/Create
       

        
    }
}