using System;
using System.Collections.Generic;
using Xunit;

namespace Parking.Domain.Test
{
    public class ParkingDomainServicesTest : BaseTest
    {
        private readonly ParkingDomainServices parkingDomain;

        public ParkingDomainServicesTest()
        {
            parkingDomain = Resolve<ParkingDomainServices>();
            parkingDomain.DeleteAll();
        }

        [Fact]
        public void EnterParking_EnterCarWhenParkingHasSpace_Successful_Test()
        {
            //Arrenge
            var plate = "FIS100";
            VehicleType vehicleType = VehicleType.Car;
            double cylinderCapacity = 1500;
            var vehicleEntryTime = DateTime.UtcNow;            

            //Act
            parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity,vehicleEntryTime);

            //Assert
            List<VehicleModel> list = parkingDomain.GetVehicleList();
            var vehicle = list.Exists(v => v.Plate.Equals("FIS100"));
            Assert.True(vehicle);
        }

        [Fact]
        public void EnterParking_EnterCarWithEmptyPlate_BusinessException_Test()
        {
            //Arrenge
            string plate = null;
            VehicleType vehicleType = VehicleType.Car;
            double cylinderCapacity = 1500;
            var vehicleEntryTime = DateTime.UtcNow;

            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime));
        }

        [Fact]
        public void EnterParking_EnterCarWithZeroCylinderCapacity_BusinessException_Test()
        {
            //Arrenge
            var plate = "FIS100";
            VehicleType vehicleType = VehicleType.Car;
            double cylinderCapacity = 0;
            var vehicleEntryTime = DateTime.UtcNow;

            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime));
        }

        [Fact]
        public void EnterParking_EnterCarWithPlateRepeat_BusinessException_Test() 
        {
            //Arrenge
            var plate = "FIS100";
            VehicleType vehicleType = VehicleType.Car;
            double cylinderCapacity = 1500;
            var vehicleEntryTime = DateTime.UtcNow;

            parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime);
            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime));
        
        }

        [Fact]
        public void EnterParking_EnterMotorcycleWithEmptyPlate_BusinessException_Test()
        {
            //Arrenge
            string plate = null;
            VehicleType vehicleType = VehicleType.Motorcycle;
            double cylinderCapacity = 200;
            var vehicleEntryTime = DateTime.UtcNow;

            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime));
        }

        [Fact]
        public void EnterParking_EnterMotorcycleWithZeroCylinderCapacity_BusinessException_Test()
        {
            //Arrenge
            var plate = "DPA78D";
            VehicleType vehicleType = VehicleType.Motorcycle;
            double cylinderCapacity = 0;
            var vehicleEntryTime = DateTime.UtcNow;

            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime));
        }

        [Fact]
        public void EnterParking_EnterMotorcycleWithPlateRepeat_BusinessException_Test()
        {
            //Arrenge
            var plate = "DPA78D";
            VehicleType vehicleType = VehicleType.Motorcycle;
            double cylinderCapacity = 1500;
            var vehicleEntryTime = DateTime.UtcNow;

            parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime);
            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime));
        }

        [Fact]
        public void EnterParking_EnterCarWhenIsSundayAndPlateBeginsA_BusinessException_Test()
        {
            //Arrenge

            var plate = "AIS100";
            VehicleType vehicleType = VehicleType.Car;
            double cylinderCapacity = 1500;
            var vehicleEntryTime = new DateTime(2020, 7, 19);

            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime));
        }

        [Fact]
        public void EnterParking_EnterCarWhenParkingNotHaveCapacityForCars_BusinessException_Test()
        {
            //Arrenge
            foreach (var vehicle in ParkingDomainTestAux.CreateListCar())
            {
                parkingDomain.EnterParking(vehicle.Plate, vehicle.VehicleType, vehicle.CylinderCapacity, vehicle.VehicleEntryTime);
            }

            //Act
            var plate = "ZBC500";
            VehicleType vehicleType = VehicleType.Car;
            double cylinderCapacity = 1500;
            var vehicleEntryTime = DateTime.UtcNow;

            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime));
        }

        [Fact]
        public void EnterParking_EnterMotorcycleWhenParkingNotHaveCapacityForMotorcycles_BusinessException_Test() 
        {
            //Arrenge
            foreach (var vehicle in ParkingDomainTestAux.CreateListMotorCycle())
            {
                parkingDomain.EnterParking(vehicle.Plate, vehicle.VehicleType, vehicle.CylinderCapacity, vehicle.VehicleEntryTime);
            }

            //Act
            var plate = "ZBC990";
            VehicleType vehicleType = VehicleType.Motorcycle;
            double cylinderCapacity = 200;
            var vehicleEntryTime = DateTime.UtcNow;

            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime));
        }

        [Fact]
        public void EnterParking_EnterVehicleWhenParkingNotHaveCapacityForVehicles_BusinessException_Test()
        {
            //Arrenge
            foreach (var vehicle in ParkingDomainTestAux.CreateListVehicle())
            {
                parkingDomain.EnterParking(vehicle.Plate, vehicle.VehicleType, vehicle.CylinderCapacity, vehicle.VehicleEntryTime);
            }

            //Act
            var plate = "ZBC990";
            VehicleType vehicleType = VehicleType.Car;
            double cylinderCapacity = 1500;
            var vehicleEntryTime = DateTime.UtcNow;

            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime));
        }

        [Fact]
        public void GetVehicle_getCarFromParking_Successful_Test()
        {
            //Arrenge
            var plate = "FIS100";
            VehicleType vehicleType = VehicleType.Car;
            double cylinderCapacity = 1500;
            var vehicleEntryTime = DateTime.UtcNow;

            parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime);

            //Act
            VehicleModel vehicle = parkingDomain.GetVehicle(plate);

            //Assert
            Assert.Equal(plate, vehicle.Plate);
        }

        [Fact]
        public void GetVehicle_GetCarFromParkingNonexistent_DataBaseException_Test()
        {
            //Arrenge
            //Act
            //Assert
            Assert.Throws<DataBaseException>(() => parkingDomain.GetVehicle("FIS100"));
        }

        [Fact]
        public void CalculateValueParking_CalculateForHoursPriceCars_Successful_Test()
        {
            //Arrenge
            var plate = "FIS100";
            VehicleType vehicleType = VehicleType.Car;
            double cylinderCapacity = 1500;
            var vehicleEntryTime = new DateTime(2020, 6, 19, 0, 5, 0);
            parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime);

            //Act
            VehicleModel vehicle = parkingDomain.GetVehicle(plate);
            var vehicleDepartureTime = new DateTime(2020, 6, 19, 2, 25, 0);
            int valuePay = parkingDomain.CalculateValueParking(vehicle.VehicleType, vehicle.CylinderCapacity, vehicle.VehicleEntryTime, vehicleDepartureTime);

            bool validateValuePay = valuePay == 2000;
            //Assert
            Assert.True(validateValuePay);
        }

        [Fact]
        public void CalculateValueParking_CalculateForDayPriceCars_Successful_Test()
        {
            //Arrenge
            var plate = "FIS100";
            VehicleType vehicleType = VehicleType.Car;
            double cylinderCapacity = 1500;
            var vehicleEntryTime = new DateTime(2020, 7, 19, 0, 5, 0);

            parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime);

            //Act
            VehicleModel vehicle = parkingDomain.GetVehicle(plate);
            var vehicleDepartureTime = new DateTime(2020, 7, 20, 2, 25, 0);
            int valuePay = parkingDomain.CalculateValueParking(vehicle.VehicleType, vehicle.CylinderCapacity, vehicle.VehicleEntryTime, vehicleDepartureTime);
            bool validateValuePay = valuePay == 10000;

            //Assert;
            Assert.True(validateValuePay);
        }

        [Fact]
        public void CalculateValueParking_CalculateForHoursPriceMotorcycle_Successful_Test()
        {
            //Arrenge
            var plate = "BBC100";
            VehicleType vehicleType = VehicleType.Motorcycle;
            double cylinderCapacity = 100;
            var vehicleEntryTime = new DateTime(2020, 7, 19, 0, 5, 0);

            parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime);

            //Act
            VehicleModel vehicle = parkingDomain.GetVehicle(plate);
            var vehicleDepartureTime = new DateTime(2020, 7, 19, 2, 25, 0);
            int valuePay = parkingDomain.CalculateValueParking(vehicle.VehicleType, vehicle.CylinderCapacity, vehicle.VehicleEntryTime, vehicleDepartureTime);
            bool validateValuePay = valuePay == 1000;

            //Assert;
            Assert.True(validateValuePay);
        }

        [Fact]
        public void CalculateValueParking_CalculateForHoursPriceMotorcycleGreatear500_Successful_Test()
        {

            //Arrenge
            var plate = "BBC100";
            VehicleType vehicleType = VehicleType.Motorcycle;
            double cylinderCapacity = 1500;
            var vehicleEntryTime = new DateTime(2020, 6, 19, 0, 5, 0);

            parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime);

            //Act
            VehicleModel vehicle = parkingDomain.GetVehicle(plate);
            var vehicleDepartureTime = new DateTime(2020, 6, 19, 2, 25, 0);
            int valuePay = parkingDomain.CalculateValueParking(vehicle.VehicleType, vehicle.CylinderCapacity, vehicle.VehicleEntryTime, vehicleDepartureTime);
            bool validateValuePay = valuePay == 3000;
            parkingDomain.LeaveVehicle(vehicle.Plate);

            //Assert;
            Assert.True(validateValuePay);
        }

        [Fact]
        public void LeaveVehicle_LeaveCarParkingAndRemoveToVehicleList_DataBaseException_Test()
        {
            //Arrenge
            var plate = "BBC100";
            VehicleType vehicleType = VehicleType.Car;
            double cylinderCapacity = 1500;
            var vehicleEntryTime = new DateTime(2020, 6, 19, 0, 5, 0);
            parkingDomain.EnterParking(plate, vehicleType, cylinderCapacity, vehicleEntryTime);

            //Act
            VehicleModel vehicle = parkingDomain.GetVehicle(plate);
            vehicle.VehicleDepartureTime = new DateTime(2020, 6, 19, 2, 25, 0);
            parkingDomain.LeaveVehicle(vehicle.Plate);

            //Assert
            Assert.Throws<DataBaseException>(() => parkingDomain.GetVehicle(plate));
        }
    }
}
