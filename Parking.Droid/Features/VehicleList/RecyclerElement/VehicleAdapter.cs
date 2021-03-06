﻿using System;
using System.Collections.Generic;
using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Parking.Domain;

namespace Parking.Droid
{
    public class VehicleAdapter : RecyclerView.Adapter
    {
        public List<VehicleModel> vehicleList;
        private VehicleListActivity vehicleListActivity;

        public VehicleAdapter(List<VehicleModel> vehicleList, VehicleListActivity vehicleListActivity)
        {
            this.vehicleList = vehicleList;
            this.vehicleListActivity = vehicleListActivity;
        }

        public override int ItemCount => vehicleList.Count;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup viewGroup, int viewType)
        {
            View view = LayoutInflater.From(viewGroup.Context).Inflate(Resource.Layout.item_vehicle_list, viewGroup, false);
            return new VehicleItemList(view);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder vehicleHolder, int position)
        {
            VehicleModel vehicleDto = vehicleList.ElementAt(position);
            VehicleItemList vehicleItemList = vehicleHolder as VehicleItemList;
            vehicleItemList.BindVehicle(vehicleDto);
            vehicleItemList.leave.Click -= ShowDialogConfirm;
            vehicleItemList.leave.Click += ShowDialogConfirm;
            vehicleItemList.leave.Tag = position;
        }

        private void ShowDialogConfirm(object sender, EventArgs args)
        {
            View view = sender as View;
            int position = (int)view.Tag;
            VehicleModel vehicle = vehicleList.ElementAt(position);
            DialogFragmentConfirmVehicleDeparture confirmVehicleDeparture =
                    new DialogFragmentConfirmVehicleDeparture(vehicle, position, vehicleListActivity);
            confirmVehicleDeparture.Show(vehicleListActivity.SupportFragmentManager.BeginTransaction(),
                    "confirmVehicleDeparture");
        }

        public void Filter(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                vehicleList.Clear();
                vehicleList = vehicleListActivity.GetVehicleList();
            }
            else
            {
                List<VehicleModel> vehicleDtoList = new List<VehicleModel>();
                foreach (var vehicle in vehicleListActivity.GetVehicleList())
                {
                    if (vehicle.Plate.Contains(text))
                    {
                        vehicleDtoList.Add(vehicle);
                    }                        
                }
                vehicleList = vehicleDtoList;
            }
            if (vehicleList.Count == 0)
            {
                Toast.MakeText(vehicleListActivity, Resource.String.list_is_empty, ToastLength.Short).Show();
            }                           
            NotifyDataSetChanged();
        }
    }
}
