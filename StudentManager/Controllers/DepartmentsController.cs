using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManager.Data;
using StudentManager.Models;
using StudentManager.Models.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace StudentManager.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly StudentManagerContext _context;

        public DepartmentsController(StudentManagerContext context)
        {
            _context = context;
        }

        // GET: Departments
        [Route("Department/index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();

            var studentManagerContext = _context.Departments
                .Include(d => d.Leader)
                .ThenInclude(a => a.User)
                .Where(d => d.DeletedAt == null)
                .Where(a => a.DeletedAt == null);

            return View(await studentManagerContext.ToListAsync());
        }

        // GET: Departments/Create
        public IActionResult Create()
        {

            ViewData["model"] = _context.Accounts.Include(a => a.Role)
                .Include(a => a.User)
                .Where(a => a.RoleId == 2)
                .Where(a => a.DeletedAt == null).ToList();

            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();

            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Slug,DateBeginning,Status,Logo,CreatedAt,UpdatedAt,DeletedAt,LeaderId")] Department department)
        {
            department.Slug = GenerateSlug(department.Name);

            // Thêm giá trị "raw value" cho trường Slug
            
            ModelState["Slug"].RawValue = department.Slug;
            ModelState["Slug"].AttemptedValue = department.Slug;
            ModelState["Slug"].ValidationState = ModelValidationState.Valid;
            ModelState["Leader"].ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                //UploadFile(department.Logo);
                department.CreatedAt = DateTime.UtcNow;
                _context.Add(department);
                await _context.SaveChangesAsync();
                TempData["Message"] = "created successfully.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["model"] = _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.User)
                .Where(a => a.RoleId == 2)
                .Where(a => a.DeletedAt == null).ToList();
            ViewData["LeaderId"] = new SelectList(_context.Accounts, "Id", "Id", department.LeaderId);
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();

            ViewData["model"] = _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.User)
                .Where(a => a.RoleId == 2)
                .Where(a => a.DeletedAt == null)
                .ToList();

            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug,DateBeginning,Status,Logo,CreatedAt,UpdatedAt,DeletedAt,LeaderId")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }
            department.Slug = GenerateSlug(department.Name);

            ModelState["Slug"].RawValue = department.Slug;
            ModelState["Slug"].AttemptedValue = department.Slug;
            ModelState["Slug"].ValidationState = ModelValidationState.Valid;
            ModelState["Leader"].ValidationState = ModelValidationState.Valid;


            if (ModelState.IsValid)
            {
                try
                {
                    department.UpdatedAt = DateTime.UtcNow;
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "updated successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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

            ViewData["model"] = _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.User)
                .Where(a => a.RoleId == 2)
                .Where(a => a.DeletedAt == null)
                .ToList();

            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(d => d.Leader)
                .FirstOrDefaultAsync(m => m.Id == id);

            var account = _context.Accounts.FirstOrDefault(a => a.Id == department.LeaderId);

            if (account != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == account.UserId);
                if (user != null)
                {
                    ViewBag.FullName = user.FullName;
                }
            }
            if (department == null)
            {
                return NotFound();
            }

            ViewBag.ModulePage = HttpContext.Request.RouteValues["controller"].ToString();

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                department.DeletedAt = DateTime.UtcNow;
                _context.Update(department);
                TempData["Message"] = "deleted successfully.";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /*public void UploadFile(IFormFile file)
        {
            // Create a folder named "img" if it doesn't exist
            string imgFolder = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(imgFolder))
            {
                Directory.CreateDirectory(imgFolder);
            }

            // Generate a unique file name
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Save the file to the "img" folder
            string filePath = Path.Combine(imgFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }*/

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }

        private string GenerateSlug(string text)
        {
            // Viết logic xử lý Slug ở đây
            return text.Replace(" ", "-");
        }
    }
}
