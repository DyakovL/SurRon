using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurRon.Data;
using SurRon.Infrastructure.Data;
using SurRon.Models.Events;
using System.Security.Claims;
using SurRon.Infrastructure.Data.Models;
using SurRon.Models.Motorcycles;

namespace SurRon.Controllers
{
    public class EventController : Controller
    {
        private readonly SurRonDbContext _data;

        public EventController(SurRonDbContext context)
        {
            _data = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var events = await _data.Events
                .AsNoTracking()
                .Select(e => new EventViewModel(
                    e.Id,
                    e.Name,
                    e.Description,
                    e.Date,
                    e.Location,
                    e.Organizer.UserName
                ))
                .ToListAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EventFormViewModel();
            model.Date = DateTime.Today;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {
            var entity = new Event()
            {
                Name = model.Name,
                Location = model.Location,
                Description = model.Description,
                OrganizerId = GetUserId(),
                Date = DateTime.Now
            };

            await _data.Events.AddAsync(entity);
            await _data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var e = await _data.Events
                .FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new EventFormViewModel()
            {
                Name = e.Name,
                Location = e.Location,
                Description = e.Description,
                Date = e.Date
            };

            return View(model);
        }

        //[HttpGet]
        //public async Task<IActionResult> Sell(int id)
        //{
        //    var m = await _data.Motorcycles
        //        .FindAsync(id);

        //    if (m == null)
        //    {
        //        return BadRequest();
        //    }

        //    var model = await _data.Motorcycles
        //        .Where(x => x.Id == id)
        //        .Select(m => new MotorcycleSellViewModel()
        //        {
        //            Id = id,
        //            Name = "",
        //            DateTime = DateTime.Now,
        //            Address = "",
        //            City = "",
        //            Country = "",
        //            Vin = m.Vin,
        //            Engine = m.Engine,
        //            Color = m.Color,
        //            MotorcycleType = m.MotorcycleType.Name
        //        })
        //        .FirstOrDefaultAsync();

        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormViewModel model, int id)
        {
            var e = await _data.Events
                .FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            e.Name = model.Name;
            e.Location = model.Location;
            e.Description = model.Description;
            e.Date = model.Date;
            e.OrganizerId = GetUserId();

            await _data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var e = await _data.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var entry = new EventParticipant()
            {
                EventId = e.Id,
                ParticipantId = userId
            };

            if (await _data.EventsParticipants.ContainsAsync(entry))
            {
                return RedirectToAction(nameof(All));
            }

            await _data.EventsParticipants.AddAsync(entry);
            await _data.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));

        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await _data.EventsParticipants
                .Where(sp => sp.ParticipantId == userId)
                .AsNoTracking()
                .Select(sp => new EventViewModel(
                    sp.EventId,
                    sp.Event.Name,
                    sp.Event.Description,
                    sp.Event.Date,
                    sp.Event.Location,
                    sp.Event.Organizer.UserName
                ))
                .ToListAsync();


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var e = await _data.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var ep = e.EventsParticipants
                .FirstOrDefault(ep => ep.ParticipantId == userId);

            if (ep == null)
            {
                return BadRequest();
            }

            e.EventsParticipants.Remove(ep);
            await _data.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _data.Events
                .Where(e => e.Id == id)
                .AsNoTracking()
                .Select(e => new EventDetailsModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Date = e.Date,
                    Description = e.Description,
                    Location = e.Location,
                    Organizer = e.Organizer.UserName
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _data.Events
                .FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = await _data.Events
                .Where(e => e.Id == id)
                .AsNoTracking()
                .Select(e => new EventDeleteModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Date = e.Date,
                    Organizer = e.Organizer.UserName
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var e = await _data.Events
                .Where(s => s.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var ep = e.EventsParticipants
                .FirstOrDefault(ep => ep.EventId == e.Id);

            if (ep != null)
            {
                _data.EventsParticipants.Remove(ep);
            }

            _data.Events.Remove(e);
            await _data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
