using System;
using Android.App;
using Android.Runtime;
using Android.Util;
using Unity;

namespace Parking.Droid
{
    [Application]
    public class BaseApplication : Application
    {
        static UnityContainer DIContainer { get; set; }

        public BaseApplication() { }

        public BaseApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
            DIContainer = new UnityContainer();
            DependencyInjection.RegisterTypes(DIContainer);
        }

        public static TRequest Resolve<TRequest>()
        {
            if (DIContainer == null)
            {
                throw new InvalidOperationException("The injection container is not ready");
            }                
            return DIContainer.Resolve<TRequest>();
        }

        public override void OnCreate()
        {
            base.OnCreate();
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironmentUnhandledExceptionRaiser;
        }

        void AndroidEnvironmentUnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            Log.Error("QUEMES_ERROR", "Unhandled error: " + e.Exception);
        }   
    }
}
