﻿#region Using statements
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BensModManager.Data;
using BensModManager.Models;
using static BensModManager.Helper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
#endregion

namespace BensModManager.Controllers
{
    public class ModsController : Controller
    {
        private readonly BensModManagerContext _context;

        #region Default Routes
        [Route("/Error")]
        public IActionResult Error()
        {
            return View();
        }
        #endregion

        #region Db Class
        public ModsController(BensModManagerContext context)
        {
            _context = context;
        }
        #endregion

        //GET: Mods
        public async Task<IActionResult> Index ( string modName, string modType, string sortOrder, int? pageNumber )
        {
            //Set the search parameters
            ViewData["ModName"] = modName;
            ViewData["ModType"] = modType;

            var mods = from s in _context.Mod
                       select s;

            //Search criteria
            if (!String.IsNullOrEmpty(modName))
            {
                mods = mods.Where(s => s.ModName.Contains(modName.TrimEnd()));
            }

            if (!String.IsNullOrEmpty(modType))
            {
                mods = mods.Where(s => s.ModType.Contains(modType));
            }

            #region Column Sorting
            //Set the various sort parameters
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ModNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "modNameDescending" : "";
            ViewData["PriceSortParam"] = sortOrder == "priceAscending" ? "priceDescending" : "priceAscending";
            ViewData["ModTypeSortParam"] = sortOrder == "modTypeAscending" ? "modTypeDescending" : "modTypeAscending";

            //Switch between the sort orders
            mods = sortOrder switch
            {
                "modNameDescending" => mods.OrderByDescending(s => s.ModName),
                "priceAscending" => mods.OrderBy(s => s.Price),
                "priceDescending" => mods.OrderByDescending(s => s.Price),
                "modTypeAscending" => mods.OrderBy(s => s.ModType),
                "modTypeDescending" => mods.OrderByDescending(s => s.ModType),
                _ => mods.OrderBy(s => s.ModName),
            };
            #endregion

            var pageSize = 18;
            return View(await PaginatedList<Mod>.CreateAsync(mods.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        
        //GET: Total Price of all Mods
        public string TotalPrice()
        {
            var mods = from s in _context.Mod
                             select s;

            decimal totalPrice = mods.Sum(x => x.Price);

            var result = string.Format(new System.Globalization.CultureInfo("en-GB"), "{0:C}", totalPrice);

            return result;
        }

        //GET: Mod by ID
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Mod());
            else
            {
                var modModel = await _context.Mod.FindAsync(id);
                if (modModel == null)
                {
                    return NotFound();
                }
                return View(modModel);
            }
        }

        //POST: Added or Updated Mod
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, List<IFormFile> files, Mod modModel)
        {

            foreach (var file in files)
            {

                if (System.IO.File.Exists((modModel.FilePath)))
                {
                    System.IO.File.Delete(modModel.FilePath);
                }

                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\files\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);
                var extension = Path.GetExtension(file.FileName);


                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    modModel = new Mod
                    {
                        ID = id,
                        ModName = modModel.ModName,
                        Price = modModel.Price,
                        ModType = modModel.ModType,
                        Obsolete = modModel.Obsolete,
                        Notes = modModel.Notes,
                        FileName = fileName,
                        FileType = file.ContentType,
                        FileExtension = extension,
                        FilePath = filePath
                    };
                    _context.Mod.Update(modModel);
                    _context.SaveChanges();
                }
            }

            _context.Update(modModel);
            await _context.SaveChangesAsync();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Mod.ToList()) });
        }

        //GET: Mod Invoice
        public async Task<IActionResult> Invoice(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modModel = await _context.Mod
                .FirstOrDefaultAsync(m => m.ID == id);
            if (modModel == null)
            {
                return NotFound();
            }

            return View(modModel);
        }

        //GET: Load DeleteInvoice Popup
        public async Task<IActionResult> DeleteInvoice(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modModel = await _context.Mod
                .FirstOrDefaultAsync(m => m.ID == id);

            return View(modModel);
        }

        //POST: Delete selected Mod
        [HttpPost, ActionName("DeleteInvoice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteInvoiceConfirmed(int id)
        {
            var modModel = await _context.Mod.FindAsync(id);
            if (modModel == null) return null;
            if (System.IO.File.Exists(modModel.FilePath))
            {
                modModel.FileExtension = null;
                modModel.FileName= null;
                modModel.FileType= null;
            }
            System.IO.File.Delete(modModel.FilePath);
            modModel.FilePath = null;

            _context.Mod.Update(modModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Mod.ToList()) });
        }

        //GET: Load DeleteMod Popup
        public async Task<IActionResult> DeleteMod(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modModel = await _context.Mod
                .FirstOrDefaultAsync(m => m.ID == id);

            return View(modModel);
        }

        //POST: Delete selected Mod
        [HttpPost, ActionName("DeleteMod")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteModConfirmed(int id)
        {
            var modModel = await _context.Mod.FindAsync(id);
            if (modModel == null) return null;
            if (System.IO.File.Exists(modModel.FilePath))
            {
                System.IO.File.Delete(modModel.FileName);
            }
            _context.Mod.Remove(modModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Mod.ToList()) });
        }
    }
}
