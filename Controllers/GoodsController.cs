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
    public class GoodsController : Controller
    {
        private readonly IRepository<Application> applicationRepository;
        private readonly IRepository<Goods> goodsRepository;
        private readonly IRepository<TypeOfGoods> typeOfGoodsRepository;
        private readonly MainContext context;

        public GoodsController(MainContext context, IRepository<Application> applicationRepository, IRepository<Goods> goodsRepository, IRepository<TypeOfGoods> typeOfGoodsRepository)
        {
            this.applicationRepository = applicationRepository;
            this.goodsRepository = goodsRepository;
            this.typeOfGoodsRepository = typeOfGoodsRepository;
            this.context = context;
        }

        // GET: Goods
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<Goods> source = goodsRepository.GetAll(k => k.TypeOfGood, b => b.Application);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            GoodsViewModel viewModel = new GoodsViewModel
            {
                PageViewModel = pageViewModel,
                Goods = items
            };
            return View(viewModel);
        }

        // GET: Goods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            Goods source = goodsRepository.GetAll(k => k.TypeOfGood, b => b.Application).FirstOrDefault(n => n.Id == id.Value);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        // GET: Goods/Create
        public IActionResult Create()
        {
            ViewData["TypeOfGoods"] = new SelectList(typeOfGoodsRepository.GetAll().ToList(), "Id", "nameOfGoods");
            ViewData["Applications"] = new SelectList(applicationRepository.GetAll().ToList(), "Id", "applicationNumber");
            return View(new GoodsCreateViewModel());
        }

        // POST: Goods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("applicationId,typeOfGoodsId,countOfGoods,productPrice")] GoodsCreateViewModel goods)
        {
            if (ModelState.IsValid)
            {
                var newGoods = new Goods
                {
                    applicationId = goods.applicationId,
                    typeOfGoodsId = goods.typeOfGoodsId,
                    countOfGoods = goods.countOfGoods,
                    productPrice = goods.productPrice
                };
                await goodsRepository.Create(newGoods);
                await goodsRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(goods);
        }

        // GET: Goods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["TypeOfGoods"] = new SelectList(typeOfGoodsRepository.GetAll().ToList(), "Id", "nameOfGoods");
            ViewData["Applications"] = new SelectList(applicationRepository.GetAll().ToList(), "Id", "applicationNumber");
            if (id == null)
            {
                return NotFound();
            }

            var goods = await goodsRepository.GetAll(m => m.TypeOfGood, a => a.Application).FirstOrDefaultAsync(n => n.Id == id.Value);
            if (goods == null)
            {
                return NotFound();
            }
            return View(new GoodsEditViewModel
            {
                Id = goods.Id,
                applicationId = goods.applicationId,
                typeOfGoodsId = goods.typeOfGoodsId,
                countOfGoods = goods.countOfGoods,
                productPrice = goods.productPrice
            });
        }

        // POST: Goods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,applicationId,typeOfGoodsId,countOfGoods,productPrice")] GoodsEditViewModel goodsEditView)
        {
            if (id != goodsEditView.Id)
            {
                return NotFound();
            }

            var newGoods = new Goods
            {
                Id = goodsEditView.Id,
                applicationId = goodsEditView.applicationId,
                typeOfGoodsId = goodsEditView.typeOfGoodsId,
                countOfGoods = goodsEditView.countOfGoods,
                productPrice = goodsEditView.productPrice
            };

            if (ModelState.IsValid)
            {
                try
                {
                    await goodsRepository.Update(newGoods);
                    await goodsRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodsExists(newGoods.Id))
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
            ViewData["TypeOfGoods"] = new SelectList(typeOfGoodsRepository.GetAll().ToList(), "Id", "nameOfGoods");
            ViewData["Applications"] = new SelectList(applicationRepository.GetAll().ToList(), "Id", "applicationNumber");
            return View(goodsEditView);
        }

        // GET: Goods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Goods source = goodsRepository.GetAll(k => k.TypeOfGood, b => b.Application).FirstOrDefault(n => n.Id == id.Value);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        // POST: Goods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goods = await goodsRepository.GetById(id);
            await goodsRepository.Delete(id);
            await goodsRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool GoodsExists(int id)
        {
            return goodsRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
