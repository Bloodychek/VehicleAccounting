using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.Models
{
    public class Route : IBaseObject
    {
        public int Id { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessage = "Длина поля 'Точка отправления' должно содержать от 3 до 40 символов")]
        [Display(Name = "Точка отправления")]
        public string departurePoint { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessage = "Длина поля 'Точка прибытия' должно содержать от 3 до 40 символов")]
        [Display(Name = "Точка прибытия")]
        public string arrivalPoint { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessage = "Длина поля 'Место стоянки' должно содержать от 3 до 40 символов")]
        [Display(Name = "Место стоянки")]
        public string stoppingPoint { get; set; }

        ICollection<Application> applications { get; set; }
    }
}
