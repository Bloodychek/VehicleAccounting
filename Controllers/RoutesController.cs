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
using Route = VehicleAccounting.Models.Route;

namespace VehicleAccounting.Controllers
{
    public class RoutesController : Controller
    {
        private readonly IRepository<Route> routeRepository;
        private readonly MainContext context;

        public RoutesController(MainContext context, IRepository<Route> routeRepository)
        {
            this.routeRepository = routeRepository;
            this.context = context;
        }

        // GET: Routes
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<Models.Route> source = routeRepository.GetAll();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            RouteViewModel viewModel = new RouteViewModel
            {
                PageViewModel = pageViewModel,
                Routes = items
            };
            return View(viewModel);
        }

        // GET: Routes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await routeRepository.GetById(id.Value);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,departurePoint,arrivalPoint,stoppingPoint")] Models.Route route)
        {
            if (ModelState.IsValid)
            {
                await routeRepository.Create(route);
                await routeRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await routeRepository.GetById(id.Value);
            if (route == null)
            {
                return NotFound();
            }
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,departurePoint,arrivalPoint,stoppingPoint")] Models.Route route)
        {
            if (id != route.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await routeRepository.Update(route);
                    await routeRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.Id))
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
            return View(route);
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await routeRepository.GetById(id.Value);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var route = await routeRepository.GetById(id);
            await routeRepository.Delete(id);
            await routeRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(int id)
        {
            return routeRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
