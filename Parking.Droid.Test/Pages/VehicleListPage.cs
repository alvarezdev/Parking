using System;
namespace Parking.Droid.Test
{
    public class VehicleListPage : BasePage
    {
        public static string EditTextSearch = "input_search";

        public static string ButtonItemRecyclerView = "leave_vehicle";

        public static string RecyclerView = "recycler_view_vehicle_list";

        public static string MessageListEmptyToast = "No se ha encontrado ningun vehículo con esa placa";

        public VehicleListPage() { }

        public static void ClickElementOfAnItemList()
        {
            Click(ButtonItemRecyclerView);
        }

        public static void CheckNotExistItemList(string text)
        {
            app.WaitForNoElement(x => x.Text(text));
        }

        /*public static void ClickElementOfAnItemList(string recyclerId, string itemComponentid, int position)
        {
            Click()

            //app.WaitForElement(x => x.Frame("recycler_view_vehicle_list").Index(1).Text("FIS300"));
            //app.Tap(x => x.Class("recycler_view_vehicle_list").Index(1).Child("leave_vehicle"));
        }*/
    }
}
