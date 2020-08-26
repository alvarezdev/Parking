using System;
using System.Collections.Generic;

namespace Parking.Domain.Test
{
    public class ParkingDomainTestAux
    {
        public ParkingDomainTestAux() { }


        public List<VehicleDto> CreateListVehicle()
        {
            List<VehicleDto> vehicleDtoList = new List<VehicleDto>();
            for (int i = 0; i < 30; i++)
            {
                string plate;
                if (i >= 0 && i <= 9)
                {
                    plate = "FIS10" + i;
                }                                    
                else
                {
                    plate = "FIS1" + i;
                }                    
                
                VehicleDto vehicleDto = new VehicleDto();
                vehicleDto.Plate = plate;
                if (i <= 19)
                {
                    vehicleDto.VehicleType = VehicleType.Car;
                    vehicleDto.CylinderCapacity = 1500;
                }
                else
                {
                    vehicleDto.VehicleType = VehicleType.Motorcycle;
                    vehicleDto.CylinderCapacity = 100;
                }
                vehicleDto.VehicleEntryTime = DateTime.UtcNow;
                vehicleDtoList.Add(vehicleDto);
            }
            return vehicleDtoList;
        }

        public List<VehicleDto> CreateListCar()
        {
            List<VehicleDto> vehicleDtoList = new List<VehicleDto>();
            for (int i = 0; i < 20; i++)
            {
                string plate;
                if (i >= 0 && i <= 9)
                {
                    plate = "FIS10" + i;
                }                                   
                else
                {
                    plate = "FIS1" + i;
                }                   

                VehicleDto vehicleDto = new VehicleDto
                {
                    Plate = plate,
                    VehicleType = VehicleType.Car,
                    CylinderCapacity = 1500,
                    VehicleEntryTime = DateTime.UtcNow
                };
                vehicleDtoList.Add(vehicleDto);
            }
            return vehicleDtoList;
        }

        public List<VehicleDto> CreateListMotorCycle()
        {
            List<VehicleDto> vehicleDtoList = new List<VehicleDto>();
            for (int i = 0; i < 10; i++)
            {
                VehicleDto vehicleDto = new VehicleDto
                {
                    Plate = "ZBC10" + i,
                    VehicleType = VehicleType.Motorcycle,
                    CylinderCapacity = 100,
                    VehicleEntryTime = DateTime.UtcNow
                };
                vehicleDtoList.Add(vehicleDto);
            }
            return vehicleDtoList;
        }
    }
}
