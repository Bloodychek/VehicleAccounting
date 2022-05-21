using ArrayToExcel;
using VehicleAccounting.Models;

namespace VehicleAccounting.Reports
{
    public class ReportApplication
    {
        public byte [] Report(IEnumerable<Application> report)
        {
            return report.ToExcel(schema => schema
            .AddColumn("Дата загрузки", x => x.uploadDate)
            .AddColumn("Дата выгрузки", x => x.unloadingDate)
            .AddColumn("Номер заявки", x => x.applicationNumber)
            .AddColumn("Точка отправления", x => x.Route?.departurePoint?? "Пустое поле")
            .AddColumn("ФИО водителя", x => x.Transport?.driverFIO ?? "Пустое поле")
            .AddColumn("Цена", x => x.Treaty?.currency ?? "Пустое поле")
            .AddColumn("Название заказчика", x => x.Customer?.customerName ?? "Пустое поле")
            .AddColumn("Название исполнителя", x => x.OrderExecutor?.orderExecutorName ?? "Пустое поле"));
        }
    }
}
