namespace myFirstApp.Infrastructure
{
    using ViewModels;
    
    class InstanceLocator
    {
        #region MyRegion
        public MainViewModel Main
        {
            get;
            set;
        }
        #endregion


        #region Constructor
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}
