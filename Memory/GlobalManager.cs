using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    public static class GlobalManager
    {
        public static Dictionary<string, Color> colorsList = new Dictionary<string, Color>()
            {
                {"Gray", Colors.Gray},
                {"Dark gray", Colors.DarkGray},
                {"Orange", Colors.Orange},
                {"Dark orange", Colors.DarkOrange},
                {"Red", Colors.Red},
                {"Dark red", Colors.DarkRed},
                {"Yellow", Colors.Yellow},
                {"Blue", Colors.Blue},
                {"Dark blue", Colors.DarkBlue},
                {"Sky blue", Colors.SkyBlue},
                {"Pink", Colors.Pink},
                {"Purple",Colors.Purple},
                {"Violet",Colors.Violet},
                {"Dark violet", Colors.DarkViolet},
                {"Green", Colors.Green},
                {"Dark green",Colors.DarkGreen},
                {"Sea green", Colors.SeaGreen},
                {"Cyan", Colors.Cyan},
                {"Dark cyan", Colors.DarkCyan},
                {"Brown", Colors.Brown}
            };
        public async static void LoadingOverlay(Grid LoadingOverlay, INavigation Navigation)
        {
            LoadingOverlay.IsVisible = true;
            await Task.Delay(500);
            await Navigation.PopAsync();
            await Task.Delay(1500);
            LoadingOverlay.IsVisible = false;
        }

        public async static void LoadingOverlay(Grid LoadingOverlay, INavigation Navigation, Page PageToPush)
        {
            LoadingOverlay.IsVisible = true;
            await Task.Delay(500);
            await Navigation.PushAsync(PageToPush);
            await Task.Delay(1500);
            LoadingOverlay.IsVisible = false;
        }
    }
}
