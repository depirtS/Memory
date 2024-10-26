using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    public static class GlobalManager
    {
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
