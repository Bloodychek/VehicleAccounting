using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.ViewModels
{
    public class GoodsCreateViewModel
    {
        [Display(Name = "Номер заявки")]
        public int applicationId { get; set; }

        [Display(Name = "Тип товара")]
        public int typeOfGoodsId { get; set; }
    }
}
