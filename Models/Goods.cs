using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.Models
{
    public class Goods : IBaseObject
    {
        /// <summary>
        /// Класс, реализующий сущность "Продукция в заявках"
        /// </summary>
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Номер заявки")]
        public int applicationId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Продукция")]
        public int typeOfGoodsId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Количество товаров")]
        public int countOfGoods { get; set; }

        [StringLength(15, MinimumLength = 2, ErrorMessage = "Длина поля 'Цена товара' должно содержать от 2 до 15 символов")]
        [Display(Name = "Цена товара")]
        public string productPrice { get; set; }

        public TypeOfGoods TypeOfGood { get; set; }
        public Application Application { get; set; }
    }
}
