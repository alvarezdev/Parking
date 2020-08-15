using System;
namespace Parking.Domain
{
    public class VehicleDto
    {
        public string Plate { get; set; }
        public VehicleType VehicleType { get; set; }
        public double CylinderCapacity { get; set; }
        public DateTime VehicleEntryTime { get; set; }
        public DateTime VehicleDepartureTime { get; set; }
    }
}
