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
    public class LivroController : Controller
    {
        private readonly BibliotecaContext _context;

        public LivroController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Livro
        public async Task<IActionResult> Index()
        {
            var bibliotecaContext = _context.Livro.Include(l => l.Editora).Include(l => l.Genero);
            return View(await bibliotecaContext.ToListAsync());
        }

        // GET: Livro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Livro == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .Include(l => l.Editora)
                .Include(l => l.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livro/Create
        public IActionResult Create()
        {
            ViewData["EditoraId"] = new SelectList(_context.Set<Editora>(), "Id", "Nome");
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Nome");
            return View();
        }

        // POST: Livro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,ISBN,DataLancamento,ImagemCapa,Autor,EditoraId,GeneroId")] Livro livro)
        {
            var file = Request.Form.Files["ImagemCapa"];
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                livro.ImagemCapa = memoryStream.ToArray();
            }
            ModelState.Clear();
            TryValidateModel(livro);
            ModelState.Remove("Genero");
            ModelState.Remove("Editora");
            if (ModelState.IsValid)
            {
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EditoraId"] = new SelectList(_context.Set<Editora>(), "Id", "Nome", livro.EditoraId);
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Nome", livro.GeneroId);
            return View(livro);
        }

        // GET: Livro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Livro == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            ViewData["EditoraId"] = new SelectList(_context.Set<Editora>(), "Id", "Nome", livro.EditoraId);
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Nome", livro.GeneroId);
            return View(livro);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,ISBN,DataLancamento,Autor,ImagemCapa,EditoraId,GeneroId")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            var file = Request.Form.Files["ImagemCapa"];
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                livro.ImagemCapa = memoryStream.ToArray();
            }
            ModelState.Clear();
            TryValidateModel(livro);
            ModelState.Remove("Genero");
            ModelState.Remove("Editora");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
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
            ViewData["EditoraId"] = new SelectList(_context.Set<Editora>(), "Id", "Nome", livro.EditoraId);
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Nome", livro.GeneroId);
            return View(livro);
        }

        // GET: Livro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Livro == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .Include(l => l.Editora)
                .Include(l => l.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Livro == null)
            {
                return Problem("Entity set 'BibliotecaContext.Livro'  is null.");
            }
            var livro = await _context.Livro.FindAsync(id);
            if (livro != null)
            {
                _context.Livro.Remove(livro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
          return _context.Livro.Any(e => e.Id == id);
        }
    }
}
