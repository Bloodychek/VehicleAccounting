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
using VehicleAccounting.Reports;
using VehicleAccounting.Repositories;
using VehicleAccounting.ViewModels;
using Route = VehicleAccounting.Models.Route;

namespace VehicleAccounting.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IRepository<Application> applicationRepository;
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<OrderExecutor> orderExecutorsRepository;
        private readonly IRepository<Route> routeRepository;
        private readonly IRepository<Transport> transportRepository;
        private readonly IRepository<Treaty> treatyRepository;
        private readonly MainContext _context;
        private readonly ReportApplication reportApplication = new ReportApplication();

        public ApplicationsController(MainContext context, IRepository<Application> applicationRepository, IRepository<Customer> customerRepository,
            IRepository<OrderExecutor> orderExecutorsRepository, IRepository<Route> routeRepository, IRepository<Transport> transportRepository, IRepository<Treaty> treatyRepository)
        {
            this.applicationRepository = applicationRepository;
            this.customerRepository = customerRepository;
            this.orderExecutorsRepository = orderExecutorsRepository;
            this.routeRepository = routeRepository;
            this.transportRepository = transportRepository;
            this.treatyRepository = treatyRepository;
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<Application> source = applicationRepository.GetAll(k => k.Customer, q => q.OrderExecutor,
                w => w.Route, e => e.Transport, r => r.Treaty).AsNoTracking();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            ApplicationViewModel viewModel = new ApplicationViewModel
            {
                PageViewModel = pageViewModel,
                Applications = items
            };
            return View(viewModel);
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application newApplication = applicationRepository.GetAll(k => k.Customer, q => q.OrderExecutor,
                w => w.Route, e => e.Transport, r => r.Treaty).FirstOrDefault(n => n.Id == id.Value);
            if (newApplication == null)
            {
                return NotFound();
            }

            return View(newApplication);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["Customers"] = new SelectList(customerRepository.GetAll().ToList(), "Id", "customerName");
            ViewData["OrderExecutors"] = new SelectList(orderExecutorsRepository.GetAll().ToList(), "Id", "orderExecutorName");
            ViewData["Routes"] = new SelectList(routeRepository.GetAll().ToList(), "Id", "departurePoint");
            ViewData["Transports"] = new SelectList(transportRepository.GetAll().ToList(), "Id", "driverFIO");
            ViewData["Treaties"] = new SelectList(treatyRepository.GetAll().ToList(), "Id", "currency");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,uploadDate,unloadingDate,applicationNumber,routeId,transportId,treatyId,customerId,orderExecutorId")] ApplicationsCreateAndAnyViewModel applications)
        {
            if (ModelState.IsValid)
            {
                try {
                    var newApplications = new Application
                    {
                        uploadDate = applications.uploadDate,
                        unloadingDate = applications.unloadingDate,
                        applicationNumber = applications.applicationNumber,
                        routeId = applications.routeId,
                        transportId = applications.transportId,
                        treatyId = applications.treatyId,
                        customerId = applications.customerId,
                        orderExecutorId = applications.orderExecutorId
                    };
                    DateValidation(newApplications);
                    await applicationRepository.Create(newApplications);
                    await applicationRepository.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewData["Customers"] = new SelectList(customerRepository.GetAll().ToList(), "Id", "customerName");
            ViewData["OrderExecutors"] = new SelectList(orderExecutorsRepository.GetAll().ToList(), "Id", "orderExecutorName");
            ViewData["Routes"] = new SelectList(routeRepository.GetAll().ToList(), "Id", "departurePoint");
            ViewData["Transports"] = new SelectList(transportRepository.GetAll().ToList(), "Id", "driverFIO");
            ViewData["Treaties"] = new SelectList(treatyRepository.GetAll().ToList(), "Id", "currency");
            return View(applications);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Customers"] = new SelectList(customerRepository.GetAll().ToList(), "Id", "customerName");
            ViewData["OrderExecutors"] = new SelectList(orderExecutorsRepository.GetAll().ToList(), "Id", "orderExecutorName");
            ViewData["Routes"] = new SelectList(routeRepository.GetAll().ToList(), "Id", "departurePoint");
            ViewData["Transports"] = new SelectList(transportRepository.GetAll().ToList(), "Id", "driverFIO");
            ViewData["Treaties"] = new SelectList(treatyRepository.GetAll().ToList(), "Id", "currency");
            if (id == null)
            {
                return NotFound();
            }

            var applications = await applicationRepository.GetAll(k => k.Customer, q => q.OrderExecutor,
                w => w.Route, e => e.Transport, r => r.Treaty).FirstOrDefaultAsync(n => n.Id == id.Value);
            if (applications == null)
            {
                return NotFound();
            }
            return View(new ApplicationsEditAndAnyViewModel
            {
                Id = applications.Id,
                uploadDate = applications.uploadDate,
                unloadingDate = applications.unloadingDate,
                applicationNumber = applications.applicationNumber,
                routeId = applications.routeId.Value,
                transportId = applications.transportId.Value,
                treatyId = applications.treatyId.Value,
                customerId = applications.customerId.Value,
                orderExecutorId = applications.orderExecutorId.Value
            });
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,uploadDate,unloadingDate,applicationNumber,routeId,transportId,treatyId,customerId,orderExecutorId")] ApplicationsEditAndAnyViewModel applicationsEditView)
        {
            if (id != applicationsEditView.Id)
            {
                return NotFound();
            }

            var newApplication = new Application
            {
                Id = applicationsEditView.Id,
                uploadDate = applicationsEditView.uploadDate,
                unloadingDate = applicationsEditView.unloadingDate,
                applicationNumber = applicationsEditView.applicationNumber,
                routeId = applicationsEditView.routeId,
                transportId = applicationsEditView.transportId,
                treatyId = applicationsEditView.treatyId,
                customerId = applicationsEditView.customerId,
                orderExecutorId = applicationsEditView.orderExecutorId
            };

            if (ModelState.IsValid)
            {
                try
                {
                    DateValidation(newApplication);
                    await applicationRepository.Update(newApplication);
                    await applicationRepository.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(newApplication.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewData["Customers"] = new SelectList(customerRepository.GetAll().ToList(), "Id", "customerName");
            ViewData["OrderExecutors"] = new SelectList(orderExecutorsRepository.GetAll().ToList(), "Id", "orderExecutorName");
            ViewData["Routes"] = new SelectList(routeRepository.GetAll().ToList(), "Id", "departurePoint");
            ViewData["Transports"] = new SelectList(transportRepository.GetAll().ToList(), "Id", "driverFIO");
            ViewData["Treaties"] = new SelectList(treatyRepository.GetAll().ToList(), "Id", "currency");
            return View(applicationsEditView);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Application source = await applicationRepository.GetAll(k => k.Customer, q => q.OrderExecutor,
                w => w.Route, e => e.Transport, r => r.Treaty).FirstOrDefaultAsync(n => n.Id == id.Value);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await applicationRepository.GetById(id);
            await applicationRepository.Delete(id);
            await applicationRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Report()
        {
            const string fileName = "applications.xlsx";
            var bytes = reportApplication.Report(applicationRepository.GetAll(k => k.Customer, q => q.OrderExecutor,
                w => w.Route, e => e.Transport, r => r.Treaty).AsNoTracking());
            return File(bytes, "application/force-download", fileName);
        }

        public void DateValidation(Application application)
        {
                if (application.uploadDate > application.unloadingDate)
                {
                    throw new ApplicationException("Дата загрузки не может совпадать или быть такой же как дата выгрузки");
                }            
        }

        private bool ApplicationExists(int id)
        {
            return applicationRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
