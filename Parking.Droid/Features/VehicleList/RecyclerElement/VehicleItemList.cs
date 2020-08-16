using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Parking.Domain;

namespace Parking.Droid
{
    public class VehicleItemList : RecyclerView.ViewHolder
    {
        private TextView plate;
        private TextView vehicleType;
        private TextView cylinderCapacity;
        private TextView entryTime;
        public Button leave;

        public VehicleItemList(View itemView) : base(itemView)
        {
            plate = itemView.FindViewById<TextView>(Resource.Id.plate);
            vehicleType = itemView.FindViewById<TextView>(Resource.Id.vehicle_type);
            cylinderCapacity = itemView.FindViewById<TextView>(Resource.Id.cylinder_capacity);
            entryTime = itemView.FindViewById<TextView>(Resource.Id.entry_time);
            leave = itemView.FindViewById<Button>(Resource.Id.leave_vehicle);
        }

        public void BindVehicle(VehicleDto vehicleDto)
        {
            if (vehicleDto != null)
            {
                plate.Text = vehicleDto.Plate;
                vehicleType.Text = vehicleDto.VehicleType.ToString();
                cylinderCapacity.Text = vehicleDto.CylinderCapacity.ToString();
                entryTime.Text = vehicleDto.VehicleEntryTime.ToLocalTime().ToString("dd-M-yyyy hh:mm:ss");
            }
        }
    }
}
