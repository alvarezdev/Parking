using System.Collections.Generic;
using Parking.Domain;

namespace Parking.Data
{
    public class VehicleDaoMock : IVehicleDao
    {
        private List<VehicleModel> vehicleList;

        public VehicleDaoMock()
        {
            vehicleList = new List<VehicleModel>();
        }

        public void AddVehicle(VehicleModel vehicleModel)
        {
            vehicleList.Add(vehicleModel);
        }

        public void DeleteAll()
        {
            if (vehicleList.Count > 0)
            {
                vehicleList.Clear();
            }                           
        }

        public void DeleteVehicle(string plate)
        {
            var vehicle = GetVehicle(plate);
            if  (vehicle != null)
            {
                vehicleList.Remove(vehicle);
            }
                           
        }

        public List<VehicleModel> GetVehicleList()
        {
            return vehicleList;
        }

        public VehicleModel GetVehicle(string plate)
        {
            for (int i = 0; i < vehicleList.Count; i++)
            {
                VehicleModel vehicleDto = vehicleList[i];
                if (vehicleDto.Plate.Equals(plate))                
                    return vehicleDto;                
            }
            return null;
        }
    }
}
