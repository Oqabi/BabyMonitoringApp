using BabyMonitoringApp.Pages;
using BabyMonitoringApp.Utils;
using Microsoft.Extensions.Logging;

namespace BabyMonitoringApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-Regular.ttf", "Roboto");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Add services
            builder.Services.AddSingleton<IDialogService, DialogService>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginPageViewModel>();

            return builder.Build();
        }
    }
}
