using DataAccess.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using Utility;

namespace PhlitsTicket.Controllers
{
    [Authorize]
    public class SeatController : Controller
    {
        SeatIRepo _seat;
        FlightIRepo _flight;
        public SeatController(SeatIRepo seat, FlightIRepo flight)
        {
            _flight = flight;
            _seat = seat;
        }

        public IActionResult Index()
        {
            return View(_seat.GetAll(includes: [e => e.Flight]).ToList());
        }
        [Authorize(Roles = StaticData.Admin)]
        public IActionResult Create()
        {
            ViewData["Flights"] = _flight.GetAll().ToList();
            return View();
        }
        [Authorize(Roles = StaticData.Admin)]
        [HttpPost]
        public IActionResult Create(SeatVM seatVM)
        {
            if (ModelState.IsValid)
            {
                var model = new Seat { Class = seatVM.Class, FlightID = seatVM.FlightID, Availability = seatVM.Availability };
                _seat.Create(model);
                _seat.commit();
                return RedirectToAction("Index");
            }
            return View(seatVM);
        }
        public IActionResult Details(int id)
        {
            var seat = _seat.GetOne(e => e.SeatID == id, includes: [e => e.Flight]);
            if (seat != null)
            {
                return View(seat);
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = StaticData.Admin)]
        public IActionResult Edit(int id)
        {
            var seat = _seat.GetOne(e => e.SeatID == id, includes: [e => e.Flight]);
            ViewData["Flights"] = _flight.GetAll().ToList();
            if (seat != null)
            {
                return View(seat);
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = StaticData.Admin)]
        [HttpPost]
        public IActionResult Edit(SeatVM seatVM)
        {
            if (ModelState.IsValid)
            {
                var seat = _seat.GetOne(e => e.SeatID == seatVM.SeatID);
                if (seat != null)
                {
                    seat.Availability = seatVM.Availability;
                    seat.Class = seatVM.Class;
                    seat.FlightID = seatVM.FlightID;
                    _seat.Edit(seat);
                    _seat.commit();
                    return RedirectToAction("Index");
                }
            }
            return View(seatVM);
        }
        [Authorize(Roles = StaticData.Admin)]
        public IActionResult Delete(int id)
        {
            if (_seat.Delete(id))
            {
                _seat.commit();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}