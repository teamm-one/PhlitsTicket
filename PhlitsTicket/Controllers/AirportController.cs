using DataAccess.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using Utility;

namespace PhlitsTicket.Controllers
{
    [Authorize(Roles = StaticData.Admin)]

    public class AirportController : Controller
    {
        AirPortIRepo airPort;
        public AirportController(AirPortIRepo airPort)
        {
            this.airPort = airPort;
        }
        public IActionResult Index()
        {
            return View(airPort.GetAll().ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AirportVM airport)
        {
            if (ModelState.IsValid)
            {
                Airport newAir = new Airport { Name = airport.Name, Country = airport.Country, City = airport.City };
                airPort.Create(newAir);
                airPort.commit();
                return RedirectToAction("Index");
            }
            return View(airport);
        }
        public IActionResult Edit(int id)
        {
            return View(airPort.GetOne(e => e.AirportId == id));
        }
        [HttpPost]
        public IActionResult Edit(Airport airport)
        {
            airPort.Edit(airport);
            airPort.commit();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id) {
                airPort.Delete(id);
                airPort.commit();
                return RedirectToAction("Index");
            
        }
        public IActionResult Details(int id) {
            return View(airPort.GetOne(e=>e.AirportId==id));
        }
    }
}
