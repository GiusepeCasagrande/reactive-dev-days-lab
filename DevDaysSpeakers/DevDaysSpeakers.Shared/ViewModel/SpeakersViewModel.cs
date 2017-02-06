using DevDaysSpeakers.Model;
using DevDaysSpeakers.Services;
using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Xamarin.Forms;
using Xamvvm;

namespace DevDaysSpeakers.ViewModel
{
    public class SpeakersViewModel 
        : BasePageModelRxUI
    {
        public ReactiveList<Speaker> Speakers { get; } = new ReactiveList<Speaker>();

        Speaker speaker;
        public Speaker Speaker
        {
            get { return speaker; }
            set { this.RaiseAndSetIfChanged(ref speaker, value); }
        }

        public ReactiveCommand GetSpeakers { get; }

        public ReactiveCommand GoToDetails { get; }

        readonly ObservableAsPropertyHelper<bool> busy;
        public bool IsBusy => busy.Value;

        public SpeakersViewModel()
            : this(null)
        {

        }

        public SpeakersViewModel(AzureService azureService = null)
        {
            var service = azureService ?? DependencyService.Get<AzureService>();

            GetSpeakers = ReactiveCommand.CreateFromObservable(() => 
                service.GetSpeakers().ToObservable()
                .SelectMany(speakers => Observable.Start(() => 
                {
                    Speakers.Clear();
                    Speakers.AddRange(speakers);
                }, RxApp.MainThreadScheduler)));

            GetSpeakers.IsExecuting
                .ToProperty(this, vm => vm.IsBusy, out busy);

            GetSpeakers.ThrownExceptions
                .Subscribe(error => Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK"));

            // go to details page when Speaker is set
            this.WhenAnyValue(vm => vm.Speaker)
                .Where(speaker => speaker != null)
                .SelectMany(speaker => this.PushPageFromCacheAsync<DetailsViewModel>(vm => vm.Speaker = speaker).ToObservable())
                .Subscribe();
        }
    }
}