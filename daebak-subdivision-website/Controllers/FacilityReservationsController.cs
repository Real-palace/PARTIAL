using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using daebak_subdivision_website.Models;
using daebak_subdivision_website.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace daebak_subdivision_website.Controllers
{
    public class FacilityReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacilityReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display all reservations with facility and user details
        public async Task<IActionResult> Index()
        {
            var reservations = await _context.FacilityReservations
                .Include(r => r.Facility)
                .Include(r => r.User)
                .Select(r => new FacilityReservationViewModel
                {
                    ReservationId = r.Id,
                    FacilityId = r.FacilityId,
                    UserId = r.UserId,
                    ReservationDate = r.ReservationDate,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    FacilityName = r.Facility.Name,
                    UserName = r.User.FirstName + " " + r.User.LastName
                })
                .ToListAsync();

            return View(reservations);
        }

        // Load the Create Reservation Form with available facilities
        public async Task<IActionResult> Create()
        {
            var model = new FacilityReservationViewModel
            {
                AvailableFacilities = await _context.Facilities.ToListAsync()
            };
            return View(model);
        }

        // Handle new reservation submission
        [HttpPost]
        public async Task<IActionResult> Create(FacilityReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reservation = new FacilityReservation
                {
                    FacilityId = model.FacilityId,
                    UserId = model.UserId,
                    ReservationDate = model.ReservationDate,
                    Status = model.Status,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.FacilityReservations.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            model.AvailableFacilities = await _context.Facilities.ToListAsync();
            return View(model);
        }

        // Handle reservation deletion
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _context.FacilityReservations.FindAsync(id);
            if (reservation != null)
            {
                _context.FacilityReservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
