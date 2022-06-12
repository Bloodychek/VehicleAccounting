using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.Models
{
    public class TypeOfGoods : IBaseObject
    {
        /// <summary>
        /// Класс, реализующий сущность "Продукция"
        /// </summary>
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина поля 'Название товара' должно содержать от 3 до 50 символов")]
        [Display(Name = "Название продукции")]
        public string nameOfGoods { get; set; }

        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина поля 'Единица измерения' должно содержать от 2 до 25 символов")]
        [Display(Name = "Единица измерения")]
        public string unit { get; set; }
        ICollection<Goods> _goods { get; set; }
    }
}
