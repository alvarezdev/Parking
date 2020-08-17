using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace Parking.Droid.Test
{
    [TestFixture]
    public class ParkingDroidTest
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp
                .Android                
                .ApkFile("../../../Parking.Droid/bin/Release/com.ceiba.parking.apk")
                .EnableLocalScreenshots()
                .DeviceSerial("5203fe3cfa1fc34d")
                .StartApp();
                //.StartApp(Xamarin.UITest.Configuration.AppDataMode.Clear);
        }

        [Test]
        public void EnterParking_EnterCarParkingAndMessageInToastSuccess_Test()
        {
            app.WaitForElement(b => b.Button("button_add_vehicle"));
            app.Tap(b => b.Button("button_add_vehicle"));

            app.WaitForElement(tf => tf.TextField("input_plate"));
            app.Tap(x => x.Marked("input_plate"));
            app.EnterText(x => x.Marked("input_plate"), "FIS101");
            app.DismissKeyboard();

            app.WaitForElement(tf => tf.TextField("input_cylinder_capacity"));
            app.Tap(x => x.Marked("input_cylinder_capacity"));
            app.EnterText(x => x.Marked("input_cylinder_capacity"), "1500");
            app.DismissKeyboard();

            app.Tap(rb => rb.Marked("radio_button_car"));

            app.WaitForElement(b => b.Button("btn_accept_add_vehicle"));
            app.Tap(b => b.Button("btn_accept_add_vehicle"));

            app.WaitForElement("Se agrego vehículo exitosamente");
        }

        [Test]
        public void LaunchREPL()
        {
            app.Repl();
        }

        /*[Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }*/
    }
}
