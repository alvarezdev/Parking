﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Parking.Domain;

namespace Parking.Droid
{
    public class DialogFragmentConfirmVehicleDeparture : DialogFragment
    {
        private TextView payMessage;
        private Button accept;
        private Button cancel;

        private VehicleDto vehicleDto;
        private int position;
        private VehicleListActivity vehicleListActivity;

        public DialogFragmentConfirmVehicleDeparture(VehicleDto vehicleDto, int position, VehicleListActivity vehicleListActivity)
        {
            this.vehicleDto = vehicleDto;
            this.position = position;
            this.vehicleListActivity = vehicleListActivity;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.dialog_fragment_confirm_vehicle_departure, container, false);
            payMessage = view.FindViewById<TextView>(Resource.Id.pay_message);
            accept = view.FindViewById<Button>(Resource.Id.accept_dialog);
            cancel = view.FindViewById<Button>(Resource.Id.cancel_dialog);

            int valuePayForParking = vehicleListActivity.CalculateValueParking(vehicleDto);

            payMessage.Text = GetString(Resource.String.text_pay_message_dialog_fragment) + " " + valuePayForParking;

            accept.Click += delegate
            {
                vehicleListActivity.DeleteVehicleFromDialogFragment(vehicleDto, position);
                Dialog.Dismiss();
            };

            cancel.Click += delegate {
                Dialog.Dismiss();
            };

            return view;
        }
    }
}
