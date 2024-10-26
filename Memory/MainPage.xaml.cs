using CommunityToolkit.Maui.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Memory
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            IDispatcherTimer timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromMilliseconds(8001);
            timer.Tick += (s, e) =>
            {
                AnimatedBackground();
            };
            timer.Start();
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

        private void AnimatedBackground()
        {
            uint rate = 1;
            uint animatedTime = 2000;
            AnimatedFirstBackgorundButton(rate, animatedTime);
            AnimatedSecondBackgorundButton(rate, animatedTime);
            AnimatedThreeBackgorundButton(rate, animatedTime);
            AnimatedFourBackgorundButton(rate, animatedTime);
        }
        private async void AnimatedFirstBackgorundButton(uint rate, uint animatedTime)
        {
            await First.BackgroundColorTo(Colors.Blue, rate, animatedTime);
            await First.BackgroundColorTo(Colors.Yellow, rate, animatedTime);
            await First.BackgroundColorTo(Colors.Green, rate, animatedTime);
            await First.BackgroundColorTo(Colors.Red, rate, animatedTime);
        }

        private async void AnimatedSecondBackgorundButton(uint rate, uint animatedTime)
        {
            await Second.BackgroundColorTo(Colors.Yellow, rate, animatedTime);
            await Second.BackgroundColorTo(Colors.Green, rate, animatedTime);
            await Second.BackgroundColorTo(Colors.Red, rate, animatedTime);
            await Second.BackgroundColorTo(Colors.Blue, rate, animatedTime);
        }

        private async void AnimatedThreeBackgorundButton(uint rate, uint animatedTime)
        {
            await Three.BackgroundColorTo(Colors.Red, rate, animatedTime);
            await Three.BackgroundColorTo(Colors.Blue, rate, animatedTime);
            await Three.BackgroundColorTo(Colors.Yellow, rate, animatedTime);
            await Three.BackgroundColorTo(Colors.Green, rate, animatedTime);
        }

        private async void AnimatedFourBackgorundButton(uint rate, uint animatedTime)
        {
            await Four.BackgroundColorTo(Colors.Green, rate, animatedTime);
            await Four.BackgroundColorTo(Colors.Red, rate, animatedTime);
            await Four.BackgroundColorTo(Colors.Blue, rate, animatedTime);
            await Four.BackgroundColorTo(Colors.Yellow, rate, animatedTime);
        }
    }

}
