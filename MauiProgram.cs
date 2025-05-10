using BabyMonitoringApp.Pages;
using BabyMonitoringApp.Utils;
using Firebase.Auth;
using Firebase.Auth.Providers;
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
            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyCgljh67YIMVVFRIFaTfhBegPc8NyZHi20",
                AuthDomain = "babymonitoring-af3da.firebaseapp.com",
                Providers =
                [
                    new GoogleProvider(),
                    new FacebookProvider()
                ]
            }));

            return builder.Build();
        }
    }
}
