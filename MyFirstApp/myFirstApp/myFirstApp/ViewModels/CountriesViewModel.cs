namespace myFirstApp.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class CountriesViewModel : BaseViewModel
    {
        #region Services

        private ApiService apiService;
        #endregion
        #region Attributes
        private ObservableCollection<CountryItemViewModel> countries;
        private string filter;
        private bool isRefreshing;
       
        #endregion

        #region Properties
        public ObservableCollection<CountryItemViewModel> Countries
        {
            get { return this.countries; }
            set { SetValue(ref this.countries, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }

        #endregion

        #region Constructor
        public CountriesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadCountries();
        }
        #endregion

        #region Methods

        private async void LoadCountries()
        {
            this.IsRefreshing = true;
            //var connection = await this.apiService.CheckConnection();

            //if (!connection.IsSuccess)
            //{
            //    this.IsRefreshing = false;
            //    await Application.Current.MainPage.DisplayAlert(
            //        "Error",
            //        connection.Message,
            //        "Accept");
            //    await Application.Current.MainPage.Navigation.PopAsync();
            //    return;

            //}
            var response = await this.apiService.GetList<Country>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            MainViewModel.GetInstance().CountriesList = (List<Country>)response.Result;
            this.Countries = new ObservableCollection<CountryItemViewModel>(
                this.ToCountryViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<CountryItemViewModel> ToCountryViewModel()
        {
            return MainViewModel.GetInstance().CountriesList.Select(l => new CountryItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations,
            });
        }
        #endregion
       
        #region Commands

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadCountries);
            }            
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Countries = new ObservableCollection<CountryItemViewModel>(
                 this.ToCountryViewModel());
            }
            else
            {
                this.Countries = new ObservableCollection<CountryItemViewModel>(
                     this.ToCountryViewModel().Where(l => 
                     l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                     l.Capital.ToLower().Contains(this.Filter.ToLower()) ));
            }
        }
        #endregion
    }
}
