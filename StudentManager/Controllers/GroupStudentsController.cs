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
    public class GroupStudentsController : Controller
    {
        private readonly StudentManagerContext _context;

        public GroupStudentsController(StudentManagerContext context)
        {
            _context = context;
        }

        // GET: GroupStudents
        public async Task<IActionResult> Index()
        {
            ViewBag.ModulePage = HttpContext.Request.Query["c"];
            var studentManagerContext = _context.GroupStudents
                .Include(g => g.Course)
                .Include(g => g.Group)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .Where(g => g.Course.DeletedAt == null)
                .Where(g => g.Student.DeletedAt == null)
                .Where(g => g.Group.DeletedAt == null)
                .Where(g => g.Student.Role.Id == 1)
                .Where(g => g.Teacher.Role.Id == 2);
            return View(await studentManagerContext.ToListAsync());
        }

        // GET: GroupStudents/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Accounts, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Accounts, "Id", "Id");
            return View();
        }

        // POST: GroupStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("GroupId,StudentId,CourseId,TeacherId,Absent,Present,LearningDate,CreatedAt")]
            GroupStudent groupStudent)
        {
            groupStudent.CreatedAt = DateTime.UtcNow;
            _context.Add(groupStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: GroupStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupStudent = await _context.GroupStudents.FindAsync(id);
            if (groupStudent == null)
            {
                return NotFound();
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", groupStudent.CourseId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", groupStudent.GroupId);
            ViewData["StudentId"] = new SelectList(_context.Accounts, "Id", "Id", groupStudent.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Accounts, "Id", "Id", groupStudent.TeacherId);
            return View(groupStudent);
        }

        // POST: GroupStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,GroupId,StudentId,CourseId,TeacherId,Absent,Present,LearningDate,CreatedAt,UpdatedAt,DeletedAt")]
            GroupStudent groupStudent)
        {
            if (id != groupStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupStudentExists(groupStudent.Id))
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

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", groupStudent.CourseId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", groupStudent.GroupId);
            ViewData["StudentId"] = new SelectList(_context.Accounts, "Id", "Id", groupStudent.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Accounts, "Id", "Id", groupStudent.TeacherId);
            return View(groupStudent);
        }

        // GET: GroupStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupStudent = await _context.GroupStudents
                .Include(g => g.Course)
                .Include(g => g.Group)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupStudent == null)
            {
                return NotFound();
            }

            return View(groupStudent);
        }

        // POST: GroupStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupStudent = await _context.GroupStudents.FindAsync(id);
            if (groupStudent != null)
            {
                _context.GroupStudents.Remove(groupStudent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupStudentExists(int id)
        {
            return _context.GroupStudents.Any(e => e.Id == id);
        }
    }
}