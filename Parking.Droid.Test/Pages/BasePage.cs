using System;
using Xamarin.UITest;
using Xamarin.UITest.Android;

namespace Parking.Droid.Test
{
    public class BasePage
    {
        protected static AndroidApp app;

        const string ApkPath = "../../../Parking.Droid/bin/Release/com.ceiba.parking.apk";
        const string DeviceId = "5203fe3cfa1fc34d";

        public BasePage() { }

        protected void InitAndroidApp()
        {
            app = ConfigureApp
                .Android
                //.ApkFile(ApkPath)
                .Debug()
                .EnableLocalScreenshots()
                //.DeviceSerial(DeviceId)
                .StartApp();
        }

        public static void Click(string resourceId)
        {
            app.WaitForElement(b => b.Button(resourceId));
            app.Tap(b => b.Button(resourceId));
        }

        public void TypeText(string resourceId, string text)
        {
            app.WaitForElement(tf => tf.TextField(resourceId));
            app.Tap(x => x.Marked(resourceId));
            app.EnterText(x => x.Marked(resourceId), text);
            app.DismissKeyboard();
        }

        public static void CheckMessageToast(string resourceId)
        {
            app.WaitForElement(resourceId);
        }

        public static void CheckNotExistElement(string resourceId)
        {
            app.WaitForNoElement(x => x.Marked(resourceId));
        }
    }
}
