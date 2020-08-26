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
        private const string emptyParking = "Empty parking";
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

        public void EnterParking(VehicleDto vehicleDto)
        {
            VehicleModel vehicleModel = new VehicleModel(vehicleDto);
            vehicleModel.ValidateData(vehicleModel);
            List<VehicleDto> vehicleDtoList = GetVehicleList();
            if (vehicleDtoList.Count >= maxCapacityParking)
            {
                throw new BusinessException(fullParkingMsg);
            }
            else
            {
                bool plateRepeat = CheckPlateRepeat(vehicleDtoList, vehicleDto);
                bool checkDate = CheckDate(vehicleDto);
                bool maxCapacityVehicle = MaxCapacityVehicle(vehicleDtoList, vehicleDto.VehicleType);
                if (plateRepeat && checkDate && maxCapacityVehicle)
                {
                    vehicleDao.AddVehicle(vehicleDto);
                }
            }
        }

        private bool CheckPlateRepeat(List<VehicleDto> vehicleDtoList, VehicleDto vehicleDto)
        {
            bool vehicleExist = false;
            foreach (var vehicle in vehicleDtoList)
            {
                if (vehicleDto.Plate.Equals(vehicle.Plate))
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

        private bool CheckDate(VehicleDto vehicleDto)
        {
            var dayOfWeek = vehicleDto.VehicleEntryTime.DayOfWeek;
            if ((dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Monday) && vehicleDto.Plate.Substring(0, 1).Equals("A"))
            {
                throw new BusinessException(unathorizedEntry);
            }                
            else
            {
                return true;
            }
                
        }

        private bool MaxCapacityVehicle(List<VehicleDto> vehicleDtoList, VehicleType vehicleType)
        {
            int capacityNumber = 0;
            for (int i = 0; i < vehicleDtoList.Count; i++)
            {
                VehicleDto vehicleDto = vehicleDtoList[i];
                if (vehicleDto.VehicleType == vehicleType)
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

        public List<VehicleDto> GetVehicleList()
        {          
            if (GetVehicleList().Count > 0)
            {
                return vehicleDao.GetVehicleList();
            }
            else
            {
                throw new DataBaseException(emptyParking);
            }
        }

        public VehicleDto GetVehicle(string plate)
        {
            VehicleDto vehicleDto = vehicleDao.GetVehicle(plate);
            if (vehicleDto == null)
            {
                throw new DataBaseException(vehicleNotExistMsg);
            }                
            return vehicleDto;
        }

        public void LeaveVehicle(VehicleDto vehicleDto)
        {
            if (GetVehicle(vehicleDto.Plate) != null)
            {
                vehicleDao.DeleteVehicle(vehicleDto);
            }            
        }

        public int CalculateValueParking(VehicleDto vehicleDto)
        {
            long entryHour = (long)vehicleDto.VehicleEntryTime.Subtract(new DateTime(1970, 1, 1)).TotalHours;
            long departureHour = (long)vehicleDto.VehicleDepartureTime.Subtract(new DateTime(1970, 1, 1)).TotalHours;
            int time = (int)(departureHour - entryHour);

            time = (time == 0) ? time++ : time;

            int hourPrice = 0;
            int dayPrice = 0;

            switch (vehicleDto.VehicleType)
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
            if (vehicleDto.VehicleType == VehicleType.Motorcycle && vehicleDto.CylinderCapacity > cylinderCapacityGreater)
            {
                result += addMotorcyclePay;
            }                
            return result;
        }
    }
}
