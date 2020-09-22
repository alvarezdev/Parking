using System;
using AutoMapper;
using Parking.Domain;

namespace Parking.Data
{
    public class VehicleEntityMapper
    {
        IMapper iMapperVehicleEntity;
        IMapper iMapperVehicleModule;

        public VehicleEntityMapper()
        {
        }

        public VehicleModel MapDbEntityToModel(VehicleEntity vehicleEntity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<long, DateTime>().ConvertUsing(source => new LongToDateTime().Convert(source));
                cfg.CreateMap<VehicleEntity, VehicleModel>();
            });
            iMapperVehicleModule = config.CreateMapper();
            return iMapperVehicleModule.Map<VehicleEntity, VehicleModel>(vehicleEntity);
        }

        public VehicleEntity MapModelToDbEntity(VehicleModel vehicleModel)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DateTime, long>().ConvertUsing(source => new DateTimeToLong().Convert(source));
                cfg.CreateMap<VehicleModel, VehicleEntity>();
            });
            iMapperVehicleEntity = config.CreateMapper();
            return iMapperVehicleEntity.Map<VehicleModel, VehicleEntity>(vehicleModel);

        }
    }
}
