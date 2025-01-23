using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Palestine_News.DBEntities;
using Palestine_News.ViewModels;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Palestine_News.Controllers
{
    public class AccountController : Controller
    {
        private readonly PalestineNewsContext _context;

        public AccountController(PalestineNewsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Retrieve UserId from claims
            var userId = User.FindFirst("UserId")?.Value;
            Console.Write("userid = " + userId);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Fetch user details (optional)
            var user = _context.Users.Find(int.Parse(userId));
            ViewBag.UserName = user?.UserName;

            // Fetch all news items with their related categories and users
            var newsList = _context.News
                .Include(n => n.Categories) // Include the related category
                .Include(n => n.User) // Include the related user
                .ToList();

            // Log the number of news items fetched
            Console.WriteLine($"Number of news items fetched: {newsList.Count}");

            // Ensure newsList is not null
            if (newsList == null || !newsList.Any())
            {
                Console.WriteLine("No news items found in the database.");
            }

            return View(newsList);
        }
        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Create claims for the authenticated user
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("UserId", user.UserId.ToString()) // Store UserId in claims
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Sign in the user
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Redirect to the home page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }

            return View(model);
        }

     
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Login(LoginViewModel model)
        // {
        //    if (ModelState.IsValid)
        //    {
        //        var user = _context.Users
        //            .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

        //        if (user != null)
        //        {
        //            // Create claims for the authenticated user
        //            var claims = new List<Claim>
        //            {
        //                new Claim(ClaimTypes.Name, user.UserName),
        //                new Claim(ClaimTypes.Email, user.Email)
        //            };

        //            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            var principal = new ClaimsPrincipal(identity);

        //            // Sign in the user
        //            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //            // Redirect to the home page
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Invalid email or password.");
        //        }
        //    }

        //    return View(model);
        // }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Login(LoginViewModel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var user = _context.Users
        //             .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

        //         if (user != null)
        //         {
        //             // Create claims for the authenticated user
        //             var claims = new List<Claim>
        //     {
        //         new Claim(ClaimTypes.Name, user.UserName),
        //         new Claim(ClaimTypes.Email, user.Email),
        //         new Claim("UserId", user.UserId.ToString()) // Store UserId in claims
        //     };

        //             var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //             var principal = new ClaimsPrincipal(identity);

        //             // Sign in the user
        //             await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //             // Redirect to the home page
        //             return RedirectToAction("Index", "Home");
        //         }
        //         else
        //         {
        //             ModelState.AddModelError("", "Invalid email or password.");
        //         }
        //     }

        //     return View(model);
        // }

      

        [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Redirect to login page after successful registration
            return RedirectToAction("Login");
        }

        return View(model);
    }

        // GET: Account/Logout
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        // asd 
    }
}