using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.ViewModels
{
    public class RouteViewModel
    {
        public IEnumerable<Models.Route> Routes { get; set; }
        public PageViewModel PageViewModel { get; set; }
        [StringLength(50, MinimumLength = 0)]
        public string RouteName { get; set; }
        public Models.Route Route { get; set; }
    }
}
