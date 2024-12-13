using DataAccess.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using Utility;

namespace PhlitsTicket.Controllers
{
    [Authorize]
    public class FlightController : Controller
    {
        FlightIRepo _flight;
        public FlightController(FlightIRepo flight)
        {
            _flight = flight;
        }
        public IActionResult Index()
        {
            var flights = _flight.GetAll(includes: [e => e.Seats]).ToList();
            return View(flights);
        }
        [Authorize(Roles = StaticData.Admin)]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = StaticData.Admin)]
        [HttpPost]
        public IActionResult Create(FlightVM flightVM)
        {
            if (ModelState.IsValid)
            {
                Flight flight = new() { Name = flightVM.Name };
                _flight.Create(flight);
                _flight.commit();
                return RedirectToAction("Index");
            }
            return View(flightVM);
        }
        public IActionResult Details(int id)
        {
            var flight = _flight.GetOne(e => e.FlightId == id, includes: [e => e.Seats]);
            if (flight != null)
            {
                return View(flight);
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = StaticData.Admin)]
        public IActionResult Edit(int id)
        {
            var flight = _flight.GetOne(e => e.FlightId == id);
            if (flight != null)
            {
                FlightVM flightVM = new FlightVM() { FlightId = flight.FlightId, Name = flight.Name };
                return View(flightVM);
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = StaticData.Admin)]
        [HttpPost]
        public IActionResult Edit(FlightVM flightVM)
        {
            if (ModelState.IsValid)
            {
                Flight flight = _flight.GetOne(e => e.FlightId == flightVM.FlightId);
                if (flight != null)
                {
                    flight.Name = flightVM.Name;
                    _flight.Edit(flight);
                    _flight.commit();
                    return RedirectToAction("Index");
                }
            }
            return View(flightVM);
        }
        [Authorize(Roles = StaticData.Admin)]
        public IActionResult Delete(int id)
        {
            _flight.Delete(id);
            _flight.commit();
            return RedirectToAction("Index");
        }
    }
}
