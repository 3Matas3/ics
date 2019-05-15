using Caliburn.Micro;
using SimpleTeamApp.App.Services;
using SimpleTeamApp.App.Services.Events;

namespace SimpleTeamApp.App.ViewModels
{
    public class MainViewModel : Conductor<IScreen>, IHandle<ShowViewModelMessage>
    {
        public MainViewModel(IEventAggregator eventAggregator, IoCProxy ioc)
        {
            eventAggregator.Subscribe(this);
            Handle(new ShowViewModelMessage(ioc.Get<LoginPageViewModel>()));
        }

        public void Handle(ShowViewModelMessage message)
        {
            ActivateItem(message.ViewModel);
        }
    }
}
