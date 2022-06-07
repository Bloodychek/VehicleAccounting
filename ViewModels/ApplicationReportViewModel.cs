using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.ViewModels
{
    public class ApplicationReportViewModel
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Дата загрузки")]
        public DateTime uploadDate { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Дата выгрузки")]
        public DateTime unloadingDate { get; set; }
    }
}
