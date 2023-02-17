#region using statements
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForzaColourSearch.Data;
using ForzaColourSearch.Models;
using static ForzaColourSearch.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
#endregion

namespace ForzaColourSearch.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ForzaColourSearchContext _context;

        public VehiclesController(ForzaColourSearchContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index
            (
            string Mod,
            string Price,
            string ModType,
            string sortOrder,
            int? pageNumber
            )
        {
            ViewData["Mods"] = Mod;
            ViewData["Price"] = Price;
            ViewData["ModType"] = ModType;
            ViewData["CurrentSort"] = sortOrder;


            var vehicles = from s in _context.Vehicle
                           select s;

            //SearchMake search criteria =
            if (!String.IsNullOrEmpty(Mod))
            {
                vehicles = (IOrderedQueryable<Vehicle>)vehicles.Where(s => s.Mod.Contains(Mod));
            }

            if (!String.IsNullOrEmpty(Price))
            {
                vehicles = (IOrderedQueryable<Vehicle>)vehicles.Where(s => s.Price.Contains(Price));
            }

            if (!String.IsNullOrEmpty(ModType))
            {
                vehicles = (IOrderedQueryable<Vehicle>)vehicles.Where(s => s.ModType.Contains(ModType));
            }

            var pageSize = 50;
            return View(await PaginatedList<Vehicle>.CreateAsync(vehicles.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Vehicles/AddOrEdit(Insert)
        // GET: Vehicles/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Vehicle());
            else
            {
                var vehicleModel = await _context.Vehicle.FindAsync(id);
                if (vehicleModel == null)
                {
                    return NotFound();
                }
                return View(vehicleModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("ID,Mod,Price,ModType")] Vehicle vehicleModel)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _context.Add(vehicleModel);
                    await _context.SaveChangesAsync();
                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(vehicleModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!VehicleModelExists(vehicleModel.ID))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Vehicle.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", vehicleModel) });
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleModel = await _context.Vehicle.FindAsync(id);
            _context.Vehicle.Remove(vehicleModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Vehicle.ToList()) });
        }

        private bool VehicleModelExists(int id)
        {
            return _context.Vehicle.Any(e => e.ID == id);
        }
    }
}
