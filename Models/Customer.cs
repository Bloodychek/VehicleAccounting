using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.Models
{
    public class Customer : IBaseObject
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина поля 'Названия заказчика' должно содержать от 3 до 50 символов")]
        [Display(Name = "Название заказчика")]
        public string customerName { get; set; }

        [RegularExpression(@"(^\+\d{1,2})?((\(\d{3}\))|(\-?\d{3}\-)|(\d{3}))((\d{3}\-\d{4})|(\d{3}\-\d\d\-\d\d)|(\d{7})|(\d{3}\-\d\-\d{3}))", ErrorMessage = "Неверный формат ввода номера телефона")]
        [Display(Name = "Телефон заказчика")]
        public string customerPhone { get; set; }

        ICollection<Application> applications { get; set; }
    }
}
