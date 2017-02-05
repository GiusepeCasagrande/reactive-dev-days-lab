
using DevDaysSpeakers.Model;
using DevDaysSpeakers.ViewModel;
using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Xamarin.Forms;

namespace DevDaysSpeakers.View
{
    public partial class SpeakersPage 
        : ReactiveContentPage<SpeakersViewModel>
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
                    .SelectMany(speaker => ViewModel.HostScreen.Router.NavigationStack.Add(new DetailsViewModel(speaker)))
                    .Do(_ => ListViewSpeakers.SelectedItem = null)
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }
    }
}
