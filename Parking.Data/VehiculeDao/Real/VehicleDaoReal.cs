using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Parking.Domain;
using Realms;

namespace Parking.Data
{
    public class VehicleDaoReal : IVehicleDao
    {
        VehicleEntityMapper vehicleEntityMapper;

        public VehicleDaoReal()
        {
            vehicleEntityMapper = new VehicleEntityMapper();
        }

        internal Realm GetRealmDatabase()
        {
            var config = new RealmConfiguration("parking.realm") { SchemaVersion = 1};
            return Realm.GetInstance(config);
        }

        public void AddVehicle(VehicleModel vehicleModel)
        {
            var realm = GetRealmDatabase();
            var vehicleEntity = vehicleEntityMapper.MapModelToDbEntity(vehicleModel);
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

        public void DeleteVehicle(string plate)
        {
            var realm = GetRealmDatabase();
            var vehicleEntity = realm.All<VehicleEntity>().Where(v => v.Plate.Equals(plate)).ToList().FirstOrDefault();
            realm.Write(() =>
            {
                realm.Remove(vehicleEntity);
            });
        }

        public List<VehicleModel> GetVehicleList()
        {
            List<VehicleModel> vehicleList = new List<VehicleModel>();
            var realm = GetRealmDatabase();
            List<VehicleEntity> listVehicleDB = realm.All<VehicleEntity>().ToList();
            foreach (var vehicleDB in listVehicleDB)
            {
                VehicleModel vehicleModel = vehicleEntityMapper.MapDbEntityToModel(vehicleDB);
                vehicleList.Add(vehicleModel);
            }
            return vehicleList;
        }

        public VehicleModel GetVehicle(string plate)
        {
            var realm = GetRealmDatabase();
            var vehicleEntity = realm.All<VehicleEntity>().Where(v => v.Plate.Equals(plate)).ToList().FirstOrDefault();

            if (vehicleEntity != null)
            {
                VehicleModel vehicleModel = vehicleEntityMapper.MapDbEntityToModel(vehicleEntity);
                return vehicleModel;
            }
            return null;
        }
    }
}
