using System.Collections.Generic;
using Parking.Domain;

namespace Parking.Data
{
    public class VehicleDaoMock : IVehicleDao
    {
        private List<VehicleDto> vehicleList;

        public VehicleDaoMock()
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
            {
                vehicleList.Clear();
            }                           
        }

        public void DeleteVehicle(VehicleDto vehicleDto)
        {
            if (GetVehicle(vehicleDto.Plate) != null)
            {
                vehicleList.Remove(vehicleDto);
            }
                           
        }

        public List<VehicleDto> GetVehicleList()
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
