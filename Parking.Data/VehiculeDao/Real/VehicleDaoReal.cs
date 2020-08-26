using System;
using System.Collections.Generic;
using System.Linq;
using Parking.Domain;
using Realms;

namespace Parking.Data
{
    public class VehicleDaoReal : IVehicleDao
    {
        public VehicleDaoReal()
        {
        }

        internal Realm GetRealmDatabase()
        {
            var config = new RealmConfiguration("parking.realm") { SchemaVersion = 1};
            return Realm.GetInstance(config);
        }

        public void AddVehicle(VehicleDto vehicleDto)
        {
            var realm = GetRealmDatabase();
            VehicleEntity vehicleEntity = new VehicleEntity(vehicleDto);
            realm.Write(() =>
            {
                realm.Add(vehicleEntity, true);
            });
        }

        public void DeleteAll()
        {
            var realm = GetRealmDatabase();
            realm.Write(() =>
            {
                realm.RemoveAll<VehicleEntity>();
            });
        }

        public void DeleteVehicle(VehicleDto vehicleDto)
        {
            var realm = GetRealmDatabase();
            var vehicleEntity = realm.All<VehicleEntity>().Where(v => v.Plate.Equals(vehicleDto.Plate)).ToList().FirstOrDefault();
            realm.Write(() =>
            {
                realm.Remove(vehicleEntity);
            });
        }

        public List<VehicleDto> GetVehicleList()
        {
            List<VehicleDto> vehicleList = new List<VehicleDto>();
            var realm = GetRealmDatabase();
            List<VehicleEntity> listVehicleDB = realm.All<VehicleEntity>().ToList();
            foreach (var vehicleDB in listVehicleDB)
            {
                var vehicleDTO = new VehicleDto
                {
                    Plate = vehicleDB.Plate,
                    VehicleType = Enum.Parse<VehicleType>(vehicleDB.VehicleType),                    
                    CylinderCapacity = vehicleDB.CylinderCapacity,
                    VehicleEntryTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(vehicleDB.VehicleEntryTime).ToUniversalTime(),
                };
                vehicleList.Add(vehicleDTO);
            }
            return vehicleList;
        }

        public VehicleDto GetVehicle(string plate)
        {
            var realm = GetRealmDatabase();
            var vehicleEntity = realm.All<VehicleEntity>().Where(v => v.Plate.Equals(plate)).ToList().FirstOrDefault();

            if (vehicleEntity != null)
            {
                var vehicleDTO = new VehicleDto
                {                    
                    Plate = vehicleEntity.Plate,
                    VehicleType = Enum.Parse<VehicleType>(vehicleEntity.VehicleType),
                    CylinderCapacity = vehicleEntity.CylinderCapacity,
                    VehicleEntryTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(vehicleEntity.VehicleEntryTime).ToUniversalTime(),
                };
                return vehicleDTO;
            }
            return null;
        }
    }
}
