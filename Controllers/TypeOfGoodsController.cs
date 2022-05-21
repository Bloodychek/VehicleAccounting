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
    public class TypeOfGoodsController : Controller
    {
        private readonly IRepository<TypeOfGoods> typeOfGoodsRepository;
        private readonly MainContext context;

        public TypeOfGoodsController(MainContext context, IRepository<TypeOfGoods> typeOfGoodsRepository)
        {
            this.typeOfGoodsRepository = typeOfGoodsRepository;
            this.context = context;
        }

        // GET: TypeOfGoods
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<TypeOfGoods> source = typeOfGoodsRepository.GetAll();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TypeOfGoodsViewModel viewModel = new TypeOfGoodsViewModel
            {
                PageViewModel = pageViewModel,
                TypeOfGoods = items
            };
            return View(viewModel);
        }

        // GET: TypeOfGoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfGoods = await typeOfGoodsRepository.GetById(id.Value);
            if (typeOfGoods == null)
            {
                return NotFound();
            }

            return View(typeOfGoods);
        }

        // GET: TypeOfGoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfGoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nameOfGoods,countOfGoods,unit")] TypeOfGoods typeOfGoods)
        {
            if (ModelState.IsValid)
            {
                await typeOfGoodsRepository.Create(typeOfGoods);
                await typeOfGoodsRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfGoods);
        }

        // GET: TypeOfGoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfGoods = await typeOfGoodsRepository.GetById(id.Value);
            if (typeOfGoods == null)
            {
                return NotFound();
            }
            return View(typeOfGoods);
        }

        // POST: TypeOfGoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nameOfGoods,countOfGoods,unit")] TypeOfGoods typeOfGoods)
        {
            if (id != typeOfGoods.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await typeOfGoodsRepository.Update(typeOfGoods);
                    await typeOfGoodsRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfGoodsExists(typeOfGoods.Id))
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
            return View(typeOfGoods);
        }

        // GET: TypeOfGoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfGoods = await typeOfGoodsRepository.GetById(id.Value);
            if (typeOfGoods == null)
            {
                return NotFound();
            }

            return View(typeOfGoods);
        }

        // POST: TypeOfGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfGoods = await typeOfGoodsRepository.GetById(id);
            await typeOfGoodsRepository.Delete(id);
            await typeOfGoodsRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfGoodsExists(int id)
        {
            return typeOfGoodsRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
