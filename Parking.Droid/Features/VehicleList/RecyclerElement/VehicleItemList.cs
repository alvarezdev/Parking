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

        public void BindVehicle(VehicleModel vehicleModel)
        {
            if (vehicleModel != null)
            {
                plate.Text = vehicleModel.Plate;
                vehicleType.Text = vehicleModel.VehicleType.ToString();
                cylinderCapacity.Text = vehicleModel.CylinderCapacity.ToString();
                entryTime.Text = vehicleModel.VehicleEntryTime.ToLocalTime().ToString("dd-M-yyyy hh:mm:ss");
            }
        }
    }
}
