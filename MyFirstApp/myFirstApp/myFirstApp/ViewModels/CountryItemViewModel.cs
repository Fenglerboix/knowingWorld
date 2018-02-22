namespace myFirstApp.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class CountryItemViewModel : Country
    {

        #region Commands
        public ICommand SelectCountryCommand
        {
            get
            {
                return new RelayCommand(SelectCountry);
            }
            
        }
        #endregion

        #region Methods
        private async void SelectCountry()
        {
            MainViewModel.GetInstance().Country = new CountryViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new CountryTabbedPage());
        }

        #endregion
    }
}
