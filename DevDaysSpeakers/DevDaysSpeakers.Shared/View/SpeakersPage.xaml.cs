
using Xamarin.Forms;

using System;
using DevDaysSpeakers.Model;
using DevDaysSpeakers.ViewModel;
using Splat;
using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace DevDaysSpeakers.View
{
    public partial class SpeakersPage : ReactiveContentPage<SpeakersViewModel>
    {
        public SpeakersPage()
        {
            InitializeComponent();

            ViewModel = new SpeakersViewModel();
            BindingContext = ViewModel;

            this.WhenActivated(disposables =>
            {
                ListViewSpeakers.Events().ItemSelected
                    .Subscribe(async e =>
                    {
                        var speaker = e.SelectedItem as Speaker;
                        if (speaker == null)
                            return;

                        await Navigation.PushAsync(new DetailsPage(speaker));

                        ListViewSpeakers.SelectedItem = null;
                    })
                    .DisposeWith(disposables);
            });
        }
    }
}
