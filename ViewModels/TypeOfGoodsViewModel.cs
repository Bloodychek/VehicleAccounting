using System.ComponentModel.DataAnnotations;
using VehicleAccounting.Models;

namespace VehicleAccounting.ViewModels
{
    public class TypeOfGoodsViewModel
    {
        public IEnumerable<TypeOfGoods> TypeOfGoods { get; set; }
        public PageViewModel PageViewModel { get; set; }
        [StringLength(50, MinimumLength = 0)]
        public string TypeOfGoodsName { get; set; }
        public TypeOfGoods TypeOfGood { get; set; }
    }
}
