namespace Memory
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private void Normal_Clicked(object sender, EventArgs e)
        {
            GlobalManager.LoadingOverlay(LoadingOverlay,Navigation,new PlayPage(true));
        }

        private void RandomColor_Clicked(object sender, EventArgs e)
        {
            GlobalManager.LoadingOverlay(LoadingOverlay, Navigation, new PlayPage(false));
        }
    }

}
