using Caliburn.Micro;

namespace SimpleTeamApp.App.Services.Events
{
    public class ShowViewModelMessage
    {
        public Screen ViewModel { get; set; }

        public ShowViewModelMessage(Screen viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
