using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManager.Data;

namespace StudentManager.Controllers
{
    public class CoursesController : Controller
    {
        private readonly StudentManagerContext _context;

        public CoursesController(StudentManagerContext context)
        {
            _context = context;
        }

        // GET: Courses
        [Route("Courses/index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();
            var studentManagerContext = _context.Courses.Where(c => c.DeletedAt == null).Include(c => c.Department);
            return View(await studentManagerContext.ToListAsync());
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
            ViewData["model"] = _context.Departments.Where(d => d.DeletedAt == null).ToList();
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug,DepartmentId,Status,CreatedAt,UpdatedAt,DeletedAt")] Course course)
        {
            course.Slug = GenerateSlug(course.Name);
            isValid(course);

            if (ModelState.IsValid)
            {
                course.CreatedAt = DateTime.UtcNow;

                _context.Add(course);
                await _context.SaveChangesAsync();
                TempData["Message"] = "created successfully.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["model"] = _context.Departments.Where(d => d.DeletedAt == null).ToList();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", course.DepartmentId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();
            ViewData["model"] = _context.Departments.Where(d => d.DeletedAt == null).ToList();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", course.DepartmentId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug,DepartmentId,Status,CreatedAt,UpdatedAt,DeletedAt")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            course.Slug = GenerateSlug(course.Name);
            isValid(course);

            if (ModelState.IsValid)
            {
                course.UpdatedAt = DateTime.UtcNow;

                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "updated successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["model"] = _context.Departments.Where(d => d.DeletedAt == null).ToList();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", course.DepartmentId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            var department = _context.Departments.FirstOrDefault(d => d.Id == course.DepartmentId);
            if (department != null)
            {
                ViewBag.Name = department.Name;
            }
            if (course == null)
            {
                return NotFound();
            }

            //ViewData["model"] = _context.Departments.Where(d => d.DeletedAt == null).ToList();
            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                course.DeletedAt = DateTime.UtcNow;
                _context.Courses.Update(course);
                TempData["Message"] = "deleted successfully.";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        private string GenerateSlug(string text)
        {
            // Viết logic xử lý Slug ở đây
            return text.Replace(" ", "-");
        }

        private void isValid(Course course)
        {
            ModelState["Slug"].RawValue = course.Slug;
            ModelState["Slug"].AttemptedValue = course.Slug;
            ModelState["Slug"].ValidationState = ModelValidationState.Valid;
            ModelState["Department"].ValidationState = ModelValidationState.Valid;
        }
    }
}
