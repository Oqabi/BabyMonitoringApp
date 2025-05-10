namespace BabyMonitoringApp.Utils
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string title, string message, string cancel);
    }
}
