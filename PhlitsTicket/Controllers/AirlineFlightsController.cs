using DataAccess.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using Utility;

namespace PhlitsTicket.Controllers
{
    [Authorize(Roles = StaticData.Admin)]

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
        public IActionResult Create()
        {
            ViewData["airlines"] = _ailine.GetAll().ToList();
            ViewData["flights"] = _flight.GetAll().ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(AirLineFlightsVM airLineFlights)
        {
            if (ModelState.IsValid)
            {
                var existingFlight = _airlineFlights.GetOne(e => e.FlightId == airLineFlights.FlightId);
                if (existingFlight != null && existingFlight.AirlineId == airLineFlights.AirlineId)
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
        public IActionResult Edit(int lineId, int flightId)
        {
            var air = _airlineFlights.GetOne((e => e.AirlineId == lineId & e.FlightId == flightId));
            if (air != null)
            {
                ViewData["airlines"] = _ailine.GetAll().ToList();
                ViewData["flights"] = _flight.GetAll().ToList();
                AirLineFlightsVM airlineFlightsVM = new()
                {
                    AirlineId = air.AirlineId,
                    FlightId = flightId
                };
                return View(airlineFlightsVM);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(AirLineFlightsVM airLineFlightsVM, int lastFlightId, int lastAirId)
        {
            if (ModelState.IsValid)
            {
                var last = _airlineFlights.GetOne((e => e.AirlineId == lastAirId & e.FlightId == lastFlightId));
                if (last != null)
                {
                    _airlineFlights.Delete(null, last);
                    AirLineFlights newAir = new() { AirlineId = airLineFlightsVM.AirlineId, FlightId = lastFlightId };
                    try
                    {
                        _airlineFlights.Create(newAir);
                        _airlineFlights.commit();
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        ViewData["airlines"] = _ailine.GetAll().ToList();
                        ViewData["flights"] = _flight.GetAll().ToList();
                        ModelState.AddModelError("ModelOnly", "This flight is already assigned to another airline.");
                        airLineFlightsVM.AirlineId=lastAirId;
                        airLineFlightsVM.FlightId=lastFlightId;
                        return View(airLineFlightsVM);
                    }
                }
                ModelState.AddModelError("ModelOnly", "con not defind  airline flight");
            }
            ViewData["airlines"] = _ailine.GetAll().ToList();
            ViewData["flights"] = _flight.GetAll().ToList();
            return View(airLineFlightsVM);
        }
        public IActionResult Details(int lineId, int flightId)
        {
            var air = _airlineFlights.GetOne((e => e.AirlineId == lineId & e.FlightId == flightId), includes: [e=>e.Airline,e=>e.Flight]);
            if(air != null)
            {
                return View(air);
            }
            return RedirectToAction("Index");
        }
    }
}
