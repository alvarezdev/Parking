using System;
using Parking.Domain;
using Realms;

namespace Parking.Data
{
    public class VehicleEntity : RealmObject
    {
        public string Plate { get; set; }
        public string VehicleType { get; set; }
        public double CylinderCapacity { get; set; }
        public long VehicleEntryTime { get; set; }

        public VehicleEntity() { }

        public VehicleEntity(VehicleDto vehicleDto)
        {
            Plate = vehicleDto.Plate;
            VehicleType = vehicleDto.VehicleType.ToString();            
            CylinderCapacity = vehicleDto.CylinderCapacity;
            VehicleEntryTime = (long)vehicleDto.VehicleEntryTime.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}
