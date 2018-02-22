namespace myFirstApp.ViewModels
{
    using Models;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class CountryViewModel : BaseViewModel
    {

        #region Attributes
        private ObservableCollection<Borders> borders;
        #endregion


        #region Properties
        public Country Country
        {
            get;
            set;
        }

        public ObservableCollection<Borders> Borders
        {
            get { return this.borders; }
            set { SetValue(ref this.borders, value); }

        }
        #endregion

        #region Constructor
        public CountryViewModel(Country country)
        {
            this.Country = country;
            this.LoadBorders();
        }


        #endregion

        #region Methods

        private void LoadBorders()
        {
            this.Borders = new ObservableCollection<Borders>();

            foreach (string border in this.Country.Borders)
            {
                var country = MainViewModel.GetInstance().CountriesList.
                                            Where(x => x.Alpha3Code == border).
                                            FirstOrDefault();

                if (country != null)
                {
                    this.Borders.Add(new Borders
                    {
                        Code = country.Alpha3Code,
                        Name = country.Name,
                    });

                }
            }
        }
        #endregion
    }
}
