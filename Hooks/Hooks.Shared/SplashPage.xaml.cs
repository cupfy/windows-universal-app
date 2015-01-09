using Hooks.API;
using Hooks.Utils;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Hooks
{
    public sealed partial class SplashPage : Page
    {
        public SplashPage()
        {
            this.InitializeComponent();

            LoadApplication();
        }

        async void LoadApplication()
        {
            App.ChannelUri = AppSettings.Instance.ChannelUri;

            //get hooks and device info
            var res = await DeviceAPI.Instance.GetDeviceInfoAndHooks();
            
            if (res != null)
            {
                App.Hooks = res.Hooks;
                App.DeviceInfo = res.Device;
            }

            Frame.Navigate(App.DeviceInfo == null ? typeof(RegisterDevicePage) : typeof(MainPage));

            //update channel uri
            App.ChannelUri = await DeviceAPI.Instance.UpdateChannel(App.ChannelUri);
            AppSettings.Instance.ChannelUri = App.ChannelUri;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Frame.BackStack.Clear();
        }
    }
}
