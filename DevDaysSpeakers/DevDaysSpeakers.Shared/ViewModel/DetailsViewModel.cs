using DevDaysSpeakers.Model;
using Plugin.TextToSpeech;
using ReactiveUI;
using Splat;
using System;
using System.Reactive.Linq;
using Xamarin.Forms;
using Xamvvm;

namespace DevDaysSpeakers.ViewModel
{
    public class DetailsViewModel
        : BasePageModelRxUI
    {
        public Speaker Speaker { get; set; }

        public string Name => Speaker.Name;

        public string Title => Speaker.Title;

        public string Description => Speaker.Description;

        public string Avatar => Speaker.Avatar;

        //public ReactiveCommand Speak { get; }

        //public ReactiveCommand VisitWebSite { get; }

        public DetailsViewModel()
        {
            //Speak = ReactiveCommand.CreateFromObservable(() => Observable.Start(() => 
            //    CrossTextToSpeech.Current.Speak(speaker.Description)));

            //var canVisitWebSite = this.WhenAnyValue(vm => vm.Speaker)
            //    .Select(speaker => speaker.Website.StartsWith("http"));
            //VisitWebSite = ReactiveCommand.CreateFromObservable(
            //    () => Observable.Start(() => Device.OpenUri(new Uri(speaker.Website))),
            //    canVisitWebSite);
        }
    }
}