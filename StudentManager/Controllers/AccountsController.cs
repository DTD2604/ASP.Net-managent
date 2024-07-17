using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManager.Data;

namespace StudentManager.Controllers
{
    public class AccountsController : Controller
    {
        private readonly StudentManagerContext _context;

        public AccountsController(StudentManagerContext context)
        {
            _context = context;
        }

        // GET: Accounts
        [HttpGet]
        public async Task<IActionResult> Index(string? role)
        {
            if (role != null)
            {
                ViewBag.ModulePage = role;
                if (role == "Students")
                {
                    var userManagerContext = _context.Accounts
                        .Include(a => a.Role)
                        .Include(a => a.User)
                        .Where(a => a.Role.Name == "Student")
                        .Where(a => a.DeletedAt == null);
                    return View(await userManagerContext.ToListAsync());
                }
                else if (role == "Teachers")
                {
                    var teacherManagerContext = _context.Accounts
                        .Include(a => a.Role)
                        .Include(a => a.User)
                        .Where(a => a.Role.Name == "Teacher")
                        .Where(a => a.DeletedAt == null);
                    return View(await teacherManagerContext.ToListAsync());
                }
                else
                {
                    var adminManagerContext = _context.Accounts
                        .Include(a => a.Role)
                        .Include(a => a.User)
                        .Where(a => a.Role.Name == "Admin")
                        .Where(a => a.DeletedAt == null);
                    return View(await adminManagerContext.ToListAsync());
                }
            }

            ViewBag.ModulePage = HttpContext.Request.RouteValues["Controller"].ToString();
            var studentManagerContext = _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.User)
                .Where(a => a.DeletedAt == null);
            return View(await studentManagerContext.ToListAsync());
        }

        // GET: Accounts/Create
        /*[HttpPost]
        [Route("{controller}/Create/{id?}")]*/
        public IActionResult Create()
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();
            ViewData["users"] = _context.Users.Where(d => d.DeletedAt == null).ToList();
            ViewData["roles"] = _context.Roles.Where(r => r.DeletedAt == null).ToList();
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleId,UserId,Username,Password,Status,IpClient,LastLogin,LastLogout,CreatedAt,UpdatedAt,DeletedAt")] Account account)
        {
            ModelState["Role"].ValidationState = ModelValidationState.Valid;
            ModelState["User"].ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                account.CreatedAt = DateTime.UtcNow;
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["users"] = _context.Users.Where(d => d.DeletedAt == null).ToList();
            ViewData["roles"] = _context.Roles.Where(r => r.DeletedAt == null).ToList();
            return View(account);
        }

        // GET: Accounts/Edit/5
        /*[HttpPost]
        [Route("{controller}/Edit/{id}")]*/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewBag.ModulePage = RouteData.Values["controller"].ToString();
            ViewData["users"] = _context.Users.Where(d => d.DeletedAt == null).ToList();
            ViewData["roles"] = _context.Roles.Where(r => r.DeletedAt == null).ToList();
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleId,UserId,Username,Password,Status,IpClient,LastLogin,LastLogout,CreatedAt,UpdatedAt,DeletedAt")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            ModelState["Role"].ValidationState = ModelValidationState.Valid;
            ModelState["User"].ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                account.IpClient = _context.Accounts.Where(a => a.Id == id).Select(a => a.IpClient).FirstOrDefault();
                account.UpdatedAt = DateTime.UtcNow;
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
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
            ViewBag.ModulePage = RouteData.Values["controller"].ToString();
            ViewData["users"] = _context.Users.Where(d => d.DeletedAt == null).ToList();
            ViewData["roles"] = _context.Roles.Where(r => r.DeletedAt == null).ToList();
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                account.DeletedAt = DateTime.UtcNow;
                _context.Accounts.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                        .Include(a => a.Role)
                        .Include(a => a.User)
                        .Where(a => a.DeletedAt == null)
                        .FirstOrDefaultAsync(m => m.Id == id); ;
            if (account == null)
            {
                return NotFound();
            }
            /*var accounts = _context.Accounts
                        .Include(a => a.Role)
                        .Include(a => a.User)
                        .Where(a => a.DeletedAt == null)
                        .FirstOrDefaultAsync(m => m.Id == id);*/
            ViewBag.ModulePage = RouteData.Values["Controller"].ToString();
            return View(account);
        }


        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
