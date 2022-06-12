using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.ViewModels
{
    public class GoodsEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Номер заявки")]
        public int applicationId { get; set; }

        [Display(Name = "Тип товара")]
        public int typeOfGoodsId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Количество товаров")]
        public int countOfGoods { get; set; }

        [StringLength(15, MinimumLength = 2, ErrorMessage = "Длина поля 'Цена товара' должно содержать от 2 до 15 символов")]
        [Display(Name = "Цена товара")]
        public string productPrice { get; set; }
    }
}
