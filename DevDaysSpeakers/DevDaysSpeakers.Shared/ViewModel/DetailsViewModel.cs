using DevDaysSpeakers.Model;
using DevDaysSpeakers.Services;
using Plugin.TextToSpeech;
using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Xamarin.Forms;

namespace DevDaysSpeakers.ViewModel
{
    public class DetailsViewModel
        : ReactiveObject
        , IRoutableViewModel
    {
        public string UrlPathSegment => GetType().FullName;

        public IScreen HostScreen { get; }

        public ReactiveCommand Speak { get; }

        public ReactiveCommand VisitWebSite { get; }

        public DetailsViewModel(
            Speaker speaker,
            IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            Speak = ReactiveCommand.CreateFromObservable(
                () => Observable.Start(() => CrossTextToSpeech.Current.Speak(speaker.Description)));

            var canVisitWebSite = Observable.Start(() => speaker.Website.StartsWith("http"));
            VisitWebSite = ReactiveCommand.CreateFromObservable(
                () => Observable.Start(() => Device.OpenUri(new Uri(speaker.Website))),
                canVisitWebSite);
        }
    }
}