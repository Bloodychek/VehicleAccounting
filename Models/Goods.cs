using System.ComponentModel.DataAnnotations;

namespace VehicleAccounting.Models
{
    public class Goods : IBaseObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Номер заявки")]
        public int applicationId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Продукция")]
        public int typeOfGoodsId { get; set; }

        public TypeOfGoods TypeOfGood { get; set; }
        public Application Application { get; set; }
    }
}
