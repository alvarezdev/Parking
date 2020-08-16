using System;
using Android.Arch.Lifecycle;
using Parking.Domain;

namespace Parking.Droid
{
    public class AddVehicleViewModel : ViewModel
    {
        private readonly ParkingDomain parkingDomain;

        public AddVehicleViewModel()
        {
            parkingDomain = BaseApplication.Resolve<ParkingDomain>();
        }

        public void AddVehicle(VehicleDto vehicleDto)
        {
            parkingDomain.EnterParking(vehicleDto);
        }
    }

}
