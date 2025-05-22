using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using online.Models;
using PagedList.Core;

namespace online.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTintucsController : Controller
    {
        private readonly newDataContext _context;

        public AdminTintucsController(newDataContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminTintucs
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var IsTintucs = _context.Tintucs
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);
            PagedList<Tintuc> models = new PagedList<Tintuc>(IsTintucs, pageSize, pageNumber);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminTintucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tintucs == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintucs
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tintuc == null)
            {
                return NotFound();
            }

            return View(tintuc);
        }

        // GET: Admin/AdminTintucs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTintucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] Tintuc tintuc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tintuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tintuc);
        }

        // GET: Admin/AdminTintucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tintucs == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintucs.FindAsync(id);
            if (tintuc == null)
            {
                return NotFound();
            }
            return View(tintuc);
        }

        // POST: Admin/AdminTintucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] Tintuc tintuc)
        {
            if (id != tintuc.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tintuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TintucExists(tintuc.PostId))
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
            return View(tintuc);
        }

        // GET: Admin/AdminTintucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tintucs == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintucs
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tintuc == null)
            {
                return NotFound();
            }

            return View(tintuc);
        }

        // POST: Admin/AdminTintucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tintucs == null)
            {
                return Problem("Entity set 'newDataContext.Tintucs'  is null.");
            }
            var tintuc = await _context.Tintucs.FindAsync(id);
            if (tintuc != null)
            {
                _context.Tintucs.Remove(tintuc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TintucExists(int id)
        {
          return (_context.Tintucs?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
