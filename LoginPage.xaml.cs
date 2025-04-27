namespace BabyMonitoringApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void OnGoogleLoginClicked(object sender, TappedEventArgs e)
        {
            DisplayAlert("Login", "Google Login Clicked", "OK");
        }

        private void OnFacebookLoginClicked(object sender, TappedEventArgs e)
        {
            // TODO: Implement Facebook login logic
            DisplayAlert("Login", "Facebook Login Clicked", "OK");
        }
    }

}
