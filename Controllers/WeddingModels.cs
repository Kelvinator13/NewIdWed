using IdWedNu.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace IdWedNu.Controllers
{
    public class WeddingModelController
    {
        private readonly ApplicationDbContext _context;

        public WeddingModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WeddingModels
        public async Task<IActionResult> Index()
        {
            PredictiveModel model = new PredictiveModel(_context);


            //Find out what guest is using the app at the current moment
            /* var guest = _context.Guests.Where(/***here is where you figure that part out, ask an instructor**//*);*/

            string sampleGUID = "35asd684as3da8sd43ads68a4sd3";
            var guest = _context.Guests.Where(c => c.GuestModelPrimaryKey== sampleGUID).FirstOrDefault();
            var listOfRecomendations = model.GetWeddingRecomendations(guest
                );
            return View(listOfRecomendations);
        }

        // GET: WeddingModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingModel = await _context.Weddings
                .FirstOrDefaultAsync(m => m.WeddingModelPrimaryKey == id);
            if (weddingModel == null)
            {
                return NotFound();
            }

            return View(weddingModel);
        }

        // GET: WeddingModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeddingModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeddingModelPrimaryKey,WeddingName,WeddingPhone,AddressKey,PriceRangeIndex,WebsiteUrl,Open_now,Lat,Lng,Price_level,Rating,Place_Id")] WeddingModel weddingModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weddingModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weddingModel);
        }

        // GET: WeddingModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingModel = await _context.Weddings.FindAsync(id);
            if (weddingModel == null)
            {
                return NotFound();
            }
            return View(weddingModel);
        }

        // POST: WeddingModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("WeddingModelPrimaryKey,WeddingName,WeddingPhone,AddressKey,PriceRangeIndex,WebsiteUrl,Open_now,Lat,Lng,Price_level,Rating,Place_Id")] WeddingModel weddingModel)
        {
            if (id != weddingModel.WeddingModelPrimaryKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weddingModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeddingModelExists(weddingModel.WeddingModelPrimaryKey))
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
            return View(weddingModel);
        }

        // GET: WeddingModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingModel = await _context.Weddings
                .FirstOrDefaultAsync(m => m.WeddingModelPrimaryKey == id);
            if (weddingModel == null)
            {
                return NotFound();
            }

            return View(weddingModel);
        }

        // POST: WeddingModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var weddingModel = await _context.Weddings.FindAsync(id);
            _context.Wedding.Remove(weddingModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeddingModelExists(string id)
        {
            return _context.Weddings.Any(e => e.WeddingModelPrimaryKey == id);
        }

        private class PredictiveModel
        {
            private ApplicationDbContext context;

            public PredictiveModel(ApplicationDbContext context)
            {
                this.context = context;
            }

            internal object GetWeddingRecomendations(object guest)
            {
                throw new NotImplementedException();
            }
        }
    }
}
