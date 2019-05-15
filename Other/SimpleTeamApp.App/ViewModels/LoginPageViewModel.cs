using Caliburn.Micro;
using SimpleTeamApp.App.Services;
using SimpleTeamApp.App.Services.Events;
using SimpleTeamApp.App.Services.MessageBoxService;
using SimpleTeamApp.App.UWPHelper.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleTeamApp.App.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Services

        public LoginPageViewModel(IMessageBoxService messageBoxService, IEventAggregator eventAggregator, IoCProxy ioc) : base(messageBoxService, eventAggregator, ioc)
        {
            AddValidationRule(() => Email)
                .Condition(() => !EmailValidation.Check(Email))
                .Message("-");

            AddValidationRule(() => Password)
                .Condition(() => string.IsNullOrEmpty(Password))
                .Message("-");

#if DEBUG
            // Pro účely ladění a vývoje. Zadávat furt heslo je otravný.
            Email = "ics@zakladna.eu";
            Password = "icsprojekt123";
#endif
        }

        #endregion

        private string email;
        private string password;

        public string Email
        {
            get { return email; }
            set
            {
                Set(ref email, value);
                Validate();
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                Set(ref password, value);
                Validate();
            }
        }

        public bool CanLogin
        {
            get { return !HasError; }
        }

        public override void NotifyErrorChanged()
        {
            NotifyOfPropertyChange(() => CanLogin);
            base.NotifyErrorChanged();
        }

        public void Login()
        {
            var user = AppRepository.UsersRepository.FindUserByEmail(Email);
            string encodedPassword = Password; //TODO Encode password

            if(user == null)
            {
                MessageBoxService.ShowErrorMessageBox(new Exception("Invalid user"));
            }
            else if (encodedPassword != user.Password)
            {
                MessageBoxService.ShowMessageBox("Invalid password");
            }
            else
            {
                AppRepository.UsersRepository.UpdateLastLogin(user.ID, DateTime.Now);

                var model = IoC.Get<MainPageViewModel>();
                EventAggregator.PublishOnUIThread(new ShowViewModelMessage(model));
            }
        }

        public void Register()
        {
            var model = IoC.Get<RegisterPageViewModel>();
            EventAggregator.PublishOnUIThread(new ShowViewModelMessage(model));
        }
    }
}
