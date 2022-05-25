using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.ViewModels
{
    public class GoodsCreateViewModel
    {
        [Display(Name = "Номер заявки")]
        public int applicationId { get; set; }

        [Display(Name = "Тип товара")]
        public int typeOfGoodsId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Количество товаров")]
        public int countOfGoods { get; set; }
    }
}
