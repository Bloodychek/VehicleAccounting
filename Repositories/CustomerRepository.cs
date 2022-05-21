using VehicleAccounting.Models;

namespace VehicleAccounting.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(MainContext mainContext): base(mainContext) { }
    }
}
