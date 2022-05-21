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
    public class CustomersController : Controller
    {
        private readonly MainContext context;
        private readonly IRepository<Customer> customerRepository;

        public CustomersController(MainContext context, IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
            this.context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<Customer> source = customerRepository.GetAll();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            CustomerViewModel viewModel = new CustomerViewModel
            {
                PageViewModel = pageViewModel,
                Customers = items
            };
            return View(viewModel);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerRepository.GetById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,customerName,customerPhone,customerFeedback")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await customerRepository.Create(customer);
                await customerRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerRepository.GetById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,customerName,customerPhone, customerFeedback")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await customerRepository.Update(customer);
                    await customerRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerRepository.GetById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await customerRepository.GetById(id);
            await customerRepository.Delete(id);
            await customerRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return customerRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
