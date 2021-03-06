using MudBlazor;

namespace PersonalWebsite.Client.Shared
{
    public partial class MainLayout
    {
        private MudTheme darkTheme;
        private bool drawerOpen = true;


        public MainLayout()
        {
            darkTheme = new MudTheme()
            {
                Palette = new Palette()
                {
                    Black = "#27272f",
                    Background = "#32333d",
                    BackgroundGrey = "#27272f",
                    Surface = "#373740",
                    DrawerBackground = "#27272f",
                    DrawerText = "rgba(255,255,255, 0.50)",
                    DrawerIcon = "rgba(255,255,255, 0.50)",
                    AppbarBackground = "#27272f",
                    AppbarText = "rgba(255,255,255, 0.70)",
                    TextPrimary = "rgba(255,255,255, 0.70)",
                    TextSecondary = "rgba(255,255,255, 0.50)",
                    ActionDefault = "#adadb1",
                    ActionDisabled = "rgba(255,255,255, 0.26)",
                    ActionDisabledBackground = "rgba(255,255,255, 0.12)"
                }
            };
        }

        private void DrawerToggle()
        {
            drawerOpen = !drawerOpen;
        }
    }
}
