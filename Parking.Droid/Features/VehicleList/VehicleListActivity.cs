
using System;
using System.Collections.Generic;

using Android.App;
using Android.Arch.Lifecycle;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Widget;
using Parking.Domain;

namespace Parking.Droid
{
    [Activity(Label = "@string/app_name")]
    public class VehicleListActivity : AppCompatActivity
    {
        private const string TAG = "VehicleListActivity";

        private EditText inputSearch;
        private RecyclerView vehicleRecyclerView;
        private VehicleAdapter vehicleAdapter;

        private VehicleListViewModel vehicleListViewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehicle_list);
            InitComponent();
        }

        private void InitComponent()
        {
            vehicleListViewModel = (VehicleListViewModel) new ViewModelProvider
                .AndroidViewModelFactory(Application).Create(Java.Lang.Class.FromType(typeof(VehicleListViewModel)));
            inputSearch = FindViewById<EditText>(Resource.Id.input_search);
            inputSearch.TextChanged += (sender, args) =>
            {
                string text = inputSearch.Text.ToUpper();
                vehicleAdapter.Filter(text);
            };
            vehicleRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view_vehicle_list);
            vehicleRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            vehicleAdapter = new VehicleAdapter(GetVehicleList(), this);
            vehicleRecyclerView.SetAdapter(vehicleAdapter);
        }

        public List<VehicleDto> GetVehicleList()
        {
            try
            {
                return vehicleListViewModel.GetUserList();
            }
            catch (DataBaseException e)
            {
                Log.Error(TAG, e.Message);
                Toast.MakeText(this, e.Message, ToastLength.Short).Show();
            }
            catch (Exception e)
            {
                Log.Error(TAG, e.Message);
                Toast.MakeText(this, Resource.String.error_message, ToastLength.Short).Show();
            }
            return new List<VehicleDto>();        
        }

        public int CalculateValueParking(VehicleDto vehicleDto)
        {
            vehicleDto.VehicleDepartureTime = DateTime.UtcNow;
            return vehicleListViewModel.CalculateValueParking(vehicleDto);
        }

        public void DeleteVehicleFromDialogFragment(VehicleDto vehicleDto, int position)
        {
            try
            {
                vehicleListViewModel.DeleteVehicle(vehicleDto);
            }
            catch (DataBaseException e)
            {
                Log.Error(TAG, e.Message);
                Toast.MakeText(this, e.Message, ToastLength.Short).Show();
            }
            catch (Exception e)
            {
                Log.Error(TAG, e.Message);
                Toast.MakeText(this, Resource.String.error_message, ToastLength.Short).Show();
            }
            
            vehicleAdapter.vehicleList.RemoveAt(position);
            inputSearch.Text = "";
            vehicleAdapter.NotifyDataSetChanged();
        }

    }
}
