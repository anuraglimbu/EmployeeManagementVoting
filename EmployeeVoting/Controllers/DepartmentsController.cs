using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeVoting.Data;
using EmployeeVoting.Models;

namespace EmployeeVoting.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
              return _context.ev_Departments != null ? 
                          View(await _context.ev_Departments.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Departments'  is null.");
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ev_Departments == null)
            {
                return NotFound();
            }

            var department = await _context.ev_Departments
                .FirstOrDefaultAsync(m => m.department_id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("department_id,department_name")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();

                TempData["StatusMessage"] = "Department Created Successfully";
                return RedirectToAction(nameof(Index));
            }

            TempData["StatusMessage"] = "Error: Department Creation Failed";
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ev_Departments == null)
            {
                return NotFound();
            }

            var department = await _context.ev_Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("department_id,department_name")] Department department)
        {
            if (id != department.department_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.department_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["StatusMessage"] = "Department Edited Successfully";
                return RedirectToAction(nameof(Index));
            }

            TempData["StatusMessage"] = "Error: Editing Department Failed";
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ev_Departments == null)
            {
                return NotFound();
            }

            var department = await _context.ev_Departments
                .FirstOrDefaultAsync(m => m.department_id == id);
            if (department == null)
            {
                return NotFound();
            }

            var roles = _context.ev_Roles.Where(st => st.department_id == id);
            ViewData["numOfRoles"] = roles.Count();

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string? delOption)
        {
            if (_context.ev_Departments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Departments'  is null.");
            }
            var department = await _context.ev_Departments.FindAsync(id);
            if (department != null)
            {
                if (delOption != null && delOption == "delete")
                {
                    var roles = _context.ev_Roles.Where(r => r.department_id == department.department_id);
                    foreach (var r in roles)
                    {
                        _context.ev_Roles.Remove(r);
                    }
                }

                _context.ev_Departments.Remove(department);
            }
            
            await _context.SaveChangesAsync();

            TempData["StatusMessage"] = "Department Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
          return (_context.ev_Departments?.Any(e => e.department_id == id)).GetValueOrDefault();
        }
    }
}
