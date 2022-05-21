using VehicleAccounting.Models;

namespace VehicleAccounting.Repositories
{
    public class OrderExecutorRepository: BaseRepository<OrderExecutor>
    {
        public OrderExecutorRepository(MainContext mainContext) : base(mainContext) { }
    }
}
