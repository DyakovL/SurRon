﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurRon.Infrastructure.Data;
using SurRon.Infrastructure.Data.Models;
using SurRon.Models.Inventory;
using SurRon.Models.Motorcycles;

namespace SurRon.Controllers
{
    public class InventoryController : Controller
    {

        private readonly SurRonDbContext _data;

        public InventoryController(SurRonDbContext context)
        {
            _data = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var items = await _data.Inventory
                .AsNoTracking()
                .Select(i => new InventoryViewModel(
                    i.Id,
                    i.Name,
                    i.Price,
                    i.Amount,
                    i.ImageUrl
                ))
                .ToListAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new InventoryFormViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(InventoryFormViewModel model)
        {
            var entity = new Inventory()
            {
                Name = model.Name,
                Price = model.Price,
                Amount = model.Amount,
                ImageUrl = model.ImageUrl
            };

            await _data.Inventory.AddAsync(entity);
            await _data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            var items = await _data.Inventory
                .Where(i => i.Name == searchString)
                .AsNoTracking()
                .Select(i => new InventoryViewModel(
                    i.Id,
                    i.Name,
                    i.Price,
                    i.Amount,
                    i.ImageUrl
                ))
                .ToListAsync();

            if (items.Count == 0)
            {
                return RedirectToAction(nameof(All));
            }

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var e = await _data.Inventory
                .FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            var model = new InventoryFormViewModel()
            {
                Name = e.Name,
                Amount = e.Amount,
                Price = e.Price,
                ImageUrl = e.ImageUrl
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InventoryFormViewModel model, int id)
        {
            var e = await _data.Inventory
                .FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            e.Name = model.Name;
            e.Price = model.Price;
            e.Amount = model.Amount;
            e.ImageUrl = model.ImageUrl;

            await _data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _data.Inventory
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            _data.Inventory.Remove(e);

            await _data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
    }
}
