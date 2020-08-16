using System;
using System.Collections.Generic;
using Parking.Domain;

namespace Parking.Data
{
    public class VehiculeDaoMock : IVehicleDao
    {
        private List<VehicleDto> vehicleList;

        public VehiculeDaoMock()
        {
            vehicleList = new List<VehicleDto>();
        }

        public void AddVehicle(VehicleDto vehicleDto)
        {
            vehicleList.Add(vehicleDto);
        }

        public void DeleteAll()
        {
            if (vehicleList.Count > 0)            
                vehicleList.Clear();            
        }

        public void DeleteVehicle(VehicleDto vehicleDto)
        {
            if (GetVehicle(vehicleDto.Plate) != null)            
                vehicleList.Remove(vehicleDto);            
        }

        public List<VehicleDto> GetListVehicle()
        {
            return vehicleList;
        }

        public VehicleDto GetVehicle(string plate)
        {
            for (int i = 0; i < vehicleList.Count; i++)
            {
                VehicleDto vehicleDto = vehicleList[i];
                if (vehicleDto.Plate.Equals(plate))                
                    return vehicleDto;                
            }
            return null;
        }
    }
}
