﻿using backProject.DAL.Entities;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backProject.Models;

namespace backProject.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _dbContext;

        public EventsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            var eventt = await _dbContext.Events.Include(x => x.EventSpeakers).SingleOrDefaultAsync(t => t.Id == id);

            if (eventt is null) return NotFound();

            List<Speaker> speakers = await _dbContext.Speakers.ToListAsync();

            return View(new EventViewModel
            {
                eventt = eventt,
                speakers = speakers
            });
        }
    }
}
