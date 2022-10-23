using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Context;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class EditoraController : Controller
    {
        private readonly BibliotecaContext _context;

        public EditoraController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Editora
        public async Task<IActionResult> Index()
        {
              return View(await _context.Editora.ToListAsync());
        }

        // GET: Editora/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Editora == null)
            {
                return NotFound();
            }

            var editora = await _context.Editora
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editora == null)
            {
                return NotFound();
            }

            return View(editora);
        }

        // GET: Editora/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Editora editora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(editora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(editora);
        }

        // GET: Editora/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Editora == null)
            {
                return NotFound();
            }

            var editora = await _context.Editora.FindAsync(id);
            if (editora == null)
            {
                return NotFound();
            }
            return View(editora);
        }

        // POST: Editora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Editora editora)
        {
            if (id != editora.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditoraExists(editora.Id))
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
            return View(editora);
        }

        // GET: Editora/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Editora == null)
            {
                return NotFound();
            }

            var editora = await _context.Editora
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editora == null)
            {
                return NotFound();
            }

            return View(editora);
        }

        // POST: Editora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Editora == null)
            {
                return Problem("Entity set 'BibliotecaContext.Editora'  is null.");
            }
            var editora = await _context.Editora.FindAsync(id);
            if (editora != null)
            {
                _context.Editora.Remove(editora);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditoraExists(int id)
        {
          return _context.Editora.Any(e => e.Id == id);
        }
    }
}
