using System;

namespace SimpleTeamApp.App.Services.MessageBoxService
{
    public interface IMessageBoxService
    {
        void ShowErrorMessageBox(Exception ex);
        void ShowMessageBox(string message, string title = null);
    }
}
