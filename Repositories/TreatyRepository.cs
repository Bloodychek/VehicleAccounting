using VehicleAccounting.Models;

namespace VehicleAccounting.Repositories
{
    public class TreatyRepository: BaseRepository<Treaty>
    {
        public TreatyRepository(MainContext mainContext): base(mainContext) { }
    }
}
