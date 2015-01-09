using Hooks.API;
using Hooks.Common;
using System;

namespace Hooks.ViewModels
{
    public class RegisterDeviceViewModel : BaseViewModel
    {
        public RelayCommand RegisterOrUpdateDeviceCommand { get; private set; }

        public RegisterDeviceViewModel()
        {
            RegisterOrUpdateDeviceCommand = new RelayCommand(RegisterOrUpdateDevice);

            App.DeviceInfoChanged += (s, e) => { if (App.DeviceInfo != null) { DeviceName = App.DeviceInfo.Name; } NotifyChanges(); };
        }

        public string DeviceName { get { return deviceName; } set { deviceName = value; RegisterOrUpdateDeviceCommand.RaiseCanExecuteChanged(); } }
        private string deviceName = "";

        public string DeviceModelName { get { return DeviceAPI.DEVICE_MODEL; } }

        #region COMMANDS_ACTIONS

        private async void RegisterOrUpdateDevice()
        {
            if (String.IsNullOrEmpty(DeviceName)) return;

            if (App.DeviceInfo == null)
                App.DeviceInfo = await DeviceAPI.Instance.Register(DeviceName, App.ChannelUri);
            else
                App.DeviceInfo = await DeviceAPI.Instance.UpdateInfo(DeviceName);

            if (App.DeviceInfo != null)
                App.RootFrame.Navigate(typeof(MainPage));
        }

        #endregion
    }
}
