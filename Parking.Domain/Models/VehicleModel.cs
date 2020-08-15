using System;

namespace Parking.Domain
{
    public class VehicleModel
    {
        private const string nullVehiclePlateMsg = "Vehicle plate can't null or empty";
        private const string zeroCylinderCapacityMsg = "The cylinder capacity of the vehicle must be greater than zero";

        public string Plate { get; set; }
        public VehicleType VehicleType { get; set; }
        public double CylinderCapacity { get; set; }
        public DateTime VehicleEntryTime { get; set; }
        public DateTime VehicleDepartureTime { get; set; }

        public VehicleModel(VehicleDto vehicleDto)
        {
            Plate = vehicleDto.Plate;
            VehicleType = vehicleDto.VehicleType;
            CylinderCapacity = vehicleDto.CylinderCapacity;
            VehicleEntryTime = vehicleDto.VehicleEntryTime;
        }

        internal void ValidateData(VehicleModel vehicle)
        {
            if (string.IsNullOrEmpty(vehicle.Plate))            
                throw new BusinessException(nullVehiclePlateMsg);            
            if (vehicle.CylinderCapacity <= 0)            
                throw new BusinessException(zeroCylinderCapacityMsg);            
        }
    }
}
