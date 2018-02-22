namespace myFirstApp.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class LoginViewModel: BaseViewModel
    {
        #region Attributes

        private string password;
        private string email;
        private bool isRunning;
        private bool isEnabled;
        #endregion


        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value);  }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        #endregion

        #region Constructor

        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;

            this.Email = "juliohg8913@gmail.com";
            this.Password = "8913";
        }

        #endregion

        #region Commands

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
            
        }        

        private async void Login()
        {          

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "There was an error!!",
                    "you must enter an email...",
                    "Ok");

                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "There was an error!!",
                    "you must enter a password...",
                    "Ok");

                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Password != "8913" || this.Email != "juliohg8913@gmail.com")
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "incorrect password or email, try again...",
                    "Ok");

                this.Password = string.Empty;
                return;

            }

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;

            MainViewModel.GetInstance().Countries = new CountriesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new CountriesPage());
        }
        #endregion
    }
}
