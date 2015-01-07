using Hooks.API;
using Hooks.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            App.Hooks = await HookAPI.Instance.GetHooks();

            Frame.Navigate(DeviceAPI.IsRegistered == true ? typeof(MainPage) : typeof(RegisterDevicePage));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Frame.BackStack.Clear();
        }
    }
}
