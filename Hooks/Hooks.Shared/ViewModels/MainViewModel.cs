using Hooks.API;
using Hooks.Common;
using Hooks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Web.Http;

namespace Hooks.ViewModels
{
    public class MainViewModel
    {
        public RelayCommand AddHookCommand { get; private set; }
        //public RelayCommand ChangeDeviceNameCommand { get; private set; }
        //public RelayCommand OpenSettingsCommand { get; private set; }

        public MainViewModel()
        {
            AddHookCommand = new RelayCommand(AddHook, CanAddHook);
            //ChangeDeviceNameCommand = new RelayCommand(ChangeDeviceName, CanChangeDeviceName);
            //OpenSettingsCommand = new RelayCommand(OpenSettings, CanOpenSettings);
        }

        public HookList Hooks { get { return App.Hooks; } set { App.Hooks = value; } }
        public List<Hook> ApprovedHooks { get { return App.Hooks.Where(h => h.Approved == true).ToList(); } }
        public List<Hook> PendingHooks { get { return App.Hooks.Where(h => h.Approved == false).ToList(); } }

        #region COMMANDS_ACTIONS

        private bool CanAddHook()
        {
            return true;
        }

        private void AddHook()
        {
            App.RootFrame.Navigate(typeof(AddHookPage));
        }

        //private bool CanChangeDeviceName()
        //{
        //    return false;
        //}

        //private void ChangeDeviceName()
        //{
        //    App.RootFrame.Navigate(typeof(RegisterDevicePage));
        //}

        //private bool CanOpenSettings()
        //{
        //    return false;
        //}

        //private void OpenSettings()
        //{
        //    //App.RootFrame.Navigate(typeof(SettingsPage));
        //}

        #endregion
    }
}
