﻿using System;
using System.Collections.Generic;

namespace Parking.Domain.Test
{
    public class ParkingDomainServicesTestAux
    {
        public ParkingDomainServicesTestAux() { }


        public List<VehicleModel> CreateListVehicle()
        {
            List<VehicleModel> vehicleList = new List<VehicleModel>();
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

                VehicleModel vehicleModel = new VehicleModel
                {
                    Plate = plate
                };

                if (i <= 19)
                {
                    vehicleModel.VehicleType = VehicleType.Car;
                    vehicleModel.CylinderCapacity = 1500;
                }
                else
                {
                    vehicleModel.VehicleType = VehicleType.Motorcycle;
                    vehicleModel.CylinderCapacity = 100;
                }
                vehicleModel.VehicleEntryTime = DateTime.UtcNow;
                vehicleList.Add(vehicleModel);
            }
            return vehicleList;
        }

        public List<VehicleModel> CreateListCar()
        {
            List<VehicleModel> vehicleDtoList = new List<VehicleModel>();
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

                VehicleModel vehicleModel = new VehicleModel(plate, VehicleType.Car, 1500, DateTime.UtcNow);
                vehicleDtoList.Add(vehicleModel);
            }
            return vehicleDtoList;
        }

        public List<VehicleModel> CreateListMotorCycle()
        {
            List<VehicleModel> vehicleDtoList = new List<VehicleModel>();
            for (int i = 0; i < 10; i++)
            {
                VehicleModel vehicleModel = new VehicleModel("ZBC10" + i, VehicleType.Motorcycle, 100, DateTime.UtcNow);
                vehicleDtoList.Add(vehicleModel);
            }
            return vehicleDtoList;
        }
    }
}
