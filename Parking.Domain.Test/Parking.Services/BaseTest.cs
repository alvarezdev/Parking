using System;
using Parking.Data;
using Unity;

namespace Parking.Domain.Test
{
    public class BaseTest
    {
        static UnityContainer DIContainer { get; set; }

        protected ParkingDomainServicesTestAux ParkingDomainTestAux;

        public BaseTest()
        {
            DIContainer = new UnityContainer();
            ParkingDomainTestAux = new ParkingDomainServicesTestAux();
            DIContainer.RegisterType<IVehicleDao, VehicleDaoMock>();
        }

        public static TRequest Resolve<TRequest>()
        {
            if (DIContainer == null)
            {
                throw new InvalidOperationException("The injection container is not ready");
            }                
            return DIContainer.Resolve<TRequest>();
        }
    }
}
