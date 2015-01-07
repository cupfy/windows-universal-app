using Hooks.API;
using Hooks.Common;
using Hooks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.Web.Http;

namespace Hooks.ViewModels
{
    public class MainViewModel : Notifiable
    {
        public RelayCommand AddHookCommand { get; private set; }
        public RelayCommand RefreshCommand { get; private set; }
        //public RelayCommand ChangeDeviceNameCommand { get; private set; }
        //public RelayCommand OpenSettingsCommand { get; private set; }

        public MainViewModel()
        {
            AddHookCommand = new RelayCommand(AddHook);
            RefreshCommand = new RelayCommand(Refresh);
            //ChangeDeviceNameCommand = new RelayCommand(ChangeDeviceName);
            //OpenSettingsCommand = new RelayCommand(OpenSettings);

            App.Hooks.CollectionChanged += Hooks_CollectionChanged;
        }

        public HookList Hooks { get { return App.Hooks; } set { App.Hooks = value; } }
        public List<Hook> ApprovedHooks { get { return App.Hooks.Where(h => h.Approved == true).ToList(); } }
        public List<Hook> PendingHooks { get { return App.Hooks.Where(h => h.Approved == false).ToList(); } }

        private void Hooks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyChanges();
        }

        #region COMMANDS_ACTIONS

        private void AddHook()
        {
            App.RootFrame.Navigate(typeof(AddHookPage));
        }

        private async void Refresh()
        {
#if WINDOWS_PHONE_APP
            StatusBar.GetForCurrentView().ProgressIndicator.Text = "Updating...";
            await StatusBar.GetForCurrentView().ProgressIndicator.ShowAsync();
#endif

            App.Hooks = await HookAPI.Instance.GetHooks();

#if WINDOWS_PHONE_APP
            await Task.Delay(0500);
            StatusBar.GetForCurrentView().ProgressIndicator.Text = String.Empty;
            await StatusBar.GetForCurrentView().ProgressIndicator.HideAsync();
#endif
        }

        //private void ChangeDeviceName()
        //{
        //    App.RootFrame.Navigate(typeof(RegisterDevicePage));
        //}

        //private void OpenSettings()
        //{
        //    //App.RootFrame.Navigate(typeof(SettingsPage));
        //}

        #endregion
    }
}
