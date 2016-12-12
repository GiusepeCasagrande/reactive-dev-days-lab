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

namespace DevDaysSpeakers.ViewModel
{
    public class SpeakersViewModel : ReactiveObject
    {
        public ReactiveList<Speaker> Speakers { get; } = new ReactiveList<Speaker>();

        readonly ReactiveCommand getSpeakers;
        public ReactiveCommand GetSpeakers => this.getSpeakers;

        public SpeakersViewModel()
        {
            this.getSpeakers = ReactiveCommand.CreateFromTask(async () => await GetSpeakersAsync());

            this.getSpeakers.IsExecuting.ToProperty(this, vm => vm.IsBusy, out busy);
        }

        readonly ObservableAsPropertyHelper<bool> busy;
        public bool IsBusy
        {
            get { return busy.Value; }
        }
        
        async Task GetSpeakersAsync()
        {
            if (IsBusy)
                return;

            Exception error = null;
            try
            {
                var service = DependencyService.Get<AzureService>();
                var items = await service.GetSpeakers();

                Speakers.Clear();
                foreach (var item in items)
                    Speakers.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
                error = ex;
            }

            if (error != null)
                await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");
        }
    }
}