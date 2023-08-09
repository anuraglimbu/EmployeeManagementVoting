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
    public class VotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Votes
        public async Task<IActionResult> Index()
        {
            ViewData["employees"] = _context.ev_Employees.ToList();
            return _context.ev_Votes != null ? 
                          View(await _context.ev_Votes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Votes'  is null.");
        }

        // GET: Votes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ev_Votes == null)
            {
                return NotFound();
            }

            var vote = await _context.ev_Votes
                .FirstOrDefaultAsync(m => m.vote_id == id);
            if (vote == null)
            {
                return NotFound();
            }
            ViewData["employees"] = _context.ev_Employees.ToList();
            return View(vote);
        }

        // GET: Votes/Create
        public IActionResult Create()
        {
            ViewData["employees"] = _context.ev_Employees.ToList();
            return View();
        }

        // POST: Votes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("vote_id,vote_date,voter_id,candidate_id")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                var dbRoleId = _context.ev_Employees.First(e => e.employee_id == vote.candidate_id).role_id;
                var formRoleId = _context.ev_Employees.First(e => e.employee_id == vote.candidate_id).role_id;

                if ((_context.ev_Votes?
                    .Any(v => 
                    (v.vote_date.Year == vote.vote_date.Year)
                        && (v.vote_date.Month == vote.vote_date.Month)
                        && (v.voter_id == vote.voter_id)
                        && ( (_context.ev_Departments.First(
                                d => d.department_id == _context.ev_Roles.First(
                                    r => r.role_id == _context.ev_Employees.First(
                                        e => e.employee_id == v.candidate_id).role_id).department_id).department_id)
                                                            ==
                            (_context.ev_Departments.First(
                                d => d.department_id == _context.ev_Roles.First(
                                    r => r.role_id == _context.ev_Employees.First(
                                        e => e.employee_id == vote.candidate_id).role_id).department_id).department_id)

                        )
                    )).GetValueOrDefault())
                {
                    TempData["StatusMessage"] = "Error: Vote For This Department Has Been Casted Already!";
                    return RedirectToAction(nameof(Create));
                }


                _context.Add(vote);
                await _context.SaveChangesAsync();

                TempData["StatusMessage"] = "Vote Casted Successfully";
                return RedirectToAction(nameof(Create));
            }

            TempData["StatusMessage"] = "Error: Casting Vote Failed";
            ViewData["employees"] = _context.ev_Employees.ToList();
            return View(vote);
        }

        // GET: Votes/Edit/5
        public async Task<IActionResult> Search()
        {
            ViewData["selected"] = -1;
            ViewData["employees"] = _context.ev_Employees.ToList();
            ViewData["departments"] = _context.ev_Departments.ToList();
            ViewData["roles"] = _context.ev_Roles.ToList();
            return View(await _context.ev_Votes.ToListAsync());
        }

        // POST: Votes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(int id)
        {
            if (_context.ev_Employees == null)
            {
                return NotFound();
            }

            ViewData["selected"] = id;
            ViewData["employees"] = _context.ev_Employees.ToList();
            ViewData["departments"] = _context.ev_Departments.ToList();
            ViewData["roles"] = _context.ev_Roles.ToList();
            return View(_context.ev_Votes.Where(eh => eh.voter_id == id).ToList());
        }

        public IActionResult TopThree()
        {
            ViewData["employees"] = _context.ev_Employees.ToList();
            ViewData["departments"] = _context.ev_Departments.ToList();
            ViewData["roles"] = _context.ev_Roles.ToList();

            return View();
        }

        // POST: Votes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TopThree([Bind("vote_id,vote_date,voter_id,candidate_id")] Vote vote)
        {
            ViewData["results"] = _context.ev_Votes.Where(v => (v.vote_date.Year == vote.vote_date.Year) && (v.vote_date.Month == vote.vote_date.Month)).ToList();
            ViewData["employees"] = _context.ev_Employees.ToList();
            ViewData["departments"] = _context.ev_Departments.ToList();
            ViewData["roles"] = _context.ev_Roles.ToList();
            return View(vote);
        }

        // GET: Votes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ev_Votes == null)
            {
                return NotFound();
            }

            var vote = await _context.ev_Votes
                .FirstOrDefaultAsync(m => m.vote_id == id);
            if (vote == null)
            {
                return NotFound();
            }
            ViewData["employees"] = _context.ev_Employees.ToList();
            return View(vote);
        }

        // POST: Votes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ev_Votes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Votes'  is null.");
            }
            var vote = await _context.ev_Votes.FindAsync(id);
            if (vote != null)
            {
                _context.ev_Votes.Remove(vote);
            }
            ViewData["employees"] = _context.ev_Employees.ToList();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoteExists(int id)
        {
          return (_context.ev_Votes?.Any(e => e.vote_id == id)).GetValueOrDefault();
        }
    }
}
