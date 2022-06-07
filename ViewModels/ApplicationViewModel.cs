using System.ComponentModel.DataAnnotations;
using VehicleAccounting.Models;

namespace VehicleAccounting.ViewModels
{
    public class ApplicationViewModel
    {
        public IEnumerable<Application> Applications { get; set; }
        public PageViewModel PageViewModel { get; set; }
        [StringLength(50, MinimumLength = 0)]
        public string ApplicationName { get; set; }
        public Application Application { get; set; }
        public ApplicationReportViewModel ApplicationReport { get; set; }
    }
}
