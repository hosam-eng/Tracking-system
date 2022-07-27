using CemexProject.Models;
using System.Data;

namespace CemexProject.Repositories
{
    public interface ITruckRepository
    {
        List<Truck> GetTrucks();
        DataTable GetTrucksFromDb();

        void updateTrucks(Truck truck);
    }
}
