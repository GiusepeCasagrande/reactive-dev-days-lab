using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DevDaysSpeakers.ViewModel
{
	public class AppBootstrapper
		: ReactiveObject
		, IScreen
	{
		public RoutingState Router { get; }

		public AppBootstrapper(
			IMutableDependencyResolver dependencyResolver = null,
			RoutingState testRouter = null)
		{
			Router = testRouter ?? new RoutingState();
			dependencyResolver = dependencyResolver ?? Locator.CurrentMutable;

			dependencyResolver.RegisterConstant(this, typeof(IScreen));
			//dependencyResolver.Register(() => new SpeakersViewModel(), typeof(IViewFor<SpeakersViewModel>));
			//dependencyResolver.Register(() => new DetailsViewModel(), typeof(IViewFor<DetailsViewModel>));

			// Router.Navigate.Execute(new SpeakersViewModel(this));
			// Router.NavigationStack.Add(new DetailsViewModel(this));
		}

		public Page CreateMainView()
		{
			return new RoutedViewHost();
		}
	}
}
