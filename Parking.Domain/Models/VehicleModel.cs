using System;

namespace Parking.Domain
{
    public class VehicleModel
    {
        private const string nullVehiclePlateMsg = "Vehicle plate can't null or empty";
        private const string zeroCylinderCapacityMsg = "The cylinder capacity of the vehicle must be greater than zero";
        private const string vehicleDepartureTimeMsg = "The departure time must be greater than the entry time";

        private string plate;
        private double cylinderCapacity;
        private DateTime vehicleDepartureTime;

        private string messageException;

        private bool validatePlate;
        private bool validatecylinderCapacity;
        private bool validatevehicleDepartureTime;

        public string Plate
        {
            get => plate;
            set
            {
                if (ValidatePlate(value))
                {
                    plate = value;
                }
                else
                {
                    validatePlate = true;
                }
            }
        }      

        public VehicleType VehicleType { get; set; }
        public double CylinderCapacity
        {
            get => cylinderCapacity;
            set
            {
                if (ValidateCylinderCapacity(value))
                {
                    cylinderCapacity = value;
                }
                else
                {
                    validatecylinderCapacity = true;
                }
            }
        }
        public DateTime VehicleEntryTime { get; set; }

        public DateTime VehicleDepartureTime
        {
            get => vehicleDepartureTime;
            set
            {
                if (ValidateDepartureTime(value))
                {
                    vehicleDepartureTime = value;
                }
                else
                {
                    validatevehicleDepartureTime = true;
                }
            }
        }

        public VehicleModel()
        {
        }

        public VehicleModel(string plate, VehicleType vehicleType, double cylinderCapacity, DateTime vehicleEntryTime)
        {
            Plate = plate;
            VehicleType = vehicleType;
            CylinderCapacity = cylinderCapacity;
            VehicleEntryTime = vehicleEntryTime;
            if(validatePlate)
            {
                messageException = nullVehiclePlateMsg;
            }
            if(validatecylinderCapacity)
            {
                messageException = messageException + " - " + zeroCylinderCapacityMsg;
            }
            if(validatevehicleDepartureTime)
            {
                messageException = messageException + " - " + vehicleDepartureTimeMsg;
            }
            if (validatePlate || validatecylinderCapacity || validatevehicleDepartureTime)
            {
                throw new BusinessException(messageException);
            }
        }

        private bool ValidatePlate(string plate)
        {
            return !string.IsNullOrEmpty(plate);
        }

        private bool ValidateCylinderCapacity(double cylinderCapacity)
        {
            return cylinderCapacity >= 0;
        }

        private bool ValidateDepartureTime(DateTime vehicleDepartureTime)
        {
            return vehicleDepartureTime > VehicleEntryTime;
        }

        private void VehicleModelExceptionHandler()
        {
        }
    }
}
