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
    public class ParkingDroidTest : BasePage
    {
        [SetUp]
        public void BeforeEachTest()
        {
            InitAndroidApp();
        }

        //[Test]
        public void LaunchREPL()
        {
            app.Repl();
        }

        [Test]
        public void EnterParking_EnterCarParkingAndMessageInToastSuccess_Test()
        {
            //Given
            Click(HomePage.ButtonAddVehicle);

            TypeText(AddVehiclePage.EditTextPlate, "FIS200");
            TypeText(AddVehiclePage.EditTextCylinderCapacity, "1500");
            AddVehiclePage.ClickRadioButton(AddVehiclePage.RadioButtonCar);

            //When
            Click(AddVehiclePage.ButtonAccept);

            //Then
            CheckMessageToast(AddVehiclePage.MessageToastSuccess);
        }

        [Test]
        public void EnterParking_EnterMotorcycleParkingAndMessageInToastSuccess_Test()
        {
            //Given
            Click(HomePage.ButtonAddVehicle);

            TypeText(AddVehiclePage.EditTextPlate, "DPA78D");
            TypeText(AddVehiclePage.EditTextCylinderCapacity, "200");
            AddVehiclePage.ClickRadioButton(AddVehiclePage.RadioButtonMotorcycle);

            //When
            Click(AddVehiclePage.ButtonAccept);

            //Then
            CheckMessageToast(AddVehiclePage.MessageToastSuccess);
        }

        [Test]
        public void EnterParking_FillFailedFieldsWhenEnterCarParkingAndDisplayMessageInToastWarning_Test()
        {
            //Given
            Click(HomePage.ButtonAddVehicle);

            TypeText(AddVehiclePage.EditTextPlate, "");
            TypeText(AddVehiclePage.EditTextCylinderCapacity, "200");
            AddVehiclePage.ClickRadioButton(AddVehiclePage.RadioButtonCar);

            //When
            Click(AddVehiclePage.ButtonAccept);

            //Then
            CheckMessageToast(AddVehiclePage.MessageWarningToast);
        }

        [Test]
        public void SearchPlate_FailedResultsAndEmptyListMessage_Test()
        {
            //Given
            Click(HomePage.ButtonShowVehicleList);

            //When
            TypeText(VehicleListPage.EditTextSearch, "ZZZ999");

            //Then
            CheckMessageToast(VehicleListPage.MessageListEmptyToast);
        }

        [Test]
        public void LeaveVehicle_LeaveCarFromParkingAndRemoveToVehicleListSuccessful_Test()
        {            
            //Given
            Click(HomePage.ButtonAddVehicle);
            TypeText(AddVehiclePage.EditTextPlate, "FIS330");
            TypeText(AddVehiclePage.EditTextCylinderCapacity, "1500");
            AddVehiclePage.ClickRadioButton(AddVehiclePage.RadioButtonCar);
            Click(AddVehiclePage.ButtonAccept);            
            CheckMessageToast(AddVehiclePage.MessageToastSuccess);

            //When
            Click(HomePage.ButtonShowVehicleList);
            TypeText(VehicleListPage.EditTextSearch, "FIS330");
            VehicleListPage.ClickElementOfAnItemList();            
            DialogConfirmVehicleDeparturePage.
                ClickButtonDialogConfirm(DialogConfirmVehicleDeparturePage.ButtonAccept);

            //Then
            CheckNotExistElement(DialogConfirmVehicleDeparturePage.Tittle);
            VehicleListPage.CheckNotExistItemList("FIS330");
        }

    }
}
