
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

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}
