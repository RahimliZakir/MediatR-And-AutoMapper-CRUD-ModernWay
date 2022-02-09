#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediatRAndAutoMapper.WebUI.Models.DataContexts;
using MediatRAndAutoMapper.WebUI.Models.Entities;
using MediatR;
using MediatRAndAutoMapper.WebUI.AppCode.Modules.PassengersModule;

namespace MediatRAndAutoMapper.WebUI.Controllers
{
    public class PassengersController : Controller
    {
        readonly TransportDbContext db;
        readonly IMediator mediator;

        public PassengersController(TransportDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            PassengersQuery query = new();
            IEnumerable<Passenger> passengers = await mediator.Send(query);

            return View(passengers);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await db.Passengers.FirstOrDefaultAsync(m => m.Id == id);

            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Age,TicketNumber")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                db.Add(passenger);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(passenger);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await db.Passengers.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }
            return View(passenger);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, [Bind("Id,Name,Surname,Age,TicketNumber")] Passenger passenger)
        {
            if (id != passenger.Id)
            {
                return NotFound();
            }

            return View(passenger);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var passenger = await db.Passengers.FindAsync(id);
            db.Passengers.Remove(passenger);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
