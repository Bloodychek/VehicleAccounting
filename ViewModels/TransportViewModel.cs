using System.ComponentModel.DataAnnotations;
using VehicleAccounting.Models;

namespace VehicleAccounting.ViewModels
{
    public class TransportViewModel
    {
        public IEnumerable<Transport> Transports { get; set; }
        public PageViewModel PageViewModel { get; set; }
        [StringLength(50, MinimumLength = 0)]
        public string TransportsName { get; set; }
        public Transport Transport { get; set; }
    }
}
