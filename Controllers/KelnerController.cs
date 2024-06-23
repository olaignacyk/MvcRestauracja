using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcRestauracja.Data;
using MvcRestauracja.Models;

namespace MvcRestauracja.Controllers
{
    public class KelnerController : Controller
    {
        private readonly MvcRestauracjaContext _context;

        public KelnerController(MvcRestauracjaContext context)
        {
            _context = context;
        }

        // GET: Kelner
        public async Task<IActionResult> Index()
        {
            var kelner = _context.Kelner.Include(p => p.Stoliki).AsNoTracking();
            return View(await kelner.ToListAsync());
        }

        // GET: Kelner/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kelner = await _context.Kelner
                .Include(p => p.Stoliki)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kelner == null)
            {
                return NotFound();
            }

            return View(kelner);
        }

        private void PopulateStolikDropDownList(object selectedStoliki = null)
        {
            IEnumerable<int> selectedStolikiid = null;
            if (Request.HasFormContentType)
            {
                selectedStolikiid = Request.Form["Stoliki"].ToString().Split(',').Select(int.Parse);
            }
            else if (selectedStoliki != null && selectedStoliki is IEnumerable<int>)
            {
                selectedStolikiid = selectedStoliki as IEnumerable<int>;
            }
            ViewBag.StolikiID = new MultiSelectList(_context.Stoliki, "Id", "Nazwa", selectedStolikiid);
        }


        // GET: Kelner/Create
        public IActionResult Create()
        {
            PopulateStolikDropDownList();
            return View();
        }

        // POST: Kelner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,DataZatrudnienia")] Kelner kelner, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                var selectedStoliki = form["Stoliki"].ToString().Split(',').Select(int.Parse).ToList();
                kelner.Stoliki = _context.Stoliki.Where(s => selectedStoliki.Contains(s.Id)).ToList();

                _context.Add(kelner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateStolikDropDownList();
            return View(kelner);
        }

        // GET: Kelner/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kelner = await _context.Kelner
                .Include(k => k.Stoliki)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kelner == null)
            {
                return NotFound();
            }
            PopulateStolikDropDownList(kelner.Stoliki.Select(s => s.Id).ToList());
            return View(kelner);
        }

        // POST: Kelner/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,DataZatrudnienia")] Kelner kelner, IFormCollection form)
        {
            if (id != kelner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var selectedStoliki = form["Stoliki"].ToString().Split(',').Select(int.Parse).ToList();
                    kelner.Stoliki = _context.Stoliki.Where(s => selectedStoliki.Contains(s.Id)).ToList();

                    _context.Update(kelner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KelnerExists(kelner.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateStolikDropDownList(kelner.Stoliki.Select(s => s.Id).ToList());
            return View(kelner);
        }

        // GET: Kelner/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kelner == null)
            {
                return NotFound();
            }

            var kelner = await _context.Kelner
                .Include(p => p.Stoliki)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kelner == null)
            {
                return NotFound();
            }

            return View(kelner);
        }

        // POST: Kelner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kelner == null)
            {
                return Problem("Entity set 'MvcRestauracjaContext.Kelner'  is null.");
            }
            var kelner = await _context.Kelner.FindAsync(id);
            if (kelner != null)
            {
                _context.Kelner.Remove(kelner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KelnerExists(int id)
        {
            return (_context.Kelner?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
