using System.ComponentModel.DataAnnotations;
using VehicleAccounting.Models;

namespace VehicleAccounting.ViewModels
{
    public class GoodsViewModel
    {
        public IEnumerable<Goods> Goods { get; set; }
        public PageViewModel PageViewModel { get; set; }
        [StringLength(50, MinimumLength = 0)]
        public string GoodsName { get; set; }
        public Goods Good { get; set; }
    }
}
