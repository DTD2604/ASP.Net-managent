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
                .Include(a => a.Department)
                .Include(a => a.Term)
                .Where(a => a.DeletedAt == null)
                .Where(a => a.Department.DeletedAt == null)
                .Where(a => a.Term.DeletedAt == null);
            return View(await studentManagerContext.ToListAsync());
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();
            ViewData["departments"] = _context.Departments.Where(d => d.DeletedAt == null);
            ViewData["terms"] = _context.Terms.Where(term => term.DeletedAt == null);
            ViewData["teachers"] = _context.Accounts.Where(teacher => teacher.DeletedAt == null)
                                                    .Where(teacher => teacher.Role.Name == "teacher");
            ViewData["captains"] = _context.Accounts.Where(teacher => teacher.DeletedAt == null)
                                                    .Where(teacher => teacher.Role.Name == "student");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentId,TermId,Name,Slug,StudentNumbers,Teacher,Captain,Status,CreatedAt,UpdatedAt,DeletedAt")] Group group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["departments"] = _context.Departments.Where(d => d.DeletedAt == null);
            ViewData["terms"] = _context.Terms.Where(term => term.DeletedAt == null);
            ViewData["teachers"] = _context.Accounts.Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.Role.Name == "teacher");
            ViewData["captains"] = _context.Accounts.Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.Role.Name == "student");
            return View(group);
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
            ViewData["departments"] = _context.Departments.Where(d => d.DeletedAt == null);
            ViewData["terms"] = _context.Terms.Where(term => term.DeletedAt == null);
            ViewData["teachers"] = _context.Accounts.Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.Role.Name == "teacher");
            ViewData["captains"] = _context.Accounts.Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.Role.Name == "student");
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

            if (ModelState.IsValid)
            {
                try
                {
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
            }
            ViewData["departments"] = _context.Departments.Where(d => d.DeletedAt == null);
            ViewData["terms"] = _context.Terms.Where(term => term.DeletedAt == null);
            ViewData["teachers"] = _context.Accounts.Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.Role.Name == "teacher");
            ViewData["captains"] = _context.Accounts.Where(teacher => teacher.DeletedAt == null)
                .Where(teacher => teacher.Role.Name == "student");
            return View(group);
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
                .Include(a => a.Department)
                .Include(a => a.Term)
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
                _context.Groups.Remove(group);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
