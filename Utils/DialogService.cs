
namespace BabyMonitoringApp.Utils
{
    public class DialogService : IDialogService
    {
        public async Task ShowAlertAsync(string title, string message, string cancel)
        {
            var currentPage = Application.Current?.MainPage;
            if (currentPage != null)
            {
                await currentPage.DisplayAlert(title, message, cancel);
            }
        }
    }
}
