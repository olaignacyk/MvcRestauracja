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
    public class StolikController : Controller
    {
        private readonly MvcRestauracjaContext _context;

        public StolikController(MvcRestauracjaContext context)
        {
            _context = context;
            
        }

        // GET: Stolik
        public async Task<IActionResult> Index()
        {
              return _context.Stolik != null ? 
                          View(await _context.Stolik.ToListAsync()) :
                          Problem("Entity set 'MvcRestauracjaContext.Stolik'  is null.");
        }

        // GET: Stolik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stolik == null)
            {
                return NotFound();
            }

            var stolik = await _context.Stolik
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stolik == null)
            {
                return NotFound();
            }

            return View(stolik);
        }

        // GET: Stolik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stolik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Nakrycie")] Stolik stolik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stolik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stolik);
        }

        // GET: Stolik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stolik == null)
            {
                return NotFound();
            }

            var stolik = await _context.Stolik.FindAsync(id);
            if (stolik == null)
            {
                return NotFound();
            }
            return View(stolik);
        }

        // POST: Stolik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Nakrycie")] Stolik stolik)
        {
            if (id != stolik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stolik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StolikExists(stolik.Id))
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
            return View(stolik);
        }

        // GET: Stolik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stolik == null)
            {
                return NotFound();
            }

            var stolik = await _context.Stolik
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stolik == null)
            {
                return NotFound();
            }

            return View(stolik);
        }

        // POST: Stolik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stolik == null)
            {
                return Problem("Entity set 'MvcRestauracjaContext.Stolik'  is null.");
            }
            var stolik = await _context.Stolik.FindAsync(id);
            if (stolik != null)
            {
                _context.Stolik.Remove(stolik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StolikExists(int id)
        {
          return (_context.Stolik?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
