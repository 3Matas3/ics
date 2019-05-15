using Caliburn.Micro;
using SimpleTeamApp.App.Services;
using SimpleTeamApp.App.Services.Events;
using SimpleTeamApp.App.Services.MessageBoxService;
using SimpleTeamApp.App.UWPHelper.Validation;
using SimpleTeamApp.Entity.DataAccess.TransferModels;

namespace SimpleTeamApp.App.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private string email;
        private string password;
        private string repeatPassword;
        private string name;
        private string surname;

        public RegisterPageViewModel(IMessageBoxService messageBoxService, IEventAggregator eventAggregator, IoCProxy ioc)
            : base(messageBoxService, eventAggregator, ioc)
        {
            AddValidationRule(() => Email).Condition(() => !EmailValidation.Check(Email)).Message("-");
            AddValidationRule(() => Password).Condition(() => string.IsNullOrEmpty(Password)).Message("-");
            AddValidationRule(() => RepeatPassword).Condition(() => string.IsNullOrEmpty(RepeatPassword)).Message("-");
            AddValidationRule(() => FirstName).Condition(() => string.IsNullOrEmpty(FirstName)).Message("-");
            AddValidationRule(() => Surname).Condition(() => string.IsNullOrEmpty(Surname)).Message("-");
        }

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

        public string RepeatPassword
        {
            get { return repeatPassword; }
            set
            {
                Set(ref repeatPassword, value);
                Validate();
            }
        }

        public string FirstName
        {
            get { return name; }
            set
            {
                Set(ref name, value);
                Validate();
            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                Set(ref surname, value);
                Validate();
            }
        }

        public bool CanRegister
        {
            get { return !HasError; }
        }

        public override void NotifyErrorChanged()
        {
            base.NotifyErrorChanged();
            NotifyOfPropertyChange(() => CanRegister);
        }

        public void Register()
        {
            if(Password != RepeatPassword)
            {
                MessageBoxService.ShowMessageBox("Zadaná hesla nesouhlasí", "Chyba validace");
                return;
            }

            string encryptedPassword = Password;

            var newUser = new User()
            {
                Email = Email,
                Name = FirstName,
                Password = encryptedPassword,
                Surname = surname
            };

            AppRepository.UsersRepository.CreateUser(newUser);

            MessageBoxService.ShowMessageBox("Registrace proběhla v pořádku.", "Děkujeme");

            GoBack();
        }

        public void GoBack()
        {
            var model = IoC.Get<LoginPageViewModel>();
            EventAggregator.PublishOnUIThread(new ShowViewModelMessage(model));
        }
    }
}
