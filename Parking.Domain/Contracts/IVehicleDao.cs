using System.Collections.Generic;

namespace Parking.Domain
{
    public interface IVehicleDao
    {
        void AddVehicle(VehicleModel vehicleDto);
        VehicleModel GetVehicle(string plate);
        List<VehicleModel> GetVehicleList();
        void DeleteVehicle(string plate);
        void DeleteAll();
    }
}
