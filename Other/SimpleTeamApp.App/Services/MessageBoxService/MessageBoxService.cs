using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Windows.UI.Popups;

namespace SimpleTeamApp.App.Services.MessageBoxService
{
    [ExcludeFromCodeCoverage] // UnitTesty netestovatelné. Používá UI Systému.
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowErrorMessageBox(Exception ex)
        {
            ShowMessageBox(CreateMessage(ex), "Chyba");
        }

        public void ShowMessageBox(string message, string title = null)
        {
            MessageDialog box;

            if (!string.IsNullOrEmpty(title))
                box = new MessageDialog(message, title);
            else
                box = new MessageDialog(message);

#pragma warning disable
            box.ShowAsync();
#pragma warning restore
        }

        private string CreateMessage(Exception ex)
        {
            var builder = new StringBuilder();

            while (ex != null)
            {
                builder.AppendLine(ex.Message);
                ex = ex.InnerException;
            }

            return builder.ToString();
        }
    }
}
