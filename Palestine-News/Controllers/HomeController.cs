using Microsoft.AspNetCore.Mvc;
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
            var news = _context.News
                .Include(n => n.Categories) // Include related category data
                .Include(n => n.User) // Include related user data
                .ToList();

            return View(news);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}