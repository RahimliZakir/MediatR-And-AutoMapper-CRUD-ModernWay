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
using MediatRAndAutoMapper.WebUI.AppCode.Infrastructure;

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

        public async Task<IActionResult> Details(PassengerSingleQuery query)
        {
            PassengerViewModel passenger = await mediator.Send(query);

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
        public async Task<IActionResult> Create([Bind("Name,Surname,Age,TicketNumber")] PassengerCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
                return RedirectToAction(nameof(Index));

            return View(request);
        }

        public async Task<IActionResult> Edit(PassengerSingleQuery query)
        {
            PassengerViewModel passenger = await mediator.Send(query);

            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(/*[FromRoute] int id, */[Bind("Id,Name,Surname,Age,TicketNumber,CreatedDate")] PassengerEditCommand request)
        {
            //if (id != request.Id)
            //{
            //    return NotFound();
            //}

            int identifier = await mediator.Send(request);

            if (identifier > 0)
                return RedirectToAction(nameof(Index));

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PassengereRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
