
using System;

using Android.App;
using Android.Arch.Lifecycle;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using Parking.Domain;

namespace Parking.Droid
{
    [Activity(Label = "@string/app_name")]
    public class AddVehicleActivity : AppCompatActivity
    {
        private const string TAG = "AddVehicleActivity";

        private EditText plate;
        private EditText cylinderCapacity;
        private RadioGroup vehicleGroup;
        private RadioButton vehicleOption;
        private Button accept;
        private Button cancel;

        private AddVehicleViewModel addVehicleViewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_vehicle);
            InitComponent();
        }

        private void InitComponent()
        {
            addVehicleViewModel = (AddVehicleViewModel) new ViewModelProvider
                .AndroidViewModelFactory(Application).Create(Java.Lang.Class.FromType(typeof(AddVehicleViewModel)));
            plate = FindViewById<EditText>(Resource.Id.input_plate);
            cylinderCapacity = FindViewById<EditText>(Resource.Id.input_cylinder_capacity);
            vehicleGroup = FindViewById<RadioGroup>(Resource.Id.radio_group_vehicle);
            accept = FindViewById<Button>(Resource.Id.btn_accept_add_vehicle);
            cancel = FindViewById<Button>(Resource.Id.btn_cancel_add_vehicle);
            accept.Click += Accept;
            cancel.Click += delegate
            {
                Finish();
	        };
        }

        private void Accept(object sender, EventArgs args)
        {            
            int selectedId = vehicleGroup.CheckedRadioButtonId;
            vehicleOption = FindViewById<RadioButton>(selectedId);
            VehicleType vehicleType = vehicleOption.Text.Equals("Carro") ? VehicleType.Car : VehicleType.Motorcycle;
            try
            {
                addVehicleViewModel.AddVehicle(plate.Text, vehicleType, ProcessCylinderCapacity(cylinderCapacity.Text), DateTime.UtcNow);
                Toast.MakeText(this, Resource.String.add_vehicle_message_successful, ToastLength.Short).Show();
                Finish();
            }
            catch (BusinessException e)
            {
                Log.Error(TAG, e.Message);
                Toast.MakeText(this, e.Message, ToastLength.Short).Show();
            }
            catch (Exception e)
            {
                Log.Error(TAG, e.Message);
                Toast.MakeText(this, Resource.String.add_vehicle_message_error, ToastLength.Short).Show();  
            }
        }

        private double ProcessCylinderCapacity(string cylinderCapacity)
        {
            if(string.IsNullOrEmpty(cylinderCapacity))
            {
                return 0;
            }
            else
            {
                return double.Parse(cylinderCapacity);
            }
        }

    }
}
