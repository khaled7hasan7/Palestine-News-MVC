using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Palestine_News.DBEntities;
using System.Linq;

namespace Palestine_News.Controllers
{
    public class HomeController : Controller
    {
        private readonly PalestineNewsContext _context;

        public HomeController(PalestineNewsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

        var userId = User.FindFirst("UserId")?.Value;

            Console.Write("1 userid = "+userId) ;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = _context.Users.Find(int.Parse(userId));
            
            ViewBag.UserName = user?.UserName;
            var news = _context.News
                .Include(n => n.Categories) // Include related category data
                .Include(n => n.User) // Include related user data
                .ToList();

            return View(news);
         }

        public IActionResult Indexx()
        {
            // Retrieve UserId from claims
            var userId = User.FindFirst("UserId")?.Value;
            Console.Write("1 userid = "+userId) ;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Fetch user details (optional)
            var user = _context.Users.Find(int.Parse(userId));
            
            ViewBag.UserName = user?.UserName;

            return View();
        }
    
         
        public IActionResult Privacy()
        {
            return View();
        }
    }
}