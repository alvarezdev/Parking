using Android.Arch.Lifecycle;
using Parking.Domain;

namespace Parking.Droid
{
    public class AddVehicleViewModel : ViewModel
    {
        private readonly ParkingDomainServices parkingDomain;

        public AddVehicleViewModel()
        {
            parkingDomain = BaseApplication.Resolve<ParkingDomainServices>();
        }

        public void AddVehicle(VehicleDto vehicleDto)
        {
            parkingDomain.EnterParking(vehicleDto);
        }
    }

}
