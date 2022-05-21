using VehicleAccounting.Models;

namespace VehicleAccounting.Repositories
{
    public class ApplicationRepository : BaseRepository<Application>
    {
        public ApplicationRepository(MainContext mainContext) : base(mainContext)
        {

        }
    }
}
