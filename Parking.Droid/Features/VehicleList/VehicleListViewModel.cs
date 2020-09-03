using System;
using System.Collections.Generic;
using Android.Arch.Lifecycle;
using Parking.Domain;

namespace Parking.Droid
{
    public class VehicleListViewModel : ViewModel
    {
        private readonly ParkingDomainServices parkingDomain;

        public VehicleListViewModel()
        {
            parkingDomain = BaseApplication.Resolve<ParkingDomainServices>();
        }

        public List<VehicleModel> GetUserList()
        {
            return parkingDomain.GetVehicleList();
        }

        public void DeleteVehicle(string plate)
        {
            parkingDomain.LeaveVehicle(plate);
        }

        public int CalculateValueParking(VehicleType vehicleType, double cylinderCapacity, DateTime vehicleEntryTime, DateTime vehicleDepartureTime)
        {
            return parkingDomain.CalculateValueParking(vehicleType, cylinderCapacity, vehicleEntryTime, vehicleDepartureTime);
        }
    }
}
