using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurRon.Core.Models.Motorcycles;
using SurRon.Core.Models.MotorcycleTypes;
using SurRon.Infrastructure.Data;
using SurRon.Infrastructure.Data.Models;
using System.Security.Claims;

namespace SurRon.Controllers
{
    public class MotorcycleController : AdminBaseController
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

            if (_data.Motorcycles.Contains(entity))
            {
                return BadRequest();
            }

            await _data.Motorcycles.AddAsync(entity);
            await _data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var e = await _data.Motorcycles
                .FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            var model = await _data.Motorcycles
                .Where(x => x.Id == id)
                .Select(m => new MotorcycleFormViewModel()
                {
                    Vin = m.Vin,
                    Color = m.Color,
                    Engine = m.Engine,
                    MotorcycleTypeId = m.MotorcycleTypeId
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            model.MotorcycleType = await GetMotorcycleTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MotorcycleFormViewModel model, int id)
        {
            var e = await _data.Motorcycles
                .FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                model.MotorcycleType = await GetMotorcycleTypes();
            }

            e.Vin = model.Vin;
            e.Color = model.Color;
            e.Engine = model.Engine;
            e.MotorcycleTypeId = model.MotorcycleTypeId;

            await _data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            var motors = await _data.Motorcycles
                .Where(m => m.Vin == searchString)
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

            if (motors.Count == 0)
            {
                return RedirectToAction(nameof(All));
            }

            return View(motors);
        }

        [HttpGet]
        public async Task<IActionResult> Sell(int id)
        {
            var m = await _data.Motorcycles
                .FindAsync(id);

            if (m == null)
            {
                return BadRequest();
            }

            var model = await _data.Motorcycles
                .Where(x => x.Id == id)
                .Select(m => new MotorcycleSellViewModel()
                {
                    Id = id,
                    Name = "",
                    DateTime = DateTime.Today,
                    Address = "",
                    City = "",
                    Country = "",
                    Vin = m.Vin,
                    Engine = m.Engine,
                    Color = m.Color,
                    MotorcycleType = m.MotorcycleType.Name
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Sell(MotorcycleSellViewModel model, int id)
        {
            var m = await _data.Motorcycles
                .Where(x => x.Id == id)
                .Include(x => x.SellerMotorcycles)
                .FirstOrDefaultAsync();

            if (m == null)
            {
                return BadRequest();
            }
            
            var soldModel = new SoldMotorcycles()
            {
                Name = model.Name,
                Vin = model.Vin,
                Engine = model.Engine,
                DateSold = model.DateTime,
                City = model.City,
                Address = model.Address,
                Country = model.Country,
                Color = model.Color,
                MotorcycleTypeId = m.MotorcycleTypeId,
                Warranty = true,
                UploaderId = GetUserId()
            };
            
            await _data.SoldMotorcycles.AddAsync(soldModel);
        
            _data.Motorcycles.Remove(m);
            await _data.SaveChangesAsync();

            return RedirectToAction("All", "SoldMotorcycles");
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {

            if (statusCode == 400)
            {
                return View("Error404");
            }

            if (statusCode == 401)
            {
                return View("Error500");
            }

            return View();
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
