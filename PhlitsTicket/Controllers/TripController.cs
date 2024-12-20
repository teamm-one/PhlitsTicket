using DataAccess.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using Utility;

namespace PhlitsTicket.Controllers
{
    [Authorize(Roles = StaticData.Admin)]

    public class TripController : Controller
    {
        TripIRepo _trip;
        AirLineIRepo _airline;
        AirLineFlightsIRepo _flight;

        public TripController(TripIRepo trip, AirLineIRepo airLine, AirLineFlightsIRepo flight)
        {
            _trip = trip;
            _airline = airLine;
            _flight = flight;
        }

        public IActionResult Index()
        {
            var trips = _trip.GetAll(includes: [e => e.Airline, e => e.Flight]);
            DateTime dateTime = DateTime.Now;
            foreach (var i in trips)
            {
                i.Status = i.LeaveAt <= dateTime ? Status.Done : Status.Await;
                _trip.Edit(i);
                _trip.commit();
            }
            return View(trips);
        }
        public IActionResult Create()
        {
            ViewData["airlines"] = _airline.GetAll().ToList();
            return View();
        }
        public IActionResult SelectFlight(TripDataVM trip)
        {
            if (ModelState.IsValid)
            {
                ViewData["flights"] = _flight.GetAll(expression: e => e.AirlineId == trip.AirlineId, includes: [e => e.Flight]).ToList();
                TripVM newTrip = new()
                {
                    TripDataVM = trip
                };
                return View(newTrip);
            }
            return RedirectToAction("Create");
        }
        [HttpPost]
        public IActionResult Create(TripVM tripVM)
        {
            if (ModelState.IsValid)
            {
                Trip trip = new()
                {
                    AirlineId = tripVM.TripDataVM.AirlineId,
                    Price = tripVM.TripDataVM.Price,
                    Description = tripVM.TripDataVM.Description,
                    Status = Status.Await,
                    LeaveAt = tripVM.TripDataVM.LeaveAt,
                    FlightId = tripVM.FlightId
                };
                _trip.Create(trip);
                _trip.commit();
                return RedirectToAction("Index");
            }
            return View(tripVM);
        }
        public IActionResult Details(int id)
        {
            var trip = _trip.GetOne(e => e.TripId == id, includes: [e => e.Airline, e => e.Flight]);
            if (trip != null)
                return View(trip);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var trip = _trip.GetOne(e => e.TripId == id, includes: [e => e.Airline, e => e.Flight]);
            if (trip != null)
                return View(trip);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(TripEditVM trip)
        {
            if (ModelState.IsValid)
            {
                Trip initTrip = new()
                {
                    TripId = trip.TripId,
                    LeaveAt = trip.LeaveAt,
                    Price = trip.Price,
                    Description = trip.Description,
                    Status = trip.Status,
                    AirlineId = trip.AirlineId,
                    FlightId = trip.FlightId
                };
                _trip.Edit(initTrip);
                _trip.commit();
                return RedirectToAction("Index");
            }
            return View(trip);
        }
        public IActionResult Delete(int id)
        {
            try
            {
                _trip.Delete(id);
                _trip.commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
