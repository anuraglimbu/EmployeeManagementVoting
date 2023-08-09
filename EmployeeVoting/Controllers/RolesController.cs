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
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            ViewData["departments"] = await _context.ev_Departments.ToDictionaryAsync(x => x.department_id, x => x.department_name);
            return _context.ev_Roles != null ? 
                          View(await _context.ev_Roles.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Roles'  is null.");
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ev_Roles == null)
            {
                return NotFound();
            }

            var role = await _context.ev_Roles
                .FirstOrDefaultAsync(m => m.role_id == id);
            if (role == null)
            {
                return NotFound();
            }

            ViewData["department"] = await _context.ev_Departments.FirstAsync(x => x.department_id == role.department_id);
            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            ViewData["departments"] = _context.ev_Departments.ToList();
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("role_id,department_id,role_name")] Role role)
        {
            if (ModelState.IsValid)
            {
                _context.Add(role);
                await _context.SaveChangesAsync();

                TempData["StatusMessage"] = "Role Created Successfully";
                return RedirectToAction(nameof(Index));
            }

            TempData["StatusMessage"] = "Error: Role Creation Failed";
            ViewData["departments"] = _context.ev_Departments.ToList();
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ev_Roles == null)
            {
                return NotFound();
            }

            var role = await _context.ev_Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            ViewData["departments"] = _context.ev_Departments.ToList();
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("role_id,department_id,role_name")] Role role)
        {
            if (id != role.role_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.role_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["StatusMessage"] = "Role Edited Successfully";
                return RedirectToAction(nameof(Index));
            }

            TempData["StatusMessage"] = "Error: Editing Role Failed";
            ViewData["departments"] = _context.ev_Departments.ToList();
            return View(role);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ev_Roles == null)
            {
                return NotFound();
            }

            var role = await _context.ev_Roles
                .FirstOrDefaultAsync(m => m.role_id == id);
            if (role == null)
            {
                return NotFound();
            }

            ViewData["department"] = ((_context.ev_Departments?.Any(e => e.department_id == role.department_id)).GetValueOrDefault())?
                                    await _context.ev_Departments.FirstAsync(x => x.department_id == role.department_id)
                                    : new Department { department_name = "Deleted" };
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ev_Roles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Roles'  is null.");
            }
            var role = await _context.ev_Roles.FindAsync(id);
            if (role != null)
            {
                _context.ev_Roles.Remove(role);
            }
            
            await _context.SaveChangesAsync();

            TempData["StatusMessage"] = "Role Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
          return (_context.ev_Roles?.Any(e => e.role_id == id)).GetValueOrDefault();
        }
    }
}
