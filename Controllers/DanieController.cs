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
    public class DanieController : Controller
    {
        private readonly MvcRestauracjaContext _context;

        public DanieController(MvcRestauracjaContext context)
        {
            _context = context;
        }

        // GET: Danie
        public async Task<IActionResult> Index()
        {
            
            return _context.Danie != null ?
                        View(await _context.Danie.ToListAsync()) :
                        Problem("Entity set 'MvcRestauracjaContext.Danie'  is null.");
        }

        // GET: Danie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Danie == null)
            {
                return NotFound();
            }

            var danie = await _context.Danie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (danie == null)
            {
                return NotFound();
            }

            return View(danie);
        }

        // GET: Danie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Danie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Cena")] Danie danie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danie);
        }

        // GET: Danie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Danie == null)
            {
                return NotFound();
            }

            var danie = await _context.Danie.FindAsync(id);
            if (danie == null)
            {
                return NotFound();
            }
            return View(danie);
        }

        // POST: Danie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Cena")] Danie danie)
        {
            if (id != danie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanieExists(danie.Id))
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
            return View(danie);
        }

        // GET: Danie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Danie == null)
            {
                return NotFound();
            }

            var danie = await _context.Danie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (danie == null)
            {
                return NotFound();
            }

            return View(danie);
        }

        // POST: Danie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Danie == null)
            {
                return Problem("Entity set 'MvcRestauracjaContext.Danie'  is null.");
            }
            var danie = await _context.Danie.FindAsync(id);
            if (danie != null)
            {
                _context.Danie.Remove(danie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanieExists(int id)
        {
            return (_context.Danie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
