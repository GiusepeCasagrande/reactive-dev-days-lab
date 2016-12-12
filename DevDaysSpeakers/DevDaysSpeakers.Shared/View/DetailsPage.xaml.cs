using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using DevDaysSpeakers.Model;
using Plugin.TextToSpeech;
using DevDaysSpeakers.ViewModel;
using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace DevDaysSpeakers.View
{
    public partial class DetailsPage : ContentPage, IActivatable
    {
        Speaker speaker;
        public DetailsPage(Speaker item)
        {
            InitializeComponent();
            this.speaker = item;

            BindingContext = this.speaker;

            this.WhenActivated(disposables =>
            {
                ButtonSpeak.Events().Clicked
                    .Subscribe(_ =>
                    {
                        CrossTextToSpeech.Current.Speak(this.speaker.Description);
                    })
                    .DisposeWith(disposables);

                ButtonWebsite.Events().Clicked
                    .Subscribe(_ =>
                    {
                        if (speaker.Website.StartsWith("http"))
                            Device.OpenUri(new Uri(speaker.Website));
                    })
                    .DisposeWith(disposables);
            });
        }
    }
}
