using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data;

namespace Fisilti.MVC.Controllers
{
    public class PromptsController : Controller
    {
        private readonly FisiltiDbContext _context;

        public PromptsController()
        {
            _context = new FisiltiDbContext();
        }

        // GET: Prompts
        public async Task<IActionResult> Index()
        {
            var fisiltiDbContext = _context.Prompts.Include(p => p.Category);
            return View(await fisiltiDbContext.ToListAsync());
        }

        // GET: Prompts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prompt = await _context.Prompts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prompt == null)
            {
                return NotFound();
            }

            return View(prompt);
        }

        // GET: Prompts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Prompts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Content,Price,CategoryId,Id,CreatedDate,UpdatedDate,IsActive")] Prompt prompt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prompt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", prompt.CategoryId);
            return View(prompt);
        }

        // GET: Prompts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prompt = await _context.Prompts.FindAsync(id);
            if (prompt == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", prompt.CategoryId);
            return View(prompt);
        }

        // POST: Prompts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Content,Price,CategoryId,Id,CreatedDate,UpdatedDate,IsActive")] Prompt prompt)
        {
            if (id != prompt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prompt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromptExists(prompt.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", prompt.CategoryId);
            return View(prompt);
        }

        // GET: Prompts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prompt = await _context.Prompts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prompt == null)
            {
                return NotFound();
            }

            return View(prompt);
        }

        // POST: Prompts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prompt = await _context.Prompts.FindAsync(id);
            if (prompt != null)
            {
                _context.Prompts.Remove(prompt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromptExists(int id)
        {
            return _context.Prompts.Any(e => e.Id == id);
        }
    }
}
