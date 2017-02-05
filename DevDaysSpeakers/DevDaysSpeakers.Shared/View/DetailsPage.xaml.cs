using DevDaysSpeakers.ViewModel;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace DevDaysSpeakers.View
{
    public partial class DetailsPage 
        : ReactiveContentPage<DetailsViewModel>
    {
        public DetailsPage()
        {
            InitializeComponent();

            ViewModel = new DetailsViewModel();
            BindingContext = ViewModel;
        }
    }
}
