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
    public class OrderExecutorsController : Controller
    {
        private readonly IRepository<OrderExecutor> orderExecutorsRepository;
        private readonly MainContext context;

        public OrderExecutorsController(MainContext context, IRepository<OrderExecutor> orderExecutorsRepository)
        {
            this.orderExecutorsRepository = orderExecutorsRepository;
            this.context = context;
        }

        // GET: OrderExecutors
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<OrderExecutor> source = orderExecutorsRepository.GetAll();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            OrderExecutorViewModel viewModel = new OrderExecutorViewModel
            {
                PageViewModel = pageViewModel,
                OrderExecutors = items
            };
            return View(viewModel);
        }

        // GET: OrderExecutors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderExecutor = await orderExecutorsRepository.GetById(id.Value);
            if (orderExecutor == null)
            {
                return NotFound();
            }

            return View(orderExecutor);
        }

        // GET: OrderExecutors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderExecutors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,orderExecutorName,orderExecutorPhone")] OrderExecutor orderExecutor)
        {
            if (ModelState.IsValid)
            {
                await orderExecutorsRepository.Create(orderExecutor);
                await orderExecutorsRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(orderExecutor);
        }

        // GET: OrderExecutors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderExecutor = await orderExecutorsRepository.GetById(id.Value);
            if (orderExecutor == null)
            {
                return NotFound();
            }
            return View(orderExecutor);
        }

        // POST: OrderExecutors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,orderExecutorName,orderExecutorPhone")] OrderExecutor orderExecutor)
        {
            if (id != orderExecutor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await orderExecutorsRepository.Update(orderExecutor);
                    await orderExecutorsRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExecutorExists(orderExecutor.Id))
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
            return View(orderExecutor);
        }

        // GET: OrderExecutors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderExecutor = await orderExecutorsRepository.GetById(id.Value);
            if (orderExecutor == null)
            {
                return NotFound();
            }

            return View(orderExecutor);
        }

        // POST: OrderExecutors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderExecutor = await orderExecutorsRepository.GetById(id);
            await orderExecutorsRepository.Delete(id);
            await orderExecutorsRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExecutorExists(int id)
        {
            return orderExecutorsRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
