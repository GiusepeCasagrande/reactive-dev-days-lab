using DevDaysSpeakers.View;
using DevDaysSpeakers.ViewModel;
using Xamarin.Forms;

namespace DevDaysSpeakers
{
    public class App : Application
    {
        public AppBootstrapper AppBootstrapper { get; }

        public App()
        {
            // The root page of your application
            var content = new SpeakersPage();

            AppBootstrapper = new AppBootstrapper();
            MainPage = new NavigationPage(content);
        }
      
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
