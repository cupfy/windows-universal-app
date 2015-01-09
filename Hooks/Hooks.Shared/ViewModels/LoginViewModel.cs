using Hooks.API;
using Hooks.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Web.Http;

namespace Hooks.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public RelayCommand LoginCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(SubmitLogin, CanSubmitLogin);

            Password = "";
        }

        public string Email { get { return email; } set { email = value; LoginCommand.RaiseCanExecuteChanged(); } }
        private string email = "";

        public string Password { get { return password; } set { password = value; LoginCommand.RaiseCanExecuteChanged(); } }
        private string password = "";

        #region COMMANDS_ACTIONS

        private bool CanSubmitLogin()
        {
            return !String.IsNullOrEmpty(Email);// && !String.IsNullOrEmpty(Password);
        }

        private async void SubmitLogin()
        {
            if(await UserAPI.Instance.Login(Email, Password) != null)
            {
                App.RootFrame.Navigate(typeof(MainPage));
            }
        }

        #endregion
    }
}
