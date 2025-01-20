using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index()
        {
            var news = await _context.News
                .Include(n => n.Categories)
                .Include(n => n.User)
                .ToListAsync();
            return View(news);
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Categories)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            ViewData["CategoriesId"] = _context.Categories.Select(c => new { c.CategoriesId, c.CategoriesName }).ToList();
            ViewData["Userid"] = _context.Users.Select(u => new { u.UserId, u.UserName }).ToList();
            return View();
        }

        // POST: News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsId,Title,Content,Userid,CategoriesId")] News news)
        {
            if (ModelState.IsValid)
            {
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriesId"] = _context.Categories.Select(c => new { c.CategoriesId, c.CategoriesName }).ToList();
            ViewData["Userid"] = _context.Users.Select(u => new { u.UserId, u.UserName }).ToList();
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["CategoriesId"] = _context.Categories.Select(c => new { c.CategoriesId, c.CategoriesName }).ToList();
            ViewData["Userid"] = _context.Users.Select(u => new { u.UserId, u.UserName }).ToList();
            return View(news);
        }

        // POST: News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsId,Title,Content,Userid,CategoriesId")] News news)
        {
            if (id != news.NewsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.NewsId))
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
            ViewData["CategoriesId"] = _context.Categories.Select(c => new { c.CategoriesId, c.CategoriesName }).ToList();
            ViewData["Userid"] = _context.Users.Select(u => new { u.UserId, u.UserName }).ToList();
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Categories)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.NewsId == id);
        }
    }
}