using System.Linq;
using Microsoft.AspNetCore.Mvc;
using daebak_subdivision_website.Models;
using System.Collections.Generic;

public class ServiceRequestsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ServiceRequestsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var serviceRequests = _context.ServiceRequests
            .Select(sr => new ServiceRequestView
            {
                Id = sr.Id,
                UserId = sr.UserId,
                HouseNumber = sr.HouseNumber,
                RequestType = sr.RequestType,
                Description = sr.Description,
                Status = sr.Status,
                CreatedAt = sr.CreatedAt,
                UpdatedAt = sr.UpdatedAt,
                AssignedTo = sr.AssignedTo,
                RequestedBy = _context.Users
                    .Where(u => u.UserId == sr.UserId)
                    .Select(u => u.FirstName + " " + u.LastName)
                    .FirstOrDefault(),
                AssignedToName = _context.Users
                    .Where(u => u.UserId == sr.AssignedTo)
                    .Select(u => u.FirstName + " " + u.LastName)
                    .FirstOrDefault()
            })
            .ToList();

        return View(serviceRequests);
    }

}
