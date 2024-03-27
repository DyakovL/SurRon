using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurRon.Infrastructure.Data;
using SurRon.Models.Motorcycles;
using SurRon.Models.SoldMotorcycles;
using System.Security.Claims;

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
                    m.Uploader.UserName
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
                    m.Uploader.UserName
                ))
                .ToListAsync();

            if (motors.Count == 0)
            {
                return RedirectToAction(nameof(All));
            }

            return View(motors);
        }

        //[HttpGet]
        //public async Task<IActionResult> SellItem()
        //{

        //}

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
