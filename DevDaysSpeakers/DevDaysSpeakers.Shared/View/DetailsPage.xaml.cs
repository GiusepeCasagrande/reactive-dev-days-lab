using DevDaysSpeakers.ViewModel;
using Xamvvm;

namespace DevDaysSpeakers.View
{
    public partial class DetailsPage 
        : IBasePageRxUI<DetailsViewModel>
    {
        public DetailsPage()
        {
            InitializeComponent();

            ViewModel = new DetailsViewModel();
            BindingContext = ViewModel;
        }
    }
}
