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
    public class TeamSettingsViewModel : ViewModelBase
    {
        private string name;
        private string description;

        public string Name
        {
            get { return name; }
            set
            {
                Set(ref name, value);
                Validate();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                Set(ref description, value);
                Validate();
            }
        }

        //TODO Obrázek týmu

        public void Delete()
        {
            //TODO Are you sure?
            //TODO Mazání
            MessageBoxService.ShowMessageBox("Tým byl smazán.", "Úspěch");
            GoBack();
        }

        public void GoBack()
        {
            var model = IoC.Get<TeamPageViewModel>();
            EventAggregator.PublishOnUIThread(new ShowViewModelMessage(model));
        }

        public void Save()
        {
            if (name == null)
            {
                MessageBoxService.ShowErrorMessageBox(new Exception("Invalid team name"));
            }

            else
            {
                //TODO Save
                MessageBoxService.ShowMessageBox("Nastavení týmu bylo aktualizováno.", "Uloženo.");
                GoBack();
            }
        }
    }
}
