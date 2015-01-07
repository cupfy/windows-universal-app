using Hooks.API;
using Hooks.Common;
using System;

namespace Hooks.ViewModels
{
    public class RegisterDeviceViewModel
    {
        public RelayCommand RegisterDeviceCommand { get; private set; }

        public RegisterDeviceViewModel()
        {
            RegisterDeviceCommand = new RelayCommand(RegisterDevice);
        }

        public string DeviceName { get { return deviceName; } set { deviceName = value; RegisterDeviceCommand.RaiseCanExecuteChanged(); } }
        private string deviceName = "";

        public string DeviceModelName { get { return DeviceAPI.DEVICE_MODEL; } }

        #region COMMANDS_ACTIONS

        private async void RegisterDevice()
        {
            if(await DeviceAPI.Instance.Register(DeviceName))
            {
                App.RootFrame.Navigate(typeof(MainPage));
            }
        }

        #endregion
    }
}
