using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeVoting.Data;
using EmployeeVoting.Models;
using System.Data;

namespace EmployeeVoting.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public EmployeesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Employees
		public async Task<IActionResult> Index()
		{
			ViewData["departments"] = _context.ev_Departments.ToList();
			ViewData["roles"] = _context.ev_Roles.ToList();

			ViewData["addresses"] = _context.ev_Addresses.ToList();
			ViewData["contacts"] = _context.ev_Contacts.ToList();

			return _context.ev_Employees != null ? 
						  View(await _context.ev_Employees.ToListAsync()) :
						  Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
		}

		// GET: Employees/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.ev_Employees == null)
			{
				return NotFound();
			}

			var employee = await _context.ev_Employees
				.FirstOrDefaultAsync(m => m.employee_id == id);
			if (employee == null)
			{
				return NotFound();
			}

			ViewData["departments"] = _context.ev_Departments.ToList();
			ViewData["roles"] = _context.ev_Roles.ToList();

			ViewData["addresses"] = _context.ev_Addresses.ToList();
			ViewData["contacts"] = _context.ev_Contacts.ToList();

			return View(employee);
		}

		// GET: Employees/Create
		public IActionResult Create()
		{
			ViewData["departments"] = _context.ev_Departments.ToList();
			ViewData["roles"] = _context.ev_Roles.ToList();
			return View();
		}

		// POST: Employees/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("employee_id,employee_name,employee_dob,employee_email,role_id")] Employee employee)
		{
			if (ModelState.IsValid)
			{
				_context.Add(employee);
				await _context.SaveChangesAsync();

				var data = _context.ev_Employees.First(e => e.employee_name == employee.employee_name && e.employee_email == employee.employee_email);

				EmployeeHistory newHistory = new EmployeeHistory();
				newHistory.employee_id = data.employee_id;
				newHistory.department_name = _context.ev_Departments.First(d => d.department_id == _context.ev_Roles.First(r => r.role_id == employee.role_id).department_id).department_name;
				newHistory.role_name = _context.ev_Roles.First(r => r.role_id == employee.role_id).role_name;
				newHistory.joined_role_date = DateTime.Now;

				_context.ev_EmployeeHistories.Add(newHistory);
				await _context.SaveChangesAsync();

				TempData["StatusMessage"] = "Employee Added Successfully";
				return RedirectToAction(nameof(Index));
			}
			
			TempData["StatusMessage"] = "Error: Adding Employee Failed";
			ViewData["departments"] = _context.ev_Departments.ToList();
			ViewData["roles"] = _context.ev_Roles.ToList();
			return View(employee);
		}

		// GET: Employees/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.ev_Employees == null)
			{
				return NotFound();
			}

			var employee = await _context.ev_Employees.FindAsync(id);
			if (employee == null)
			{
				return NotFound();
			}
			ViewData["departments"] = _context.ev_Departments.ToList();
			ViewData["roles"] = _context.ev_Roles.ToList();
			return View(employee);
		}

		// POST: Employees/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("employee_id,employee_name,employee_dob,employee_email,role_id")] Employee employee)
		{
			if (id != employee.employee_id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(employee);

					EmployeeHistory newHistory = new EmployeeHistory();
					newHistory.employee_id = employee.employee_id;
					newHistory.department_name = _context.ev_Departments.First(d => d.department_id == _context.ev_Roles.First(r => r.role_id == employee.role_id).department_id).department_name;
					newHistory.role_name = _context.ev_Roles.First(r => r.role_id == employee.role_id).role_name;
					newHistory.joined_role_date = DateTime.Now;

					_context.ev_EmployeeHistories.Add(newHistory);

					await _context.SaveChangesAsync();

					
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!EmployeeExists(employee.employee_id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				//return Redirect("~/EmployeeHistories/InjectHistory");
				TempData["StatusMessage"] = "Employee Edited Successfully";
				return RedirectToAction(nameof(Index));
			}

			TempData["StatusMessage"] = "Error: Editing Employee Failed";
			ViewData["departments"] = _context.ev_Departments.ToList();
			ViewData["roles"] = _context.ev_Roles.ToList();
			return View(employee);
		}

		// GET: Employees/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.ev_Employees == null)
			{
				return NotFound();
			}

			var employee = await _context.ev_Employees
				.FirstOrDefaultAsync(m => m.employee_id == id);
			if (employee == null)
			{
				return NotFound();
			}

			ViewData["departments"] = _context.ev_Departments.ToList();
			ViewData["roles"] = _context.ev_Roles.ToList();

			ViewData["addresses"] = _context.ev_Addresses.ToList();
			ViewData["contacts"] = _context.ev_Contacts.ToList();

			return View(employee);
		}

		// POST: Employees/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.ev_Employees == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
			}

			var employee = await _context.ev_Employees.FindAsync(id);
			if (employee != null)
			{

				var addresses = _context.ev_Addresses.Where(a => a.employee_id == employee.employee_id);
				foreach (var adr in addresses)
				{
					_context.ev_Addresses.Remove(adr);
				}

				var contacts = _context.ev_Contacts.Where(c => c.employee_id == employee.employee_id);
				foreach (var con in contacts)
				{
					_context.ev_Contacts.Remove(con);
				}


				_context.ev_Employees.Remove(employee);
			}
			
			await _context.SaveChangesAsync();
			TempData["StatusMessage"] = "Employee Deleted Successfully";
			return RedirectToAction(nameof(Index));
		}

		private bool EmployeeExists(int id)
		{
		  return (_context.ev_Employees?.Any(e => e.employee_id == id)).GetValueOrDefault();
		}

		private async Task<bool> InjectHistory(int roleId, int employeeId)
		{
			var oldData = _context.ev_Employees.First(e => e.employee_id == employeeId);
			if (roleId != oldData.role_id)
			{
				EmployeeHistory newHistory = new EmployeeHistory();
				newHistory.employee_id = oldData.employee_id;
				newHistory.department_name = _context.ev_Departments.First(d => d.department_id == _context.ev_Roles.First(r => r.role_id == oldData.role_id).department_id).department_name;
				newHistory.role_name = _context.ev_Roles.First(r => r.role_id == oldData.role_id).role_name;
				newHistory.joined_role_date = DateTime.Now;

				_context.ev_EmployeeHistories.Add(newHistory);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
