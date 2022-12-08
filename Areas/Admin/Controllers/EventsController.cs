using backProject.DAL.Entities;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backProject.Data;
using backProject.Areas.Admin.Services;
using backProject.Areas.Admin.Models;
using backProject.Areas.Admin.Data;
using Microsoft.Extensions.Logging;

namespace backProject.Areas.Admin.Controllers
{
    public class EventsController : BaseController
    {
        private readonly AppDbContext _dbContext;
        private readonly SpeakerService _speakerService;
        private readonly IWebHostEnvironment _environment;

        public EventsController(AppDbContext dbContext, SpeakerService speakerService, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
            _speakerService = speakerService;
        }
        public async Task<IActionResult> Index()
        {
            List<Event> events = await _dbContext.Events.Where(s => !s.IsDeleted).Include(x => x.EventSpeakers).ToListAsync();
            return View(events);
        }

        public async Task<IActionResult> Create()
        {
            var modelForSpeakers = await _speakerService.GetSpeakers();

            return View(modelForSpeakers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateViewModel model)
        {
            var modelForSpeakers = await _speakerService.GetSpeakers();

            if (!ModelState.IsValid)
            {
                return View(modelForSpeakers);
            }

            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "Sekil secmelisiz");
                return View(modelForSpeakers);
            }

            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Sekil 10mb-den cox ola bilmez");
                return View(modelForSpeakers);
            }

            if (DateTime.Compare(model.StartTime, model.EndTime) == 0)
            {

                ModelState.AddModelError("", "starttime ile endtime =dir");
                return View(modelForSpeakers);
            }

            if (DateTime.Compare(model.StartTime, model.EndTime) == 1)
            {

                ModelState.AddModelError("", "starttime endtime-dan sonraki zaman ola bilmez");
                return View(modelForSpeakers);
            }

            string unicalName = await model.Image.GenerateFile(Constants.SpeakerAndEventPath);

            List<EventSpeaker> createdEventSpeakers = new List<EventSpeaker>();
            Event createdEvent = new Event();

            foreach (var speakerId in model.SpeakersIds)
            {
                if (!await _dbContext.Speakers.AnyAsync(s => s.Id == speakerId))
                {
                    ModelState.AddModelError("", "Spiker movcud deyil..!");
                }
                createdEventSpeakers.Add(new EventSpeaker
                {
                    SpeakerId = speakerId
                });
            }

            await _dbContext.Events.AddAsync(new Event
            {
                Name = model.Name,
                Content = model.Content,
                Venue = model.Venue,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                ImageName = unicalName,
                EventSpeakers = createdEventSpeakers
            });

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var existEvent = await _dbContext.Events
                .Include(x => x.EventSpeakers)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (existEvent is null) return NotFound();

            return View(new EventDetailsViewModel
            {
                Eventt = existEvent,
                Speakers = await _dbContext.Speakers.ToListAsync()
            });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var existEvent = await _dbContext.Events.Include(x => x.EventSpeakers).Where(x => x.Id == id).FirstOrDefaultAsync();
            var eventSpeakers = await _dbContext.EventSpeakers.ToListAsync();

            if (existEvent is null) return NotFound();

            var path = Path.Combine(Constants.SpeakerAndEventPath, existEvent.ImageName);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            _dbContext.Events.Remove(existEvent);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return NotFound();

            var eventt = await _dbContext.Events.FindAsync(id);

            if (eventt is null) return NotFound();

            var modelForSpeakers = await _speakerService.GetSpeakers();

            List<EventSpeaker> eventAllSpeakersId = await _dbContext.EventSpeakers.Where(x => x.EventId == id).ToListAsync();

            List<int> existSpeakersIds= new List<int>();

            foreach (var speaker in eventAllSpeakersId)
            {
                existSpeakersIds.Add(speaker.SpeakerId);
            }

            return View(new EventUpdateViewModel
            {
                Name = eventt.Name,
                Content= eventt.Content,
                Venue= eventt.Venue,
                ImageUrl = eventt.ImageName,
                StartTime= eventt.StartTime,
                EndTime= eventt.EndTime,
                Speakers = modelForSpeakers.Speakers,
                SpeakersIds= existSpeakersIds
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, EventUpdateViewModel model)
        {
            var modelForSpeakers = await _speakerService.GetSpeakers();

            if (id is null)
                return NotFound();

            var existEvent = await _dbContext.Events.FindAsync(id);

            List<EventSpeaker> eventAllSpeakersId=await _dbContext.EventSpeakers.Where(x=>x.EventId==id).ToListAsync();

            if (existEvent is null)
                return NotFound();

            if (existEvent.Id != id)
                return BadRequest();

            List<int> existSpeakersIds = new List<int>();

            foreach (var eSpeaker in eventAllSpeakersId)
            {
                existSpeakersIds.Add(eSpeaker.SpeakerId);
            }

            EventUpdateViewModel eventModel = new EventUpdateViewModel
            {
                Name = existEvent.Name,
                Content = existEvent.Content,
                Venue = existEvent.Venue,
                ImageUrl = existEvent.ImageName,
                StartTime = existEvent.StartTime,
                EndTime = existEvent.EndTime,
                Speakers = modelForSpeakers.Speakers,
                SpeakersIds = existSpeakersIds
            };

            if (!ModelState.IsValid)
            {
                return View(eventModel);
            }

            var updateImage = existEvent.ImageName;

            if (model.Image is not null)
            {
                var newImage = model.Image;

                if (!newImage.IsImage())
                {
                    ModelState.AddModelError("Image", "Shekil formati secilmelidir");

                    return View(eventModel);
                }

                int imageMbCount = 10;

                if (!newImage.IsAllowedSize(imageMbCount))
                {
                    ModelState.AddModelError("Image", $"Shekilin olcusu {imageMbCount} mb-dan boyuk ola bilmez");

                    return View(eventModel);
                }                

                var path = Path.Combine(Constants.SpeakerAndEventPath, existEvent.ImageName);

                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                var unicalFileName = await model.Image.GenerateFile(Constants.SpeakerAndEventPath);

                updateImage = unicalFileName;
            }

            if (DateTime.Compare(model.StartTime, model.EndTime) == 0)
            {

                ModelState.AddModelError("", "starttime ile endtime =dir");
                return View(eventModel);
            }

            if (DateTime.Compare(model.StartTime, model.EndTime) == 1)
            {

                ModelState.AddModelError("", "starttime endtime-dan sonraki zaman ola bilmez");
                return View(eventModel);
            }

            List<EventSpeaker> updatedEventSpeakers = new List<EventSpeaker>();            

            foreach (var speakerId in model.SpeakersIds)
            {
                if (!await _dbContext.Speakers.AnyAsync(s => s.Id == speakerId))
                {
                    ModelState.AddModelError("", "Spiker movcud deyil..!");
                }
                updatedEventSpeakers.Add(new EventSpeaker
                {
                    SpeakerId = speakerId
                });
            }

            existEvent.Name = model.Name;
            existEvent.Content = model.Content;
            existEvent.Venue = model.Venue;
            existEvent.StartTime = model.StartTime;
            existEvent.EndTime = model.EndTime;
            existEvent.ImageName = updateImage;
            existEvent.EventSpeakers = updatedEventSpeakers;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
