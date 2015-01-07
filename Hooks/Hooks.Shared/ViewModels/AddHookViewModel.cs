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
    public class AddHookViewModel
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
            Hook hook = await HookAPI.Instance.Subscribe(Namespace);

            if (hook != null)
            {
                App.Hooks.Add(hook);
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
