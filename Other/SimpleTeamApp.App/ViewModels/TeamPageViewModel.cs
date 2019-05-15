using Caliburn.Micro;
using SimpleTeamApp.App.Services;
using SimpleTeamApp.App.Services.Events;
using SimpleTeamApp.App.Services.MessageBoxService;
using SimpleTeamApp.App.UWPHelper.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTeamApp.App.ViewModels
{
    public class TeamPageViewModel : ViewModelBase
    {
        public void Settings()
        {
            var model = IoC.Get<TeamSettingsViewModel>();
            EventAggregator.PublishOnUIThread(new ShowViewModelMessage(model));
        }

        public void Leave()
        {
            //TODO Are you sure?
            //TODO opustit tým
            var model = IoC.Get<MainPageViewModel>();
            EventAggregator.PublishOnUIThread(new ShowViewModelMessage(model));
        }
    }
}
