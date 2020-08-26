using System;
using System.Collections.Generic;
using Xunit;

namespace Parking.Domain.Test
{
    public class ParkingDomainTest : BaseTest
    {
        private readonly ParkingDomainServices parkingDomain;
        public ParkingDomainTest()
        {
            parkingDomain = Resolve<ParkingDomainServices>();
            parkingDomain.DeleteAll();
        }

        [Fact]
        public void EnterParking_EnterCarWhenParkingHasSpace_Successful_Test()
        {
            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "FIS100",
                VehicleType = VehicleType.Car,
                CylinderCapacity = 1500,
                VehicleEntryTime = DateTime.UtcNow
            };

            //Act
            parkingDomain.EnterParking(vehicleDto);

            //Assert
            List<VehicleDto> list = parkingDomain.GetVehicleList();
            var vehicle = list.Exists(v => v.Plate.Equals("FIS100"));
            Assert.True(vehicle);
        }

        [Fact]
        public void EnterParking_EnterCarWithEmptyPlate_BusinessException_Test()
        {
            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = null,
                VehicleType = VehicleType.Car,
                CylinderCapacity = 1500,
                VehicleEntryTime = DateTime.UtcNow
            };
            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(vehicleDto));
        }

        [Fact]
        public void EnterParking_EnterCarWithZeroCylinderCapacity_BusinessException_Test()
        {
            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "FIS100",
                VehicleType = VehicleType.Car,
                CylinderCapacity = 0,
                VehicleEntryTime = DateTime.UtcNow
            };

            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(vehicleDto));
        }

        [Fact]
        public void EnterParking_EnterCarWithPlateRepeat_BusinessException_Test() 
        {
            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "FIS100",
                VehicleType = VehicleType.Car,
                CylinderCapacity = 1500,
                VehicleEntryTime = DateTime.UtcNow
            };
            parkingDomain.EnterParking(vehicleDto);
            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(vehicleDto));
        
        }

        [Fact]
        public void EnterParking_EnterMotorcycleWithEmptyPlate_BusinessException_Test()
        {
            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = null,
                VehicleType = VehicleType.Motorcycle,
                CylinderCapacity = 1500,
                VehicleEntryTime = DateTime.UtcNow
            };

            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(vehicleDto));
        }

        [Fact]
        public void EnterParking_EnterMotorcycleWithZeroCylinderCapacity_BusinessException_Test()
        {
            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "FIS100",
                VehicleType = VehicleType.Motorcycle,
                CylinderCapacity = 0,
                VehicleEntryTime = DateTime.UtcNow
            };

            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(vehicleDto));
        }

        [Fact]
        public void EnterParking_EnterMotorcycleWithPlateRepeat_BusinessException_Test()
        {
            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "FIS100",
                VehicleType = VehicleType.Motorcycle,
                CylinderCapacity = 1500,
                VehicleEntryTime = DateTime.UtcNow
            };
            parkingDomain.EnterParking(vehicleDto);
            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(vehicleDto));
        }

        [Fact]
        public void EnterParking_EnterCarWhenIsSundayAndPlateBeginsA_BusinessException_Test()
        {
            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "AIS100",
                VehicleType = VehicleType.Car,
                CylinderCapacity = 1500,
                VehicleEntryTime = new DateTime(2020, 7, 19)
            };
            //Act
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(vehicleDto));
        }

        [Fact]
        public void EnterParking_EnterCarWhenParkingNotHaveCapacityForCars_BusinessException_Test()
        {
            //Arrenge
            foreach (var vehicle in ParkingDomainTestAux.CreateListCar())
            {
                parkingDomain.EnterParking(vehicle);
            }

            //Act
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "ZBC500",
                VehicleType = VehicleType.Car,
                CylinderCapacity = 1500,
                VehicleEntryTime = DateTime.UtcNow
            };
            
            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(vehicleDto));
        }

        [Fact]
        public void EnterParking_EnterMotorcycleWhenParkingNotHaveCapacityForMotorcycles_BusinessException_Test() 
        {
            //Arrenge
            foreach (var vehicle in ParkingDomainTestAux.CreateListMotorCycle())
            {
                parkingDomain.EnterParking(vehicle);
            }

            //Act
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "ZBC990",
                VehicleType = VehicleType.Motorcycle,
                CylinderCapacity = 1500,
                VehicleEntryTime = DateTime.UtcNow
            };

            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(vehicleDto));
        }

        [Fact]
        public void EnterParking_EnterVehicleWhenParkingNotHaveCapacityForVehicles_BusinessException_Test()
        {
            //Arrenge
            foreach (var vehicle in ParkingDomainTestAux.CreateListVehicle())
            {
                parkingDomain.EnterParking(vehicle);
            }

            //Act
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "BBC999",
                VehicleType = VehicleType.Car,
                CylinderCapacity = 1500,
                VehicleEntryTime = DateTime.UtcNow
            };

            //Assert
            Assert.Throws<BusinessException>(() => parkingDomain.EnterParking(vehicleDto));
        }

        [Fact]
        public void GetVehicle_getCarFromParking_Successful_Test()
        {
            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "FIS100",
                VehicleType = VehicleType.Car,
                CylinderCapacity = 1500,
                VehicleEntryTime = DateTime.UtcNow
            };
            parkingDomain.EnterParking(vehicleDto);

            //Act
            VehicleDto vehicle = parkingDomain.GetVehicle(vehicleDto.Plate);

            //Assert
            Assert.Equal(vehicleDto.Plate, vehicle.Plate);
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
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "FIS100",
                VehicleType = VehicleType.Car,
                CylinderCapacity = 1500,
                VehicleEntryTime = new DateTime(2020, 6, 19, 0, 5, 0)
            };
            parkingDomain.EnterParking(vehicleDto);
            //Act
            VehicleDto vehicle = parkingDomain.GetVehicle(vehicleDto.Plate);
            vehicle.VehicleDepartureTime = new DateTime(2020, 6, 19, 2, 25, 0);
            int valuePay = parkingDomain.CalculateValueParking(vehicleDto);
            bool validateValuePay = valuePay == 2000;
            //Assert
            Assert.True(validateValuePay);
        }

        [Fact]
        public void CalculateValueParking_CalculateForDayPriceCars_Successful_Test()
        {
            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "FIS100",
                VehicleType = VehicleType.Car,
                CylinderCapacity = 1500,
                VehicleEntryTime = new DateTime(2020, 7, 19, 0, 5, 0)
            };
            parkingDomain.EnterParking(vehicleDto);

            //Act
            VehicleDto vehicle = parkingDomain.GetVehicle(vehicleDto.Plate);
            vehicle.VehicleDepartureTime = new DateTime(2020, 7, 20, 2, 25,0);
            int valuePay = parkingDomain.CalculateValueParking(vehicleDto);
            bool validateValuePay = valuePay == 10000;

            //Assert;
            Assert.True(validateValuePay);
        }

        [Fact]
        public void CalculateValueParking_CalculateForHoursPriceMotorcycle_Successful_Test()
        {
            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "BBC100",
                VehicleType = VehicleType.Motorcycle,
                CylinderCapacity = 100,
                VehicleEntryTime = new DateTime(2020, 7, 19, 0, 5, 0)
            };
            parkingDomain.EnterParking(vehicleDto);

            //Act
            VehicleDto vehicle = parkingDomain.GetVehicle(vehicleDto.Plate);
            vehicle.VehicleDepartureTime = new DateTime(2020, 7, 19, 2, 25, 0);
            int valuePay = parkingDomain.CalculateValueParking(vehicleDto);
            bool validateValuePay = valuePay == 1000;

            //Assert;
            Assert.True(validateValuePay);
        }

        [Fact]
        public void CalculateValueParking_CalculateForHoursPriceMotorcycleGreatear500_Successful_Test()
        {

            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "BBC100",
                VehicleType = VehicleType.Motorcycle,
                CylinderCapacity = 1500,
                VehicleEntryTime = new DateTime(2020, 6, 19, 0, 5, 0)
            };
            parkingDomain.EnterParking(vehicleDto);

            //Act
            VehicleDto vehicle = parkingDomain.GetVehicle(vehicleDto.Plate);
            vehicle.VehicleDepartureTime = new DateTime(2020, 6, 19, 2, 25, 0);
            int valuePay = parkingDomain.CalculateValueParking(vehicleDto);
            bool validateValuePay = valuePay == 3000;
            parkingDomain.LeaveVehicle(vehicleDto);

            //Assert;
            Assert.True(validateValuePay);
        }

        [Fact]
        public void LeaveVehicle_LeaveCarParkingAndRemoveToVehicleList_DataBaseException_Test()
        {
            //Arrenge
            VehicleDto vehicleDto = new VehicleDto
            {
                Plate = "BBC100",
                VehicleType = VehicleType.Car,
                CylinderCapacity = 1500,
                VehicleEntryTime = new DateTime(2020, 6, 19, 0, 5, 0)
            };
            parkingDomain.EnterParking(vehicleDto);

            //Act
            VehicleDto vehicle = parkingDomain.GetVehicle(vehicleDto.Plate);
            vehicle.VehicleDepartureTime = new DateTime(2020, 6, 19, 2, 25, 0);
            parkingDomain.LeaveVehicle(vehicleDto);

            //Assert
            Assert.Throws<DataBaseException>(() => parkingDomain.GetVehicle(vehicleDto.Plate));
        }
    }
}
