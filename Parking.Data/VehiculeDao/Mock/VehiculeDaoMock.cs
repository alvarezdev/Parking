using System;
using System.Collections.Generic;
using Parking.Domain;

namespace Parking.Data
{
    public class VehiculeDaoMock : IVehicleDao
    {
        public VehiculeDaoMock()
        {
        }

        public void AddVehicle(VehicleDto vehicleDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public void DeleteVehicle(VehicleDto vehicleDto)
        {
            throw new NotImplementedException();
        }

        public List<VehicleDto> GetListVehicle()
        {
            throw new NotImplementedException();
        }

        public VehicleDto GetVehicle(string plate)
        {
            throw new NotImplementedException();
        }
    }
}
