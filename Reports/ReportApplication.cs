using ArrayToExcel;
using VehicleAccounting.Models;

namespace VehicleAccounting.Reports
{
    public class ReportApplication
    {
        /// <summary>
        /// Метод который формирует отчеты
        /// </summary>
        /// <param name="report">Объект класса Application</param>
        /// <returns></returns>
        public byte [] Report(IEnumerable<Application> report)
        {
            return report.ToExcel(schema => schema
            .AddColumn("Дата загрузки", x => x.uploadDate)
            .AddColumn("Дата выгрузки", x => x.unloadingDate)
            .AddColumn("Дней на колличество оплаты", x => x.paymentDayTime)
            .AddColumn("Итоговая цена", x => x.currency)
            .AddColumn("Номер заявки", x => x.applicationNumber)
            .AddColumn("Точка отправления", x => x.Route?.departurePoint?? "Пустое поле")
            .AddColumn("ФИО водителя", x => x.Transport?.driverFIO ?? "Пустое поле")
            .AddColumn("Название заказчика", x => x.Customer?.customerName ?? "Пустое поле")
            .AddColumn("Название исполнителя", x => x.OrderExecutor?.orderExecutorName ?? "Пустое поле"));
        }
    }
}
