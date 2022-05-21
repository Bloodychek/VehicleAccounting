using VehicleAccounting.Models;
namespace VehicleAccounting.Repositories
{
    public class RouteRepository: BaseRepository<Models.Route>
    {
        public RouteRepository(MainContext mainContext): base(mainContext) { }
    }
}
