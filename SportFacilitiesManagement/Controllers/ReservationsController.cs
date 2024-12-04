using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportFacilitiesManagement.Data;
using SportFacilitiesManagement.Models;

namespace SportFacilitiesManagement.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index(string sortOrder, string searchString)

        {
            ViewData["CustomerSortParm"] = String.IsNullOrEmpty(sortOrder) ? "customer_desc" : "";
            ViewData["FacilitySortParm"] = sortOrder == "facility_asc" ? "facility_desc" : "facility_asc";
            ViewData["CurrentFilter"] = searchString;

            var reservations = from s in _context.Reservation.Include(r => r.Customer).Include(r => r.Facility)
                               select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                reservations = reservations.Where(s => s.Customer.Name.Contains(searchString)
                                       || s.Facility.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "customer_desc":
                    reservations = reservations.OrderByDescending(s => s.Customer);
                    break;
                case "facility_desc":
                    reservations = reservations.OrderBy(s => s.Facility);
                    break;
                case "facility_asc":
                    reservations = reservations.OrderByDescending(s => s.Facility);
                    break;
                default:
                    reservations = reservations.OrderBy(s => s.Customer);
                    break;
            }
            return View(await reservations.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Customer)
                .Include(r => r.Facility)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email");
            ViewData["FacilityId"] = new SelectList(_context.Facility, "Id", "Address");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,FacilityId,ReservationDate,ReservationHours,TotalPrice")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email", reservation.CustomerId);
            ViewData["FacilityId"] = new SelectList(_context.Facility, "Id", "Address", reservation.FacilityId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email", reservation.CustomerId);
            ViewData["FacilityId"] = new SelectList(_context.Facility, "Id", "Address", reservation.FacilityId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,FacilityId,ReservationDate,ReservationHours,TotalPrice")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email", reservation.CustomerId);
            ViewData["FacilityId"] = new SelectList(_context.Facility, "Id", "Address", reservation.FacilityId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Customer)
                .Include(r => r.Facility)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
