using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using DevDaysSpeakers.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using DevDaysSpeakers.Services;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace DevDaysSpeakers.ViewModel
{
    public class SpeakersViewModel : ReactiveObject
    {
        public ReactiveList<Speaker> Speakers { get; } = new ReactiveList<Speaker>();

        readonly ReactiveCommand getSpeakers;
        public ReactiveCommand GetSpeakers => this.getSpeakers;

        public SpeakersViewModel()
        {
            var service = DependencyService.Get<AzureService>();

            this.getSpeakers = ReactiveCommand.CreateFromObservable(() => service.GetSpeakers().ToObservable()
                .SelectMany(speakers => Observable.Start(() => 
                {
                    Speakers.Clear();
                    Speakers.AddRange(speakers);
                }, RxApp.MainThreadScheduler)));

            this.getSpeakers.IsExecuting
                .ToProperty(this, vm => vm.IsBusy, out busy);

            this.getSpeakers.ThrownExceptions
                .Subscribe(error => Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK"));
        }

        readonly ObservableAsPropertyHelper<bool> busy;
        public bool IsBusy
        {
            get { return busy.Value; }
        }
    }
}