using BabyMonitoringApp.Platforms.Windows.Authentication;
using BabyMonitoringApp.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;

namespace BabyMonitoringApp.Pages
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;
        private readonly FirebaseAuthClient authClient;
        public LoginPageViewModel(IDialogService dialogService, FirebaseAuthClient firebaseAuthClient)
        {
            _dialogService = dialogService;
            authClient = firebaseAuthClient;
        }

        // Private methods
        [RelayCommand]
        private async Task SignInWithGoogle()
        {
            string clientId = "79037211679-dr0al5ce7nutsnbjdr7e7loj0m6nsh3j.apps.googleusercontent.com";

            try
            {
                if(DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    string userName = await OAuthLoginHandler.SignInWithOAuth(OAuthProvider.Google);
                    await _dialogService.ShowAlertAsync("Login Succeeded", $"Welcome {userName}!", "OK");
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync("Login Failed", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task SignInWithFacebook()
        {
            try
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    string userName = await OAuthLoginHandler.SignInWithOAuth(OAuthProvider.Facebook);
                    await _dialogService.ShowAlertAsync("Login Succeeded", $"Welcome {userName}!", "OK");
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync("Login Failed", ex.Message, "OK");
            }
        }
    }
}
