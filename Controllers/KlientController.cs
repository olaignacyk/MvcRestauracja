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
    public class KlientController : Controller
    {
        private readonly MvcRestauracjaContext _context;

        public KlientController(MvcRestauracjaContext context)
        {
            _context = context;
        }

        // GET: Klient
        public async Task<IActionResult> Index()
        {
            var klient = _context.Klient.Include(p => p.Stolik).Include(p => p.Dania).AsNoTracking();
            return View(await klient.ToListAsync());
        }

        // GET: Klient/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klient = await _context.Klient
                .Include(p => p.Stolik)
                .Include(p => p.Dania)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klient == null)
            {
                return NotFound();
            }


            return View(klient);
        }
        private void PopulateDaniaDropDownList(object selectedDania = null)
        {
            IEnumerable<int> selectedDaniaId = null;
            if (Request.HasFormContentType)
            {
                selectedDaniaId = Request.Form["Dania"].ToString().Split(',').Select(int.Parse);
            }
            else if (selectedDania != null && selectedDaniaId is IEnumerable<int>)
            {
                selectedDaniaId = selectedDania as IEnumerable<int>;
            }
            ViewBag.DaniaID = new MultiSelectList(_context.Dania, "Id", "Nazwa", selectedDaniaId);
        }

        private void PopulateStolikDropDownList(int? selectedStolik = null)
        {
            var wybranyStolik = from e in _context.Stolik
                                orderby e.Nazwa
                                select e;
            var res = wybranyStolik.AsNoTracking();
            ViewBag.StolikID = new SelectList(res, "Id", "Nazwa", selectedStolik);
        }

        public async Task<IActionResult> DanieStatystyki()
        {
            // Pobranie wszystkich klientów z ich zamówieniami
            var klienci = await _context.Klient.Include(k => k.Dania).ToListAsync();

            // Zliczenie liczby zamówień dla każdego dania
            var zamowioneDania = klienci
                .SelectMany(k => k.Dania)
                .GroupBy(d => d.Nazwa)
                .Select(g => new DanieStatystykiViewModel
                {
                    NazwaDania = g.Key,
                    LiczbaZamowien = klienci.Count()
                })
                .ToList();

            return View("DanieStatystyki", zamowioneDania);
        }







        // GET: Klient/Create
        public IActionResult Create()
        {
            PopulateDaniaDropDownList();
            PopulateStolikDropDownList();
            return View();
        }
        // POST: Klient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,DataZamowienia")] Klient klient, IFormCollection form)
        {
            string stolikValue = form["Stolik"].ToString();
            if (ModelState.IsValid)
            {
                var selectedDania = new List<int>();

                if (!string.IsNullOrEmpty(form["Dania"]))
                {
                    selectedDania = form["Dania"].ToString().Split(',')
                                    .Where(s => int.TryParse(s, out _))
                                    .Select(int.Parse)
                                    .ToList();
                }
                klient.Dania = _context.Dania.Where(s => selectedDania.Contains(s.Id)).ToList();

                Stolik stolik = null;
                if (stolikValue != "-1")
                {
                    var ee = _context.Stolik.Where(e => e.Id == int.Parse(stolikValue));

                }
                klient.Stolik = stolik;
                _context.Add(klient);
                await _context.SaveChangesAsync();

                // Aktualizujemy statystyki dania i przekazujemy do widoku
                return await DanieStatystyki();

                // return View("DanieStatystyki", statystykiDania); // Tutaj przekazujemy do widoku statystyki dania
            }
            PopulateStolikDropDownList();
            return View(klient);
        }


        // GET: Klient/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klient = _context.Klient.Where(p => p.Id == id)
                    .Include(p => p.Stolik).Include(p => p.Dania).First();
            if (klient == null)
            {
                return NotFound();
            }
            PopulateStolikDropDownList(klient.Stolik?.Id);

            PopulateDaniaDropDownList(klient.Dania.Select(s => s.Id));
            return View(klient);
        }

        // POST: Klient/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Klient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,DataZamowienia")] Klient klient, IFormCollection form)
        {
            if (id != klient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    String stolikValue = form["Stolik"];
                    Stolik stolik = null;
                    if (!string.IsNullOrEmpty(stolikValue) && stolikValue != "-1")
                    {
                        var ee = _context.Stolik.Where(e => e.Id == int.Parse(stolikValue));
                        if (ee.Count() > 0)
                            stolik = ee.First();
                    }

                    var selectedDania = form["Dania"].ToString().Split(',').Select(int.Parse).ToList();
                    klient.Dania = _context.Dania.Where(s => selectedDania.Contains(s.Id)).ToList();
                    klient.Stolik = stolik;
                    _context.Update(klient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlientExists(klient.Id))
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
            return View(klient);
        }


        // GET: Klient/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Klient == null)
            {
                return NotFound();
            }

            var klient = await _context.Klient
                .Include(p => p.Stolik)
                .Include(p => p.Dania)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient);
        }

        // POST: Klient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var klient = await _context.Klient
                    .Include(k => k.Dania) // Uwzględnij powiązane dania
                    .FirstOrDefaultAsync(k => k.Id == id);

                if (klient == null)
                {
                    return NotFound();
                }

                // Usuń powiązane dania
                if (klient.Dania != null)
                {
                    klient.Dania.Clear();
                }

                _context.Klient.Remove(klient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Problem($"An error occurred while deleting the client: {ex.Message}");
            }
        }



        private bool KlientExists(int id)
        {
            return (_context.Klient?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}