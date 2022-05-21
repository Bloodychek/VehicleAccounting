using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.Models
{
    public class TypeOfGoods : IBaseObject
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина поля 'Название товара' должно содержать от 3 до 50 символов")]
        [Display(Name = "Название товара")]
        public string nameOfGoods { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Количество товаров")]
        public int countOfGoods { get; set; }

        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина поля 'Единица измерения' должно содержать от 3 до 25 символов")]
        [Display(Name = "Единица измерения")]
        public string unit { get; set; }
        ICollection<Goods> _goods { get; set; }
    }
}
