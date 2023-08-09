using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeVoting.Data;
using EmployeeVoting.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmployeeVoting.Controllers
{
    public class EmployeeHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeHistories
        public async Task<IActionResult> Index()
        {
            ViewData["employees"] = _context.ev_Employees.ToList();

            ViewData["departments"] = _context.ev_Departments.ToList();
            ViewData["roles"] = _context.ev_Roles.ToList();

            ViewData["selected"] = -1;
            return View(await _context.ev_EmployeeHistories.ToListAsync());
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
            return View(await _context.ev_EmployeeHistories.Where(a => a.employee_id == id).ToListAsync());
        }


        // GET: EmployeeHistories/Create
        public IActionResult Search()
        {
            ViewData["employees"] = _context.ev_Employees.ToList();
            return View();
        }

        // POST: EmployeeHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search([Bind("history_id,employee_id,department_name,role_name,joined_role_on")] EmployeeHistory employeeHistory)
        {
            ViewData["results"] = _context.ev_EmployeeHistories.Where(eh => eh.employee_id == employeeHistory.employee_id).ToList();
            ViewData["employees"] = _context.ev_Employees.ToList();
            return View(employeeHistory);
        }
    }
}
