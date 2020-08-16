using System;
using System.Collections.Generic;
using Android.Arch.Lifecycle;
using Parking.Domain;

namespace Parking.Droid
{
    public class VehicleListViewModel : ViewModel
    {
        private readonly ParkingDomain parkingDomain;

        public VehicleListViewModel()
        {
            parkingDomain = BaseApplication.Resolve<ParkingDomain>();
        }

        public List<VehicleDto> GetUserList()
        {
            return parkingDomain.GetListVehicle();
        }

        public void DeleteVehicle(VehicleDto vehicleDto)
        {
            parkingDomain.LeaveVehicle(vehicleDto);
        }

        public int CalculateValueParking(VehicleDto vehicleDto)
        {
            return parkingDomain.CalculateValueParking(vehicleDto);
        }
    }
}
