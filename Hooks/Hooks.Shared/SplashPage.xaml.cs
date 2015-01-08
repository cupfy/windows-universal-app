using Hooks.API;
using Hooks.Models;
using System;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
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
            //get hooks and update local IsRegistered property
            App.Hooks = await HookAPI.Instance.GetHooks();

            Frame.Navigate(DeviceAPI.IsRegistered == true ? typeof(MainPage) : typeof(RegisterDevicePage));

            //update channel uri
            await NotificationAPI.Instance.UpdateChannel();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Frame.BackStack.Clear();
        }
    }
}
