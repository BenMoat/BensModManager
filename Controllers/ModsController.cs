#region using statements
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BensModManager.Data;
using BensModManager.Models;
using static BensModManager.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
#endregion

namespace BensModManager.Controllers
{
	public class ModsController : Controller
	{
		private readonly BensModManagerContext _context;

		public ModsController(BensModManagerContext context)
		{
			_context = context;
		}

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

			var pageSize = 50;
			return View(await PaginatedList<Mod>.CreateAsync(mods.AsNoTracking(), pageNumber ?? 1, pageSize));
		}

		// GET: Mods/AddOrEdit(Insert)
		// GET: Mods/AddOrEdit/(Update)
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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddOrEdit(int id, [Bind("ID,ModName,Price,ModType")] Mod modModel)
		{
			if (ModelState.IsValid)
			{
				//Insert
				if (id == 0)
				{
					_context.Add(modModel);
					await _context.SaveChangesAsync();
				}
				//Update
				else
				{
					try
					{
						_context.Update(modModel);
						await _context.SaveChangesAsync();
					}
					catch (DbUpdateConcurrencyException)
					{
						if (!ModModelExists(modModel.ID))
						{ return NotFound(); }
						else
						{ throw; }
					}
				}
				return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Mod.ToList()) });
			}
			return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", modModel) });
		}

		// GET: Mods/Delete
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

		// POST: Mods/Delete
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var modModel = await _context.Mod.FindAsync(id);
			_context.Mod.Remove(modModel);
			await _context.SaveChangesAsync();
			return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Mod.ToList()) });
		}

		private bool ModModelExists(int id)
		{
			return _context.Mod.Any(e => e.ID == id);
		}
	}
}
