#region Using statements
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

        public ModsController(BensModManagerContext context)
        {
            _context = context;
        }
        #endregion

        //GET: Mods
        public async Task<IActionResult> Index
            (
            string ModName,
            string ModType,
            string sortOrder,
            int? pageNumber
            )
        {
            ViewData["ModName"] = ModName;
            ViewData["ModType"] = ModType;
            ViewData["CurrentSort"] = sortOrder;

            var mods = from s in _context.Mod
                       orderby s.ModName
                       select s;

            //Search criteria
            if (!String.IsNullOrEmpty(ModName))
            {
                mods = (IOrderedQueryable<Mod>)mods.Where(s => s.ModName.Contains(ModName));
            }

            if (!String.IsNullOrEmpty(ModType))
            {
                mods = (IOrderedQueryable<Mod>)mods.Where(s => s.ModType.Contains(ModType));
            }

            var pageSize = 20;
            return View(await PaginatedList<Mod>.CreateAsync(mods.AsNoTracking(), pageNumber ?? 1, pageSize));
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

        //POST: Added or Updateed Mod
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, List<IFormFile> files, Mod modModel)
        {
            
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
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

        //GET: Mod to Delete
        public async Task<IActionResult> Delete(int? id)
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

        //POST: Delete a Mod
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modModel = await _context.Mod.FindAsync(id);
            _context.Mod.Remove(modModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Mod.ToList()) });
        }
    }
}
