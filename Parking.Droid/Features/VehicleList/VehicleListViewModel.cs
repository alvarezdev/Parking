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

        public List<VehicleDto> GetUserList()
        {
            return parkingDomain.GetVehicleList();
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
