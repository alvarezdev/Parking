using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace Parking.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.ActionBar", MainLauncher = true)]
    public class HomeActivity : AppCompatActivity
    {
        private Button addVehicle;
        private Button showVehicleList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_home);
            InitComponent();
        }

        private void InitComponent()
        {
            addVehicle = FindViewById<Button>(Resource.Id.button_add_vehicle);
            addVehicle.Click += OpenAddVehicle;

            showVehicleList = FindViewById<Button>(Resource.Id.button_get_vehicles);
            showVehicleList.Click += OpenVehicleList;
        }

        private void OpenAddVehicle(object sender, EventArgs args)
        {
            Intent intent = new Intent(this, typeof(AddVehicleActivity));
            StartActivity(intent);
        }

        private void OpenVehicleList(object sender, EventArgs args)
        {
            Intent intent = new Intent(this, typeof(VehicleListActivity));
            StartActivity(intent);
        }
    }
}
