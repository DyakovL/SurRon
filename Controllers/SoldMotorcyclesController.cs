using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurRon.Infrastructure.Data;
using SurRon.Models.Motorcycles;
using SurRon.Models.MotorcycleTypes;
using SurRon.Models.SoldMotorcycles;
using System.Security.Claims;
using SurRon.Infrastructure.Data.Models;
using SurRon.Models.Inventory;

namespace SurRon.Controllers
{
    public class SoldMotorcyclesController : Controller
    {
        private readonly SurRonDbContext _data;

        public SoldMotorcyclesController(SurRonDbContext context)
        {
            _data = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var motors = await _data.SoldMotorcycles
                .AsNoTracking()
                .Select(m => new SoldMotorcyclesViewModel(
                    m.Id,
                    m.Name,
                    m.Address,
                    m.City,
                    m.Country,
                    m.DateSold,
                    m.Vin,
                    m.Color,
                    m.Engine,
                    m.MotorcycleType.Name,
                    m.Uploader.UserName,
                    m.Warranty
                ))
                .ToListAsync();

            return View(motors);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            var motors = await _data.SoldMotorcycles
                .Where(m => m.Vin == searchString)
                .AsNoTracking()
                .Select(m => new SoldMotorcyclesViewModel(
                    m.Id,
                    m.Name,
                    m.Address,
                    m.City,
                    m.Country,
                    m.DateSold,
                    m.Vin,
                    m.Color,
                    m.Engine,
                    m.MotorcycleType.Name,
                    m.Uploader.UserName,
                    m.Warranty
                ))
                .ToListAsync();

            if (motors.Count == 0)
            {
                return RedirectToAction(nameof(All));
            }

            return View(motors);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new SoldMotorcycleFormViewModel();
            model.DateSold = DateTime.Today;
            model.MotorcycleType = await GetMotorcycleTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SoldMotorcycleFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.MotorcycleType = await GetMotorcycleTypes();
            }

            var entity = new SoldMotorcycles()
            {
                Name = model.Name,
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                DateSold = model.DateSold,
                Vin = model.Vin,
                Color = model.Color,
                Engine = model.Engine,
                MotorcycleTypeId = model.MotorcycleTypeId,
                UploaderId = GetUserId()
            };

            if (_data.SoldMotorcycles.Contains(entity))
            {
                return BadRequest();
            }

            await _data.SoldMotorcycles.AddAsync(entity);
            await _data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> SellItem()
        {
            var model = new SellItemFormViewModel();
            model.SoldOn = DateTime.Today;
            model.InventoryItems = await GetItemTypes();

            return View(model);
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

        private async Task<IEnumerable<InventoryTypeView>> GetItemTypes()
        {
            return await _data.Inventory
                .AsNoTracking()
                .Select(i => new InventoryTypeView()
                {
                    Id = i.Id,
                    Name = i.Name
                })
                .ToListAsync();
        }
    }
}
