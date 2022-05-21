using VehicleAccounting.Models;

namespace VehicleAccounting.Repositories
{
    public class TransportRepository: BaseRepository<Transport>
    {
        public TransportRepository(MainContext mainContext): base(mainContext) { }
    }
}
