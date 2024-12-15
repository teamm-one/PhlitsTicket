using DataAccess.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using Utility;

namespace PhlitsTicket.Controllers
{
    [Authorize]
    public class AirlineFlightsController : Controller
    {
        AirLineFlightsIRepo _airlineFlights;
        AirLineIRepo _ailine;
        FlightIRepo _flight;

        public AirlineFlightsController(FlightIRepo flight, AirLineIRepo airLine, AirLineFlightsIRepo airLineFlights)
        {
            _flight = flight;
            _ailine = airLine;
            _airlineFlights = airLineFlights;
        }

        public IActionResult Index()
        {
            var models = _airlineFlights.GetAll(includes: [e => e.Airline, e => e.Flight]).ToList();
            return View(models);
        }
        [Authorize(Roles = StaticData.Admin)]
        public IActionResult Create()
        {
            ViewData["airlines"] = _ailine.GetAll().ToList();
            ViewData["flights"] = _flight.GetAll().ToList();
            return View();
        }
        [Authorize(Roles = StaticData.Admin)]
        [HttpPost]
        public IActionResult Create(airLineFlightsVM airLineFlights)
        {
            if (ModelState.IsValid)
            {
                var existingFlight = _airlineFlights.GetOne(e => e.FlightId == airLineFlights.FlightId);
                if (existingFlight != null)
                {
                    ModelState.AddModelError("ModelOnly", "This flight is already assigned to another airline.");
                }
                else
                {
                    AirLineFlights newAir = new AirLineFlights()
                    {
                        AirlineId = airLineFlights.AirlineId,
                        FlightId = airLineFlights.FlightId
                    };
                    _airlineFlights.Create(newAir);
                    _airlineFlights.commit();
                    return RedirectToAction("Index");
                }
            }
            ViewData["airlines"] = _ailine.GetAll().ToList();
            ViewData["flights"] = _flight.GetAll().ToList();
            return View(airLineFlights);
        }
        [Authorize(Roles = StaticData.Admin)]
        public IActionResult Delete(int lineId, int flightId)
        {
            var air = _airlineFlights.GetOne((e => e.AirlineId == lineId & e.FlightId == flightId));
            if (air != null)
            {
                _airlineFlights.Delete(null, model: air);
                _airlineFlights.commit();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = StaticData.Admin)]
        public IActionResult Edit(int lineId, int flightId)
        {
            var air = _airlineFlights.GetOne((e => e.AirlineId == lineId & e.FlightId == flightId));
            if (air != null)
            {
                ViewData["airlines"] = _ailine.GetAll().ToList();
                ViewData["flights"] = _flight.GetAll().ToList();
                return View(air);
            }
            return RedirectToAction("Index");
        }

    }
}
