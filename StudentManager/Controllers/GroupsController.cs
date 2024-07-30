using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManager.Data;

namespace StudentManager.Controllers
{
    public class GroupsController : Controller
    {
        private readonly StudentManagerContext _context;

        public GroupsController(StudentManagerContext context)
        {
            _context = context;
        }

        // GET: Groups
        [Route("Groups/index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();
            var studentManagerContext = _context.Groups
                .Include(group => group.Department)
                .Include(group => group.Term)
                .Include(group => group.Captain)
                    .ThenInclude(captain => captain.User)
                .Include(group => group.Teacher)
                    .ThenInclude(teacher => teacher.User)
                .Where(group => group.DeletedAt == null)
                .Where(group => group.Department.DeletedAt == null)
                .Where(group => group.Term.DeletedAt == null);
            return View(await studentManagerContext.ToListAsync());
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();
            ViewData["departments"] = _context.Departments.Where(d => d.DeletedAt == null).ToList();
            ViewData["terms"] = _context.Terms.Where(term => term.DeletedAt == null).ToList();
            ViewData["teachers"] = _context.Accounts.Include(teacher => teacher.User).Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.RoleId == 2).ToList();
            ViewData["captains"] = _context.Accounts.Include(teacher => teacher.User).Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.RoleId == 1).ToList();
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentId,TermId,Name,Slug,StudentNumbers,TeacherId,CaptainId,Status,CreatedAt,UpdatedAt,DeletedAt")] Group group)
        {
            /*if (ModelState.IsValid)
            {*/
            group.Slug = GenerateSlug(group.Name);
            group.CreatedAt = DateTime.UtcNow;
            _context.Add(group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            /*}*/
            /*ViewData["departments"] = _context.Departments.Where(d => d.DeletedAt == null).ToList();
            ViewData["terms"] = _context.Terms.Where(term => term.DeletedAt == null).ToList();
            ViewData["teachers"] = _context.Accounts.Include(teacher => teacher.User).Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.RoleId == 2).ToList();
            ViewData["captains"] = _context.Accounts.Include(teacher => teacher.User).Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.RoleId == 1).ToList();*/
            //return View(group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();
            
            if (id == null)
            {
                return NotFound();
            }
            
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            ViewData["departments"] = _context.Departments.Where(d => d.DeletedAt == null).ToList();
            ViewData["terms"] = _context.Terms.Where(term => term.DeletedAt == null).ToList();
            ViewData["teachers"] = _context.Accounts.Include(teacher => teacher.User).Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.RoleId == 2).ToList();
            ViewData["captains"] = _context.Accounts.Include(teacher => teacher.User).Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.RoleId == 1).ToList();
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentId,TermId,Name,Slug,StudentNumbers,Teacher,Captain,Status,CreatedAt,UpdatedAt,DeletedAt")] Group group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)
            {*/
                try
                {
                    group.UpdatedAt = DateTime.UtcNow;
                    _context.Update(group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(group.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            /*}*/
            /*ViewData["departments"] = _context.Departments.Where(d => d.DeletedAt == null).ToList();
            ViewData["terms"] = _context.Terms.Where(term => term.DeletedAt == null).ToList();
            ViewData["teachers"] = _context.Accounts.Include(teacher => teacher.User).Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.RoleId == 2).ToList();
            ViewData["captains"] = _context.Accounts.Include(teacher => teacher.User).Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.RoleId == 1).ToList();
            return View(group);*/
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();
                
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups
                .Include(group => group.Department)
                .Include(group => group.Term)
                .Include(group => group.Captain)
                .ThenInclude(captain => captain.User)
                .Include(group => group.Teacher)
                .ThenInclude(teacher => teacher.User)
                .Where(group => group.DeletedAt == null)
                .Where(group => group.Department.DeletedAt == null)
                .Where(group => group.Term.DeletedAt == null)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group != null)
            {
                group.DeletedAt = DateTime.UtcNow;
                _context.Groups.Update(group);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
        
        private string GenerateSlug(string text)
        {
            // Viết logic xử lý Slug ở đây
            return text.Replace(" ", "-");
        }
    }
}
