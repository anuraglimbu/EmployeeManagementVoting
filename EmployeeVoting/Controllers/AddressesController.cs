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
    public class AddressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            ViewData["employees"] = _context.ev_Employees.ToList();

            ViewData["departments"] = _context.ev_Departments.ToList();
            ViewData["roles"] = _context.ev_Roles.ToList();

            ViewData["selected"] = -1;

            return View(await _context.ev_Addresses.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id)
        {
            if (_context.ev_Employees == null)
            {
                return NotFound();
            }

            ViewData["selected"] = id;
            ViewData["employees"] = _context.ev_Employees.ToList();
            ViewData["departments"] = _context.ev_Departments.ToList();
            ViewData["roles"] = _context.ev_Roles.ToList();
            return View(await _context.ev_Addresses.Where(a => a.employee_id == id).ToListAsync());
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            ViewData["employees"] = _context.ev_Employees.ToList();
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("address_id,address_name,employee_id")] Address address)
        {
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();

                TempData["StatusMessage"] = "Address Added Successfully";
                return RedirectToAction(nameof(Index));
            }

            TempData["StatusMessage"] = "Error: Adding Address Failed";
            ViewData["employees"] = _context.ev_Employees.ToList();
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ev_Addresses == null)
            {
                return NotFound();
            }

            var selected_address = await _context.ev_Addresses
                .FirstOrDefaultAsync(m => m.address_id == id);
            if (selected_address == null)
            {
                return NotFound();
            }
            ViewData["employees"] = _context.ev_Employees.ToList();
            return View(selected_address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ev_Addresses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Addresses'  is null.");
            }
            var selected_address = await _context.ev_Addresses.FirstOrDefaultAsync(a => a.address_id == id);
            if (selected_address != null)
            {
                _context.ev_Addresses.Remove(selected_address);
            }
            
            await _context.SaveChangesAsync();
            TempData["StatusMessage"] = "Address Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(string address)
        {
          return (_context.ev_Addresses?.Any(e => e.address_name == address)).GetValueOrDefault();
        }
    }
}
