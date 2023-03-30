using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoPrograWeb_JorgeGarcia_1220019.Models;

namespace ProyectoPrograWeb_JorgeGarcia_1220019.Controllers
{
    public class ClasesController : Controller
    {
        private readonly EscuelawebContext _context;

        public ClasesController()
        {
            _context = new EscuelawebContext();
        }

        // GET: Clases
        public async Task<IActionResult> Index()
        {
            var escuelawebContext = _context.Clases.Include(c => c.IdProfesorNavigation);
            return View(await escuelawebContext.ToListAsync());
        }

        // GET: Clases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clases == null)
            {
                return NotFound();
            }

            var clase = await _context.Clases
                .Include(c => c.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // GET: Clases/Create
        public IActionResult Create()
        {
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "Id", "Nombre");
            return View();
        }

        // POST: Clases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,IdProfesor")] ScholomanceModels.Clase clase)
        {
            if (ModelState.IsValid)
            {
                var _clase = new Models.Clase
                {
                    Nombre = clase.Nombre,
                    IdProfesor = clase.IdProfesor
                };
                _context.Add(_clase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "Id", "Nombre" , clase.IdProfesor);
            return View(clase);
        }

        // GET: Clases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clases == null)
            {
                return NotFound();
            }

            var clase = await _context.Clases.FindAsync(id);
            if (clase == null)
            {
                return NotFound();
            }
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "Id", "Nombre", clase.IdProfesor);
            return View(clase);
        }

        // POST: Clases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,IdProfesor")] ScholomanceModels.Clase clase)
        {
            if (id != clase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var _clase = new Models.Clase
                    {
                        Id = clase.Id,
                        Nombre = clase.Nombre,
                        IdProfesor = clase.IdProfesor
                    };
                    _context.Update(_clase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaseExists(clase.Id))
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
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "Id", "Nombre", clase.IdProfesor);
            return View(clase);
        }

        // GET: Clases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clases == null)
            {
                return NotFound();
            }

            var clase = await _context.Clases
                .Include(c => c.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // POST: Clases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clases == null)
            {
                return Problem("Entity set 'EscuelawebContext.Clases'  is null.");
            }
            var clase = await _context.Clases.FindAsync(id);
            if (clase != null)
            {
                _context.Clases.Remove(clase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaseExists(int id)
        {
          return (_context.Clases?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
