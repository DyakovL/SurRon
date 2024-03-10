using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurRon.Infrastructure.Data;
using SurRon.Models.Motorcycles;
using System.Security.Claims;
using SurRon.Data.Models;
using SurRon.Models.MotorcycleTypes;

namespace SurRon.Controllers
{
    public class MotorcycleController : Controller
    {
        private readonly SurRonDbContext _data;

        public MotorcycleController(SurRonDbContext context)
        {
            _data = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var motors = await _data.Motorcycles
                .AsNoTracking()
                .Select(m => new MotorcycleViewModel(
                    m.Id,
                    m.Vin,
                    m.Color,
                    m.Engine,
                    m.MotorcycleType.Name,
                    m.Uploader.UserName
                ))
                .ToListAsync();

            return View(motors);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new MotorcycleFormViewModel();
            model.MotorcycleType = await GetMotorcycleTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MotorcycleFormViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.MotorcycleType = await GetMotorcycleTypes();
            }

            var entity = new Motorcycle()
            {
                Vin = model.Vin,
                Color = model.Color,
                Engine = model.Engine,
                UploaderId = GetUserId(),
                MotorcycleTypeId = model.MotorcycleTypeId
            };

            await _data.Motorcycles.AddAsync(entity);
            await _data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<IEnumerable<MotorcycleTypeView>> GetMotorcycleTypes()
        {
            return await _data.MotorcycleTypes
                .AsNoTracking()
                .Select(m => new MotorcycleTypeView()
                {
                    Id = m.Id,
                    Name = m.Name
                })
                .ToListAsync();
        }
    }
}
