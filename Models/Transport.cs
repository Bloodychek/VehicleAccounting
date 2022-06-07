using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.Models
{
    public class Transport : IBaseObject
    {
        /// <summary>
        /// Класс, реализующий сущность "Транспорт"
        /// </summary>
        public int Id { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessage = "Длина поля 'Тягач' должно содержать от 3 до 40 символов")]
        [Display(Name = "Тягач")]
        public string tractor { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина поля 'Марка тягача' должно содержать от 3 до 50 символов")]
        [Display(Name = "Марка тягача")]
        public string tractorBrand { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessage = "Длина поля 'Полуприцеп' должно содержать от 3 до 40 символов")]
        [Display(Name = "Полуприцеп")]
        public string semitrailer { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина поля 'Марка полуприцепа' должно содержать от 3 до 50 символов")]
        [Display(Name = "Марка полуприцепа")]
        public string semi_trailerBrand { get; set; }

        [StringLength(60, MinimumLength = 2, ErrorMessage = "Длина поля 'ФИО водителя' должно содержать от 2 до 60 символов")]
        [Display(Name = "ФИО водителя")]
        public string driverFIO { get; set; }

        ICollection<Application> applications { get; set; }
    }
}
