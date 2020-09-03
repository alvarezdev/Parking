using System;
using System.Collections.Generic;

namespace Parking.Domain
{
    public class ParkingDomainServices
    {
        private const string fullParkingMsg = "The parking is full";
        private const string repeatVehiclePlateMsg = "The vehicle plate is repeat";
        private const string unathorizedEntry = "Unauthorized entry";
        private const string maxCapacityCarMsg = "Parking capacity for cars is full";
        private const string maxCapacityMotorcycleMsg = "Parking capacity for Motorcycles is full";
        private const string vehicleNotExistMsg = "This vehicle is not in the parking";
        private const int maxCapacityCar = 20;
        private const int maxCapacityMotorcycle = 10;
        private const int maxCapacityParking = 30;
        private const int carHourValue = 1000;
        private const int motorcycleHourValue = 500;
        private const int carDayValue = 8000;
        private const int motorcycleDayValue = 4000;
        private const int addMotorcyclePay = 2000;
        private const double cylinderCapacityGreater = 500;

        private readonly IVehicleDao vehicleDao;

        public ParkingDomainServices(IVehicleDao vehicleDao)
        {
            this.vehicleDao = vehicleDao;
        }

        public void DeleteAll()
        {
            if(GetVehicleList().Count > 0 )
            {
                vehicleDao.DeleteAll();
            }         
        }

        public void EnterParking(string plate, VehicleType vehicleType, double cylinderCapacity, DateTime vehicleEntryTime)
        {
            var vehicleModel = new VehicleModel
            {
                Plate = plate,
                VehicleType = vehicleType,                
                CylinderCapacity = cylinderCapacity,
                VehicleEntryTime = vehicleEntryTime
            };
            List<VehicleModel> vehicleList = GetVehicleList();
            if (vehicleList.Count >= maxCapacityParking)
            {
                throw new BusinessException(fullParkingMsg);
            }
            else
            {
                bool plateRepeat = CheckPlateRepeat(vehicleList, vehicleModel);
                bool checkDate = CheckDate(vehicleModel);
                bool maxCapacityVehicle = MaxCapacityVehicle(vehicleList, vehicleModel.VehicleType);
                if (plateRepeat && checkDate && maxCapacityVehicle)
                {
                    vehicleDao.AddVehicle(vehicleModel);
                }
            }
        }

        private bool CheckPlateRepeat(List<VehicleModel> vehicleDtoList, VehicleModel vehicleModel)
        {
            bool vehicleExist = false;
            foreach (var vehicle in vehicleDtoList)
            {
                if (vehicleModel.Plate.Equals(vehicle.Plate))
                {
                    vehicleExist = true;
                }                    
            }
            if (vehicleExist)
            {
                throw new BusinessException(repeatVehiclePlateMsg);
            }                
            else
            {
                return true;
            }
                
        }

        private bool CheckDate(VehicleModel vehicleModel)
        {
            var dayOfWeek = vehicleModel.VehicleEntryTime.DayOfWeek;
            if ((dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Monday) && vehicleModel.Plate.Substring(0, 1).Equals("A"))
            {
                throw new BusinessException(unathorizedEntry);
            }                
            else
            {
                return true;
            }
                
        }

        private bool MaxCapacityVehicle(List<VehicleModel> vehicleModelList, VehicleType vehicleType)
        {
            int capacityNumber = 0;
            for (int i = 0; i < vehicleModelList.Count; i++)
            {
                VehicleModel vehicleModel = vehicleModelList[i];
                if (vehicleModel.VehicleType == vehicleType)
                {
                    capacityNumber++;
                }                    
            }

            switch (vehicleType)
            {
                case VehicleType.Car:
                    if (capacityNumber >= maxCapacityCar && vehicleType == VehicleType.Car)
                    {
                        throw new BusinessException(maxCapacityCarMsg);
                    }                        
                    break;
                case VehicleType.Motorcycle:
                    if (capacityNumber >= maxCapacityMotorcycle && vehicleType == VehicleType.Motorcycle)
                    {
                        throw new BusinessException(maxCapacityMotorcycleMsg);
                    }                        
                    break;
            }
            return true;
        }

        public List<VehicleModel> GetVehicleList()
        {
            return vehicleDao.GetVehicleList();            
        }

        public VehicleModel GetVehicle(string plate)
        {
            VehicleModel vehicleModel = vehicleDao.GetVehicle(plate);
            if (vehicleModel == null)
            {
                throw new DataBaseException(vehicleNotExistMsg);
            }                
            return vehicleModel;
        }

        public void LeaveVehicle(string plate)
        {
            if (GetVehicle(plate) != null)
            {
                vehicleDao.DeleteVehicle(plate);
            }            
        }

        public int CalculateValueParking(VehicleType vehicleType, double cylinderCapacity, DateTime vehicleEntryTime, DateTime vehicleDepartureTime)
        {
            long entryHour = (long)vehicleEntryTime.Subtract(new DateTime(1970, 1, 1)).TotalHours;
            long departureHour = (long)vehicleDepartureTime.Subtract(new DateTime(1970, 1, 1)).TotalHours;
            int time = (int)(departureHour - entryHour);

            time = (time == 0) ? time++ : time;

            int hourPrice = 0;
            int dayPrice = 0;

            switch (vehicleType)
            {
                case VehicleType.Car:
                    hourPrice = carHourValue;
                    dayPrice = carDayValue;
                    break;
                case VehicleType.Motorcycle:
                    hourPrice = motorcycleHourValue;
                    dayPrice = motorcycleDayValue;
                    break;
            }

            int result = 0;

            if (time >= 1 && time <= 9)
            {
                result = time * hourPrice;
            }                
            else if (time > 9 && time <= 24)
            {
                result = dayPrice;
            }                
            else if (time > 24)
            {
                int hours = time - 24;
                if (hours < 0)
                {
                    hours *= -1;
                }                    
                result = dayPrice + (hours * hourPrice);
            }
            if (vehicleType == VehicleType.Motorcycle && cylinderCapacity > cylinderCapacityGreater)
            {
                result += addMotorcyclePay;
            }                
            return result;
        }
    }
}
