using System.ComponentModel.DataAnnotations;
using VehicleAccounting.Models;

namespace VehicleAccounting.ViewModels
{
    public class OrderExecutorViewModel
    {
        public IEnumerable<OrderExecutor> OrderExecutors { get; set; }
        public PageViewModel PageViewModel { get; set; }
        [StringLength(50, MinimumLength = 0)]
        public string OrderExecutorName { get; set; }
        public OrderExecutor OrderExecutor { get; set; }
    }
}
