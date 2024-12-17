using DataAccess.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using System.Diagnostics;
using Utility;

namespace PhlitsTicket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TripIRepo _trip;
        public HomeController(ILogger<HomeController> logger, TripIRepo trip)
        {
            _logger = logger;
            _trip = trip;
        }

        public IActionResult Index(string from = null, string to = null)
        {
            if (from == null && to == null)
            {
                var trips=_trip.GetAll(additionalIncludes: e => e.Include(e => e.Airline)
                .ThenInclude(e => e.AirPortLeave)
                .Include(e => e.Airline)
                .ThenInclude(e => e.AirPortArrive),expression:e=>e.Status!=Status.Done).ToList();
                return View(trips);
            }
            else
            {
                var trips=_trip.GetAll(additionalIncludes: e => e.Include(e => e.Airline)
                    .ThenInclude(e => e.AirPortLeave)
                    .Include(e => e.Airline)
                    .ThenInclude(e => e.AirPortArrive), expression: e => (e.Airline.AirPortLeave.City.Contains(from) || e.Airline.AirPortArrive.City.Contains(from) ||
                    e.Airline.AirPortLeave.City.Contains(to) || e.Airline.AirPortArrive.City.Contains(to))&&e.Status!=Status.Done).ToList();
            return View(trips);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
