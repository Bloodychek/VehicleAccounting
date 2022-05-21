using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.Models
{
    public class OrderExecutor : IBaseObject
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина поля 'Название исполнителя' должно содержать от 3 до 50 символов")]
        [Display(Name = "Название исполнителя")]
        public string orderExecutorName { get; set; }

        [RegularExpression(@"(^\+\d{1,2})?((\(\d{3}\))|(\-?\d{3}\-)|(\d{3}))((\d{3}\-\d{4})|(\d{3}\-\d\d\-\d\d)|(\d{7})|(\d{3}\-\d\-\d{3}))", ErrorMessage = "Неверный формат ввода номера телефона")]
        [Display(Name = "Телефон исполнителя")]
        public string orderExecutorPhone { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина поля 'Обратная связь' должно содержать от 3 до 50 символов")]
        [Display(Name = "Обратная связь")]
        public string orderExecutorFeedback { get; set; }

        ICollection<Application> applications { get; set; }
    }
}
