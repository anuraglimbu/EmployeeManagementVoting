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
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
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

            return View(await _context.ev_Contacts.ToListAsync());
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

            return View(await _context.ev_Contacts.Where(a => a.employee_id == id).ToListAsync());
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
        public async Task<IActionResult> Create([Bind("contact_id,employee_contact,employee_id")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();

                TempData["StatusMessage"] = "Contact Added Successfully";
                return RedirectToAction(nameof(Index));
            }

            TempData["StatusMessage"] = "Error: Adding Contact Failed";
            ViewData["employees"] = _context.ev_Employees.ToList();
            return View(contact);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ev_Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.ev_Contacts
                .FirstOrDefaultAsync(m => m.contact_id == id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["employees"] = _context.ev_Employees.ToList();
            return View(contact);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ev_Contacts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Contacts'  is null.");
            }
            var selected_address = await _context.ev_Contacts.FirstOrDefaultAsync(a => a.contact_id == id);
            if (selected_address != null)
            {
                _context.ev_Contacts.Remove(selected_address);
            }
            
            await _context.SaveChangesAsync();
            TempData["StatusMessage"] = "Contact Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
          return (_context.ev_Contacts?.Any(e => e.contact_id == id)).GetValueOrDefault();
        }
    }
}
