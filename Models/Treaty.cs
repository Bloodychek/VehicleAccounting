using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.Models
{
    public class Treaty : IBaseObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно содержать хотя бы 1 символ")]
        [Range(1, int.MaxValue)]
        [Display(Name = "Дней на колличество оплаты")]
        public int paymentDayTime { get; set; }

        [StringLength(15, MinimumLength = 2, ErrorMessage = "Длина поля 'Цена' должно содержать от 2 до 15 символов")]
        [Display(Name = "Цена")]
        public string currency { get; set; }

        ICollection<Application> applications { get; set; }
    }
}
