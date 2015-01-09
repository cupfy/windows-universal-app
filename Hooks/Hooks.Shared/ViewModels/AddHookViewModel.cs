using Hooks.API;
using Hooks.Common;
using System;

namespace Hooks.ViewModels
{
    public class AddHookViewModel : BaseViewModel
    {
        public RelayCommand AddHookCommand { get; private set; }
        public RelayCommand OpenWebsiteCommand { get; private set; }

        public AddHookViewModel()
        {
            AddHookCommand = new RelayCommand(AddHook);
            OpenWebsiteCommand = new RelayCommand(OpenWebsite);
        }

        public string Namespace { get { return _namespace; } set { _namespace = value; AddHookCommand.RaiseCanExecuteChanged(); } }
        private string _namespace = "";

        #region COMMANDS_ACTIONS

        private async void AddHook()
        {
            if (String.IsNullOrEmpty(Namespace)) return;

            var res = await DeviceAPI.Instance.Hook(Namespace);

            if (res != null)
            {
                App.Hooks = res.Hooks;
                App.DeviceInfo = res.Device;

                App.RootFrame.Navigate(typeof(MainPage));
            }
        }

        private async void OpenWebsite()
        {
            await Windows.System.Launcher.LaunchUriAsync(BaseAPI.WEBVERSION_URI);
        }

        #endregion
    }
}
