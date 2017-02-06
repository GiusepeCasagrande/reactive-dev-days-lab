using DevDaysSpeakers.Model;
using DevDaysSpeakers.ViewModel;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xamarin.Forms;
using Xamvvm;

namespace DevDaysSpeakers.View
{
    public partial class SpeakersPage 
        : IBasePageRxUI<SpeakersViewModel>
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
                    .Subscribe(speaker => ViewModel.Speaker = speaker)
                    .DisposeWith(disposables);
            });
        }
    }
}
