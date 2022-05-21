#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleAccounting;
using VehicleAccounting.Models;
using VehicleAccounting.Repositories;
using VehicleAccounting.ViewModels;

namespace VehicleAccounting.Controllers
{
    public class TreatiesController : Controller
    {
        private readonly IRepository<Treaty> treatyRepository;
        private readonly MainContext context;

        public TreatiesController(MainContext context, IRepository<Treaty> treatyRepository)
        {
            this.treatyRepository = treatyRepository;
            this.context = context;
        }

        // GET: Treaties
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<Treaty> source = treatyRepository.GetAll();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TreatyViewModel viewModel = new TreatyViewModel
            {
                PageViewModel = pageViewModel,
                Treaties = items
            };
            return View(viewModel);
        }

        // GET: Treaties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treaty = await treatyRepository.GetById(id.Value);
            if (treaty == null)
            {
                return NotFound();
            }

            return View(treaty);
        }

        // GET: Treaties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Treaties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,paymentDayTime,currency")] Treaty treaty)
        {
            if (ModelState.IsValid)
            {
                await treatyRepository.Create(treaty);
                await treatyRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(treaty);
        }

        // GET: Treaties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treaty = await treatyRepository.GetById(id.Value);
            if (treaty == null)
            {
                return NotFound();
            }
            return View(treaty);
        }

        // POST: Treaties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,paymentDayTime,currency")] Treaty treaty)
        {
            if (id != treaty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await treatyRepository.Update(treaty);
                    await treatyRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreatyExists(treaty.Id))
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
            return View(treaty);
        }

        // GET: Treaties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treaty = await treatyRepository.GetById(id.Value);
            if (treaty == null)
            {
                return NotFound();
            }

            return View(treaty);
        }

        // POST: Treaties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var treaty = await treatyRepository.GetById(id);
            await treatyRepository.Delete(id);
            await treatyRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool TreatyExists(int id)
        {
            return treatyRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
