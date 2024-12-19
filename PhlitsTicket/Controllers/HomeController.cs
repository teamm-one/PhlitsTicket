using DataAccess.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using Stripe.Checkout;
using System.Diagnostics;
using Utility;
//sk_test_51QXVljFKQdroNBxjROWk3ehl9ocBy6ISz3reSQDLt6c66A1Z5XVjiVEMzm4pj8Kw2PfLkbhK4w75KVGCAABpt2oU00dTqGWyTJ
//sk_test_51QXVpbIHVyjpqHREPbN4lpmU8cXos8dFRnaT2IZPjo3vHCiL4koSv1YvRu238WtXenCb2sLM2lVgkDAW4LK6YM9500pkFdl2lt
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
                var trips = _trip.GetAll(additionalIncludes: e => e.Include(e => e.Airline)
                .ThenInclude(e => e.AirPortLeave)
                .Include(e => e.Airline)
                .ThenInclude(e => e.AirPortArrive)
                .Include(e => e.Flight)
                .ThenInclude(e => e.Seats), expression: e => e.Status != Status.Done).ToList();
                foreach (var i in trips)
                {
                    if (i.LeaveAt <= DateTime.Now)
                    {
                        i.Status = Status.Done;
                        _trip.Edit(i);
                        _trip.commit();
                    }
                }
                return View(trips);
            }
            else
            {
                var trips = _trip.GetAll(additionalIncludes: e => e.Include(e => e.Airline)
                .ThenInclude(e => e.AirPortLeave)
                .Include(e => e.Airline)
                .ThenInclude(e => e.AirPortArrive)
                .Include(e => e.Flight)
                .ThenInclude(e => e.Seats), expression: e => (e.Airline.AirPortLeave.City.Contains(from) || e.Airline.AirPortArrive.City.Contains(to)) && e.Status != Status.Done).ToList();
                foreach (var i in trips)
                {
                    if (i.LeaveAt <= DateTime.Now)
                    {
                        i.Status = Status.Done;
                        _trip.Edit(i);
                        _trip.commit();
                    }
                }
                return View(trips);
            }
        }
        [Authorize]
        public IActionResult Book(int tripId)
        {
            return View();
        }
        [Authorize]
        public IActionResult Pay(int id)
        {
            var trip = _trip.GetOne(e => e.TripId == id, additionalIncludes:
                e => e.Include(e => e.Airline)
                    .ThenInclude(e => e.AirPortLeave)
                    .Include(e => e.Airline)
                    .ThenInclude(e => e.AirPortArrive)
                    .Include(e => e.Flight)
                );
            if (trip != null)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/Home/Index",
                };
                options.LineItems.Add(
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "Egp",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Tiket From Airport {trip.Airline.AirPortLeave.Name} To Airport {trip.Airline.AirPortArrive.Name}",
                                Description = trip.Description
                            },
                            UnitAmount = trip.Price * 100
                        },
                        Quantity = 1,
                    }
                    );
                var service = new SessionService();
                var session = service.Create(options);
                return Redirect(session.Url);
            }
            return RedirectToAction("Index");
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
