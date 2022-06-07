#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly MainContext _context;
        private readonly ReportApplication reportApplication = new ReportApplication();

        public ApplicationsController(MainContext context, IRepository<Application> applicationRepository, IRepository<Customer> customerRepository,
            IRepository<OrderExecutor> orderExecutorsRepository, IRepository<Route> routeRepository, IRepository<Transport> transportRepository)
        {
            this.applicationRepository = applicationRepository;
            this.customerRepository = customerRepository;
            this.orderExecutorsRepository = orderExecutorsRepository;
            this.routeRepository = routeRepository;
            this.transportRepository = transportRepository;
            _context = context;
        }

        /// <summary>
        /// Метод Index для таблицы Application
        /// </summary>
        /// <param name="page">Объект, используемый для пагинации</param>
        /// <returns></returns>
        // GET: Applications
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<Application> source = applicationRepository.GetAll(k => k.Customer, q => q.OrderExecutor,
                w => w.Route, e => e.Transport).AsNoTracking();
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

        /// <summary>
        /// Метод для подробной информации в таблице Application
        /// </summary>
        /// <param name="id">Ид</param>
        /// <returns></returns>
        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application newApplication = applicationRepository.GetAll(k => k.Customer, q => q.OrderExecutor,
                w => w.Route, e => e.Transport).FirstOrDefault(n => n.Id == id.Value);
            if (newApplication == null)
            {
                return NotFound();
            }

            return View(newApplication);
        }

        /// <summary>
        /// Get метод для создания записи
        /// </summary>
        /// <returns></returns>
        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["Customers"] = new SelectList(customerRepository.GetAll().ToList(), "Id", "customerName");
            ViewData["OrderExecutors"] = new SelectList(orderExecutorsRepository.GetAll().ToList(), "Id", "orderExecutorName");
            ViewData["Routes"] = new SelectList(routeRepository.GetAll().ToList(), "Id", "departurePoint");
            ViewData["Transports"] = new SelectList(transportRepository.GetAll().ToList(), "Id", "driverFIO");
            return View();
        }

        /// <summary>
        /// Post метод для создания записи
        /// </summary>
        /// <param name="applications">Объект класса ApplicationsCreateAndAnyViewModel</param>
        /// <returns></returns>
        // POST: Applications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,uploadDate,unloadingDate,paymentDayTime,currency,applicationNumber,routeId,transportId,customerId,orderExecutorId")] ApplicationsCreateAndAnyViewModel applications)
        {
            if (ModelState.IsValid)
            {
                try {
                    var newApplications = new Application
                    {
                        uploadDate = applications.uploadDate,
                        unloadingDate = applications.unloadingDate,
                        paymentDayTime = applications.paymentDayTime,
                        currency = applications.currency,
                        applicationNumber = applications.applicationNumber,
                        routeId = applications.routeId,
                        transportId = applications.transportId,
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
            return View(applications);
        }

        /// <summary>
        /// Get метод для редактирования
        /// </summary>
        /// <param name="id">Ид</param>
        /// <returns></returns>
        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Customers"] = new SelectList(customerRepository.GetAll().ToList(), "Id", "customerName");
            ViewData["OrderExecutors"] = new SelectList(orderExecutorsRepository.GetAll().ToList(), "Id", "orderExecutorName");
            ViewData["Routes"] = new SelectList(routeRepository.GetAll().ToList(), "Id", "departurePoint");
            ViewData["Transports"] = new SelectList(transportRepository.GetAll().ToList(), "Id", "driverFIO");
            if (id == null)
            {
                return NotFound();
            }

            var applications = await applicationRepository.GetAll(k => k.Customer, q => q.OrderExecutor,
                w => w.Route, e => e.Transport).FirstOrDefaultAsync(n => n.Id == id.Value);
            if (applications == null)
            {
                return NotFound();
            }
            return View(new ApplicationsEditAndAnyViewModel
            {
                Id = applications.Id,
                uploadDate = applications.uploadDate,
                unloadingDate = applications.unloadingDate,
                paymentDayTime = applications.paymentDayTime,
                currency = applications.currency,
                applicationNumber = applications.applicationNumber,
                routeId = applications.routeId.Value,
                transportId = applications.transportId.Value,
                customerId = applications.customerId.Value,
                orderExecutorId = applications.orderExecutorId.Value
            });
        }

        /// <summary>
        /// Post метод для редактирования
        /// </summary>
        /// <param name="id">Ид</param>
        /// <param name="applicationsEditView">Объект класса ApplicationsEditAndAnyViewModel</param>
        /// <returns></returns>
        // POST: Applications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,uploadDate,unloadingDate,paymentDayTime,currency,applicationNumber,routeId,transportId,customerId,orderExecutorId")] ApplicationsEditAndAnyViewModel applicationsEditView)
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
                paymentDayTime = applicationsEditView.paymentDayTime,
                currency = applicationsEditView.currency,
                applicationNumber = applicationsEditView.applicationNumber,
                routeId = applicationsEditView.routeId,
                transportId = applicationsEditView.transportId,
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
            return View(applicationsEditView);
        }

        /// <summary>
        /// Get метод для удаления
        /// </summary>
        /// <param name="id">Ид</param>
        /// <returns></returns>
        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Application source = await applicationRepository.GetAll(k => k.Customer, q => q.OrderExecutor,
                w => w.Route, e => e.Transport).FirstOrDefaultAsync(n => n.Id == id.Value);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        /// <summary>
        /// Post метод для удаления
        /// </summary>
        /// <param name="id">Ид</param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод для создания отчетов
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Главный бухгалтер")]
        public async Task<ActionResult> Report(ApplicationReportViewModel applicationReportViewModel)
        {
            DateValidationForReport(applicationReportViewModel);
            var newDate = applicationRepository.GetAll(k => k.Customer, q => q.OrderExecutor,
                w => w.Route, e => e.Transport).AsNoTracking().Where(x => x.uploadDate >= applicationReportViewModel.uploadDate && x.unloadingDate <= applicationReportViewModel.unloadingDate);
            const string fileName = "applications.xlsx";
            var bytes = reportApplication.Report(newDate);
            return File(bytes, "application/force-download", fileName);
        }

        /// <summary>
        /// Метод для проверки даты в таблице
        /// </summary>
        /// <param name="application">Объект класса Application</param>
        /// <exception cref="ApplicationException"> Класс для вывода обрабатываемой ошибки</exception>
        public void DateValidation(Application application)
        {
                if (application.uploadDate > application.unloadingDate)
                {
                    throw new ApplicationException("Дата выгрузки не может быть произведена раньше чем дата загрузки");
                }            
        }

        public void DateValidationForReport(ApplicationReportViewModel applicationReportViewModel)
        {
            if (applicationReportViewModel.uploadDate == default || applicationReportViewModel.unloadingDate == default || applicationReportViewModel.uploadDate > applicationReportViewModel.unloadingDate)
            {
                throw new ApplicationException("Дата выгрузки не может быть произведена раньше чем дата загрузки");
            }
        }

        /// <summary>
        /// Метод для проверки наличия записи
        /// </summary>
        /// <param name="id">Ид</param>
        /// <returns></returns>
        private bool ApplicationExists(int id)
        {
            return applicationRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
