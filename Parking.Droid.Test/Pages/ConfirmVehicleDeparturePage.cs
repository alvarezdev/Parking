namespace Parking.Droid.Test
{
    public class DialogConfirmVehicleDeparturePage : BasePage
    {
        public static string Tittle = "title_advice";

        public static string ButtonAccept = "accept_dialog";

        public DialogConfirmVehicleDeparturePage() { }

        public static void ClickButtonDialogConfirm(string resourceId)
        {
            app.WaitForElement(x => x.Marked(Tittle));
            Click(resourceId);
        }
    }
}
