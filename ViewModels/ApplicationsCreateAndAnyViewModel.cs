using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.ViewModels
{
    public class ApplicationsCreateAndAnyViewModel
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Дата загрузки")]
        public DateTime uploadDate { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Дата выгрузки")]
        public DateTime unloadingDate { get; set; }

        [Required(ErrorMessage = "Поле должно содержать хотя бы 1 символ")]
        [Range(1, int.MaxValue)]
        [Display(Name = "Дней на колличество оплаты")]
        public int paymentDayTime { get; set; }

        [StringLength(15, MinimumLength = 2, ErrorMessage = "Длина поля 'Цена' должно содержать от 2 до 15 символов")]
        [Display(Name = "Цена")]
        public string currency { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Номер заявки")]
        public int applicationNumber { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Точка отправления")]
        public int routeId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "ФИО водителя")]
        public int transportId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Цена")]
        public int treatyId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Название заказчика")]
        public int customerId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Название исполнителя")]
        public int orderExecutorId { get; set; }
    }
}
