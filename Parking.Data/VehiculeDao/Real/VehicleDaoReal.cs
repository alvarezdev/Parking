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
        IMapper iMapperVehicleEntity;
        IMapper iMapperVehicleModule;

        public VehicleDaoReal()
        {
            MapperToVehicleEntity();
            MapperToVehicleModel();
        }

        internal Realm GetRealmDatabase()
        {
            var config = new RealmConfiguration("parking.realm") { SchemaVersion = 1};
            return Realm.GetInstance(config);
        }

        private void MapperToVehicleEntity()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DateTime, long>().ConvertUsing(source => new DateTimeToLong().Convert(source));
                cfg.CreateMap<VehicleModel, VehicleEntity>();
            });
            iMapperVehicleEntity = config.CreateMapper();            
        }

        private void MapperToVehicleModel()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<long, DateTime>().ConvertUsing(source => new LongToDateTime().Convert(source));
                cfg.CreateMap<VehicleEntity, VehicleModel>();
            });
            iMapperVehicleModule = config.CreateMapper();
        }

        public void AddVehicle(VehicleModel vehicleModel)
        {
            var realm = GetRealmDatabase();
            var vehicleEntity = iMapperVehicleEntity.Map<VehicleModel, VehicleEntity>(vehicleModel);
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
                VehicleModel vehicleModel = iMapperVehicleModule.Map<VehicleEntity, VehicleModel>(vehicleDB);
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
                VehicleModel vehicleModel = iMapperVehicleModule.Map<VehicleEntity, VehicleModel>(vehicleEntity);
                return vehicleModel;
            }
            return null;
        }
    }
}
