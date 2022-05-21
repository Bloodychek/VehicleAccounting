using VehicleAccounting.Models;

namespace VehicleAccounting.Repositories
{
    public class GoodsRepository: BaseRepository<Goods>
    {
        public GoodsRepository(MainContext mainContext): base(mainContext) { }
    }
}
