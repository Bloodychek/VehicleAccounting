using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.Models
{
    public class TypeOfGoods : IBaseObject
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина поля 'Название товара' должно содержать от 3 до 50 символов")]
        [Display(Name = "Название товара")]
        public string nameOfGoods { get; set; }
        ICollection<Goods> _goods { get; set; }
    }
}
