
using Xamarin.Forms;

using System;
using DevDaysSpeakers.Model;
using DevDaysSpeakers.ViewModel;
using Splat;
using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

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
                    .Select(e => e.SelectedItem as Speaker)
                    .Where(speaker => speaker != null)
                    .SelectMany(speaker => Navigation.PushAsync(new DetailsPage(speaker)).ToObservable())
                    .Do(_ => ListViewSpeakers.SelectedItem = null)
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }
    }
}
