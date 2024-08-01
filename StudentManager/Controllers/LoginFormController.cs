using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManager.Data;

namespace StudentManager.Controllers
{
    public class LoginFormController : Controller
    {
        private readonly StudentManagerContext _context;

        public LoginFormController(StudentManagerContext context)
        {
            _context = context;
        }
        // GET: LoginForm
        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ActionName("Login")]
        public async Task<IActionResult> Index([Bind("Username,Password,LastLogin")] Account account)
        {
            var user = await _context.Accounts
                .Include(a => a.User)
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Username == account.Username);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                if (user.Password == account.Password)
                {
                    // Update LastLogin if needed
                    user.LastLogin = DateTime.UtcNow;
                    await _context.SaveChangesAsync();

                    // Redirect to the welcome page or dashboard
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Password is incorrect
                    ViewBag.ErrorMessage = "Invalid password.";
                }
            }
            else
            {
                // Username is incorrect
                ViewBag.ErrorMessage = "Invalid username.";
            }
            return View(account);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(
            [Bind("Username,Password,RoleId,UserId,Status,CreatedAt,User")] Account account)
        {
            
            if (ModelState.IsValid)
            {
                // Check if the username already exists
                var verify = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Username == account.Username);

                if (verify == null)
                {
                    // Create a new User
                    var user = new User
                    {
                        FirstName = account.User.FirstName,
                        LastName = account.User.LastName,
                        FullName = account.User.FirstName + account.User.LastName,
                        Email = account.User.Email,
                        Phone = account.User.Phone,
                        CreatedAt = DateTime.UtcNow
                    };

                    // Set the CreatedAt property for Account
                    account.CreatedAt = DateTime.UtcNow;
                    account.Role.Name = "student";
                    account.User = user;

                    // Add the new User and Account
                    _context.Users.Add(user);
                    _context.Accounts.Add(account);
                    await _context.SaveChangesAsync();

                    // Redirect to the Index action or another page
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Username already exists
                    ModelState.AddModelError("Username", "Username already exists.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(account);
        }
        
        public IActionResult ForgotPassword()
        {
            return View();
        }

    }
}
