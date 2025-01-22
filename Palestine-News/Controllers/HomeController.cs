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
            var news = _context.News
                .Include(n => n.Categories) // Include related category data
                .Include(n => n.User) // Include related user data
                .ToList();

            return View(news);
        }
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
        public IActionResult Privacy()
        {
            return View();
        }
    }
}