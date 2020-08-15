using System;
using System.Collections.Generic;

namespace Parking.Domain
{
    public interface IVehicleDao
    {
        void AddVehicle(VehicleDto vehicleDto);
        VehicleDto GetVehicle(string plate);
        List<VehicleDto> GetListVehicle();
        void DeleteVehicle(VehicleDto vehicleDto);
        void DeleteAll();
    }
}
