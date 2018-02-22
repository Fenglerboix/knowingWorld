namespace myFirstApp.ViewModels
{
    using Models;
    using System.Collections.Generic;
    class MainViewModel
    {
        #region Properties
        
        public List<Country> CountriesList
        {
            get;
            set;
        }

        #endregion
        #region ViewModel
        public LoginViewModel Login
        {
            get;
            set;
        }

        public CountriesViewModel Countries
        {
            get;
            set;
        }

        public CountryViewModel Country
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion
    }
}
