using DataAccess.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PhlitsTicket.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly BookingIRepo _book;

        public BookingController(BookingIRepo book)
        {
            _book = book;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookings = _book.GetAll(additionalIncludes: e => e.Include(e => e.Trip)
            .ThenInclude(e => e.Airline).ThenInclude(e => e.AirPortArrive).Include(e => e.Trip).ThenInclude(e => e.Airline).ThenInclude(e => e.AirPortLeave)
            .Include(e => e.Seat), expression: e => e.ApplicationUserId == userId).ToList();
            return View(bookings);
        }
    }
}
