using System.ComponentModel.DataAnnotations;
using VehicleAccounting.Models;

namespace VehicleAccounting.ViewModels
{
    public class TreatyViewModel
    {
        public IEnumerable<Treaty> Treaties { get; set; }
        public PageViewModel PageViewModel { get; set; }
        [StringLength(50, MinimumLength = 0)]
        public string TreatyName { get; set; }
        public Treaty Treaty { get; set; }
    }
}
