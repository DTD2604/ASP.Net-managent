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
    public class SchedulesController : Controller
    {
        private readonly StudentManagerContext _context;

        public SchedulesController(StudentManagerContext context)
        {
            _context = context;
        }

        // GET: Schedules
        [Route("Schedule/index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.ModulePage = HttpContext.Request.Query["c"];
            var studentManagerContext = _context.Schedules.Include(s => s.Course).Include(s => s.Group).Include(s => s.Teacher);
            return View(await studentManagerContext.ToListAsync());
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Accounts, "Id", "Id");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,GroupId,Description,Location,TeacherId,StartDate,EndDate,Repeats,CreatedAt,UpdatedAt,DeletedAt")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", schedule.CourseId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", schedule.GroupId);
            ViewData["TeacherId"] = new SelectList(_context.Accounts, "Id", "Id", schedule.TeacherId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", schedule.CourseId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", schedule.GroupId);
            ViewData["TeacherId"] = new SelectList(_context.Accounts, "Id", "Id", schedule.TeacherId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,GroupId,Description,Location,TeacherId,StartDate,EndDate,Repeats,CreatedAt,UpdatedAt,DeletedAt")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", schedule.CourseId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", schedule.GroupId);
            ViewData["TeacherId"] = new SelectList(_context.Accounts, "Id", "Id", schedule.TeacherId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Course)
                .Include(s => s.Group)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
