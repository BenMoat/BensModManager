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
	public class ModificationsController : Controller
	{
		private readonly BensModManagerContext _context;

		public ModificationsController(BensModManagerContext context)
		{
			_context = context;
		}

		//GET: Modifications
		public async Task<IActionResult> Index
			(
			string Mod,
			string Price,
			string ModType,
			string sortOrder,
			int? pageNumber
			)
		{
			ViewData["Mod"] = Mod;
			ViewData["Price"] = Price;
			ViewData["ModType"] = ModType;
			ViewData["CurrentSort"] = sortOrder;


			var mods = from s in _context.Modification
					   select s;

			//Search criteria =
			if (!String.IsNullOrEmpty(Mod))
			{
				mods = (IOrderedQueryable<Modification>)mods.Where(s => s.Mod.Contains(Mod));
			}

			if (!String.IsNullOrEmpty(Price))
			{
				mods = (IOrderedQueryable<Modification>)mods.Where(s => s.Price.Contains(Price));
			}

			if (!String.IsNullOrEmpty(ModType))
			{
				mods = (IOrderedQueryable<Modification>)mods.Where(s => s.ModType.Contains(ModType));
			}

			var pageSize = 50;
			return View(await PaginatedList<Modification>.CreateAsync(mods.AsNoTracking(), pageNumber ?? 1, pageSize));
		}

		// GET: Modifications/AddOrEdit(Insert)
		// GET: Modifications/AddOrEdit/(Update)
		[NoDirectAccess]
		public async Task<IActionResult> AddOrEdit(int id = 0)
		{
			if (id == 0)
				return View(new Modification());
			else
			{
				var modificationModel = await _context.Modification.FindAsync(id);
				if (modificationModel == null)
				{
					return NotFound();
				}
				return View(modificationModel);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddOrEdit(int id, [Bind("ID,Mod,Price,ModType")] Modification modificationModel)
		{
			if (ModelState.IsValid)
			{
				//Insert
				if (id == 0)
				{
					_context.Add(modificationModel);
					await _context.SaveChangesAsync();
				}
				//Update
				else
				{
					try
					{
						_context.Update(modificationModel);
						await _context.SaveChangesAsync();
					}
					catch (DbUpdateConcurrencyException)
					{
						if (!ModificationModelExists(modificationModel.ID))
						{ return NotFound(); }
						else
						{ throw; }
					}
				}
				return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Modification.ToList()) });
			}
			return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", modificationModel) });
		}

		// GET: Modifications/Delete
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var modModel = await _context.Modification
				.FirstOrDefaultAsync(m => m.ID == id);
			if (modModel == null)
			{
				return NotFound();
			}

			return View(modModel);
		}

		// POST: Modifications/Delete
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var modificationModel = await _context.Modification.FindAsync(id);
			_context.Modification.Remove(modificationModel);
			await _context.SaveChangesAsync();
			return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Modification.ToList()) });
		}

		private bool ModificationModelExists(int id)
		{
			return _context.Modification.Any(e => e.ID == id);
		}
	}
}
