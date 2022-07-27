using CemexProject.Models;
using CemexProject.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace CemexProject.Hubs
{
    public class TruckHub : Hub
    {
        TruckRepository truckRepository;
        public TruckHub(IConfiguration configuration)
        {
            var connectionString = configuration.
                           GetConnectionString("DefaultConnection");
            truckRepository = new TruckRepository(connectionString);
        }
            public async Task SendTrucks()
            {
                var trucks = truckRepository.GetTrucks();
                await Clients.All.SendAsync("ReceivedTrucks", trucks);
            }
            public async Task SendTruck(Truck oldtruck ,Truck newtruck)
            {
                newtruck.LastUpdateDate = DateTime.Now;
                truckRepository.updateTrucks(newtruck);
                await Clients.All.SendAsync("updateTruckLoc", oldtruck ,newtruck);
            }
    }
}
