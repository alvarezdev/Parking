using System;
namespace Parking.Droid.Test
{
    public class AddVehiclePage : BasePage
    {
        public static string EditTextPlate = "input_plate";        

        public static string EditTextCylinderCapacity = "input_cylinder_capacity";        

        public static string RadioButtonCar = "radio_button_car";        

        public static string RadioButtonMotorcycle = "radio_button_motorcycle";        

        public static string ButtonAccept = "btn_accept_add_vehicle";

        public static string MessageToastSuccess = "Se agrego vehículo exitosamente";        

        public static string MessageWarningToast = "No hay diligenciado correctamente los campos de texto";        

        public AddVehiclePage() { }

        public static void ClickRadioButton(string resourceId)
        {
            app.Tap(rb => rb.Marked(resourceId));
        }        
    }
}
