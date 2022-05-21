using VehicleAccounting.Models;

namespace VehicleAccounting.Repositories
{
    public class TypeOfGoodsRepository: BaseRepository<TypeOfGoods>
    {
        public TypeOfGoodsRepository(MainContext mainContext): base(mainContext) { }
    }
}
