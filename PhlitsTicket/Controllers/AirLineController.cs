using DataAccess.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using Utility;

namespace PhlitsTicket.Controllers
{
    [Authorize(Roles = StaticData.Admin)]
    public class AirLineController : Controller
    {
        AirLineIRepo airline;
        AirPortIRepo airPort;
        public AirLineController(AirLineIRepo airline, AirPortIRepo airPort)
        {
            this.airline = airline;
            this.airPort = airPort;
        }
        public IActionResult Index()
        {
            return View(airline.GetAll(includes: [e => e.AirPortArrive, e => e.AirPortLeave]).ToList());
        }
        public IActionResult Create()
        {
            var airports = airPort.GetAll().ToList();
            ViewData["Airports"] = airports;
            return View();
        }
        [HttpPost]
        public IActionResult Create(AirlineVM airlineVM)
        {
            if (ModelState.IsValid)
            {
                Airline airline = new Airline();
                airline.Name = airlineVM.Name;
                airline.AirportLeaveId = airlineVM.AirportLeaveId;
                airline.AirPortArriveId = airlineVM.AirPortArriveId;
                this.airline.Create(airline);
                this.airline.commit();
                return RedirectToAction("Index");
            }
            return View(airlineVM);
        }
        public IActionResult Edit(int id)
        {
            var airlinee = airline.GetOne(e => e.AirlineId == id);
            if (airlinee == null)
            {
                return RedirectToAction("Index");
            }
            var airports = airPort.GetAll().ToList();
            ViewData["Airports"] = airports;
            AirlineVM airlineVM = new AirlineVM();
            airlineVM.Name = airlinee.Name;
            airlineVM.AirportLeaveId = airlinee.AirportLeaveId;
            airlineVM.AirPortArriveId = airlinee.AirPortArriveId;
            return View(airlineVM);
        }
        [HttpPost]
        public IActionResult Edit(AirlineVM airlineVM, int id)
        {
            if (ModelState.IsValid)
            {
                var air = airline.GetOne(e => e.AirlineId == id);
                if (air != null)
                {
                    air.AirportLeaveId = airlineVM.AirportLeaveId;
                    air.AirPortArriveId = airlineVM.AirPortArriveId;
                    air.Name = airlineVM.Name;
                    airline.Edit(air);
                    airline.commit();
                    return RedirectToAction("Index");
                }
            }
            return View(airlineVM);
        }
        public IActionResult Delete(int id)
        {
            airline.Delete(id);
            airline.commit();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var returnAirLine = airline.GetOne(e => e.AirlineId == id);
            if (returnAirLine != null)
            {
                var airports = airPort.GetAll().ToList();
                ViewData["Airports"] = airports;
                AirlineVM airlineVM = new() { Name = returnAirLine.Name, AirPortArriveId = returnAirLine.AirPortArriveId, AirportLeaveId = returnAirLine.AirportLeaveId,Id=returnAirLine.AirlineId };
                return View(airlineVM);
            }
            return RedirectToAction("Index");
        }
    }
}
