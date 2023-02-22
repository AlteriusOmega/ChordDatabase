using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChordDatabase.Data;
using ChordDatabase.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace ChordDatabase.Controllers
{
    public class ChordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Chords
        public async Task<IActionResult> Index()
        {
              return _context.Chord != null ? 
                          View(await _context.Chord.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Chord'  is null.");
        }

        // GET: Search
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }


        // POST: Search Results
        public ViewResult ShowSearchResults(
            String Quality,
            String Extension,
            String Alterations,
            String Inversion,
            String Scale,
            String Functions,
            String Feeling)
        {
            //Func<Chord, bool> chordFilter = chord => 
            //Quality != null? chord.Quality.Contains(Quality): false
            //&& Extension != null ?  chord.Extension.Contains(Extension): false
            //&& Alterations != null ? chord.Alterations.Contains(Alterations) : false
            //&& Inversion != null ? chord.Inversion.Contains(Inversion) : false
            //&& Scale != null ? chord.Scale.Contains(Scale) : false
            //&& Functions != null ? chord.Functions.Contains(Functions) : false
            //&& Feeling != null ? chord.Feeling.Contains(Feeling) : false;

            Func<Chord, bool> chordFilter = chord =>
            (Quality == null || chord.Quality?.Contains(Quality) == true) && 
            (Extension == null || chord.Extension?.Contains(Extension) == true) &&// If search term Extension is null, this line is true, if it's not, then move on to check if chord.Extension is null (that's what the little ? is doing) and if it's not then do the .Contains to check if it contains the search term Extension
            (Alterations == null || chord.Alterations?.Contains(Alterations) == true) &&
            (Inversion == null || chord.Inversion?.Contains(Inversion) == true) &&
            (Scale == null || chord.Scale?.Contains(Scale) == true) &&
            (Functions == null || chord.Functions?.Contains(Functions) == true) &&
            (Feeling == null || chord.Feeling?.Contains(Feeling) == true);

            var chords = _context.Chord.Where(chordFilter).ToList();
            return View("Index", chords);
        }

        // GET: Chords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chord == null)
            {
                return NotFound();
            }

            var chord = await _context.Chord
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chord == null)
            {
                return NotFound();
            }

            return View(chord);
        }

        // GET: Chords/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quality,Extension,Alterations,Inversion,Scale,Functions,Feeling")] Chord chord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chord);
        }

        // GET: Chords/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chord == null)
            {
                return NotFound();
            }

            var chord = await _context.Chord.FindAsync(id);
            if (chord == null)
            {
                return NotFound();
            }
            return View(chord);
        }

        // POST: Chords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quality,Extension,Alterations,Inversion,Scale,Functions,Feeling")] Chord chord)
        {
            if (id != chord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChordExists(chord.Id))
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
            return View(chord);
        }

        // GET: Chords/Delete/5
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Chord == null)
            {
                return NotFound();
            }

            var chord = await _context.Chord
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chord == null)
            {
                return NotFound();
            }

            return View(chord);
        }

        // POST: Chords/Delete/5
        [Authorize]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Chord == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Chord'  is null.");
            }
            var chord = await _context.Chord.FindAsync(id);
            if (chord != null)
            {
                _context.Chord.Remove(chord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChordExists(int id)
        {
          return (_context.Chord?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
