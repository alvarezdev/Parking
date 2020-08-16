using System;
using Parking.Data;
using Parking.Domain;
using Unity;

namespace Parking.Droid
{
    public class DependencyInjection
    {
        public static void RegisterTypes(IUnityContainer diContainer)
        {
#if MOCKS
            diContainer.RegisterType<IVehicleDao, VehicleDaoMock>();
#else
            diContainer.RegisterType<IVehicleDao, VehicleDaoReal>();
#endif
        }
    }
}
