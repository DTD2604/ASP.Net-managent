using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManager.Data;

namespace StudentManager.Controllers
{
    [Route("[role]/accounts/[action]")]
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
            ViewBag.ModulePage = HttpContext.Request.RouteValues["role"].ToString();

            var studentManagerContext = _context.Accounts.Include(a => a.Role).Include(a => a.User);

            return View(await studentManagerContext.ToListAsync());
        }

        /*[HttpGet]
        public IActionResult Teachers()
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["action"].ToString();

            var studentManagerContext = _context.Accounts.Include(a => a.Role).Include(a => a.User).Where(a => a.RoleId == 2);

            return View(studentManagerContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Admins()
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["action"].ToString();

            var studentManagerContext = _context.Accounts.Include(a => a.Role).Include(a => a.User).Where(a => a.RoleId == 3);

            return View(studentManagerContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Users()
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["action"].ToString();

            var studentManagerContext = _context.Accounts.Include(a => a.Role).Include(a => a.User).Where(a => a.RoleId == 1);

            return View(studentManagerContext.ToListAsync());
        }*/

        // GET: Accounts/Create
        [HttpPost]
        public IActionResult Create()
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleId,UserId,Username,Password,Status,IpClient,LastLogin,LastLogout,CreatedAt,UpdatedAt,DeletedAt")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", account.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", account.UserId);
            return View(account);
        }

        // GET: Accounts/Edit/5
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
            ViewBag.ModulePage = RouteData.Values["c"].ToString();
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", account.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", account.UserId);
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

            if (ModelState.IsValid)
            {
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
            ViewBag.ModulePage = RouteData.Values["c"].ToString();
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", account.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", account.UserId);
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

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewBag.ModulePage = RouteData.Values["c"].ToString();
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", account.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", account.UserId);
            return View(account);
        }
        

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
