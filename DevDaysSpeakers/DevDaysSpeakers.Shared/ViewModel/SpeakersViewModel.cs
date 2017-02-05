using DevDaysSpeakers.Model;
using DevDaysSpeakers.Services;
using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Xamarin.Forms;

namespace DevDaysSpeakers.ViewModel
{
    public class SpeakersViewModel 
        : ReactiveObject
        , IRoutableViewModel
    {
        public string UrlPathSegment => GetType().FullName;

        public IScreen HostScreen { get; }

        public ReactiveList<Speaker> Speakers { get; } = new ReactiveList<Speaker>();

        public ReactiveCommand GetSpeakers { get; }

        readonly ObservableAsPropertyHelper<bool> busy;
        public bool IsBusy => busy.Value;

        public SpeakersViewModel(
            IScreen hostScreen = null, 
            AzureService azureService = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            var service = azureService ?? DependencyService.Get<AzureService>();

            GetSpeakers = ReactiveCommand.CreateFromObservable(() => service.GetSpeakers().ToObservable()
                .SelectMany(speakers => Observable.Start(() => 
                {
                    Speakers.Clear();
                    Speakers.AddRange(speakers);
                }, RxApp.MainThreadScheduler)));

            GetSpeakers.IsExecuting
                .ToProperty(this, vm => vm.IsBusy, out busy);

            GetSpeakers.ThrownExceptions
                .Subscribe(error => Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK"));
        }
    }
}