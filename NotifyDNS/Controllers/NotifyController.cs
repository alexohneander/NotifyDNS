using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotifyDNS.Core;
using NotifyDNS.Core.Models;

namespace NotifyDNS.Controllers
{
    public class NotifyController : Controller
    {
        private readonly NotifyDNSDbContext _context;

        public NotifyController(NotifyDNSDbContext context)
        {
            _context = context;
        }

        // GET: Notify
        public async Task<IActionResult> Index()
        {
            return View(await _context.NotifyModels.ToListAsync());
        }

        // GET: Notify/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifyModel = await _context.NotifyModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notifyModel == null)
            {
                return NotFound();
            }

            return View(notifyModel);
        }

        // GET: Notify/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notify/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Domain,CurrentIP,DestinationIP,IsScheduled")] NotifyModel notifyModel)
        {
            if (ModelState.IsValid)
            {
                notifyModel.Id = Guid.NewGuid();
                _context.Add(notifyModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notifyModel);
        }

        // GET: Notify/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifyModel = await _context.NotifyModels.FindAsync(id);
            if (notifyModel == null)
            {
                return NotFound();
            }
            return View(notifyModel);
        }

        // POST: Notify/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Domain,CurrentIP,DestinationIP,IsScheduled")] NotifyModel notifyModel)
        {
            if (id != notifyModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notifyModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotifyModelExists(notifyModel.Id))
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
            return View(notifyModel);
        }

        // GET: Notify/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifyModel = await _context.NotifyModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notifyModel == null)
            {
                return NotFound();
            }

            return View(notifyModel);
        }

        // POST: Notify/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var notifyModel = await _context.NotifyModels.FindAsync(id);
            _context.NotifyModels.Remove(notifyModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotifyModelExists(Guid id)
        {
            return _context.NotifyModels.Any(e => e.Id == id);
        }
    }
}
