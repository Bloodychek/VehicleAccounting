using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.ViewModels
{
    public class ApplicationsEditAndAnyViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Дата загрузки")]
        public DateTime uploadDate { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Дата выгрузки")]
        public DateTime unloadingDate { get; set; }

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
