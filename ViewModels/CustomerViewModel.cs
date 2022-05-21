using System.ComponentModel.DataAnnotations;
using VehicleAccounting.Models;

namespace VehicleAccounting.ViewModels
{
    public class CustomerViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public PageViewModel PageViewModel { get; set; }
        [StringLength(50, MinimumLength = 0)]
        public string CustomerName { get; set; }
        public Customer Customer { get; set; }
    }
}
