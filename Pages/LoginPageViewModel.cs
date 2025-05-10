using BabyMonitoringApp.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BabyMonitoringApp.Pages
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;
        public LoginPageViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        // Private methods
        [RelayCommand]
        private async Task SignInWithGoogle()
        {
            try
            {
                // Simulate failure
                throw new Exception("Google login failed");
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
                // Simulate failure
                throw new Exception("Facebook login failed");
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync("Login Failed", ex.Message, "OK");
            }
        }
    }
}
