using Caliburn.Micro;
using SimpleTeamApp.App.Services;
using SimpleTeamApp.App.Services.MessageBoxService;
using SimpleTeamApp.App.UWPHelper.Validation;
using SimpleTeamApp.Entity.DataAccess;
using System.Diagnostics.CodeAnalysis;

namespace SimpleTeamApp.App.ViewModels
{
    public abstract class ViewModelBase : ValidatingScreen
    {
        protected IMessageBoxService MessageBoxService { get; }
        protected AppRepository AppRepository { get; set; }
        protected IEventAggregator EventAggregator { get; set; }
        protected IoCProxy IoC { get; set; }

        [ExcludeFromCodeCoverage]
        protected ViewModelBase(IMessageBoxService messageBoxService = null, IEventAggregator eventAggregator = null, IoCProxy ioc = null)
            : this(messageBoxService, eventAggregator, new AppRepository(), ioc)
        {
        }

        protected ViewModelBase(IMessageBoxService messageBoxService, IEventAggregator eventAggregator, AppRepository appRepository, IoCProxy ioc)
        {
            MessageBoxService = messageBoxService;
            AppRepository = appRepository;
            EventAggregator = eventAggregator;
            IoC = ioc;
        }

        protected override void OnDeactivate(bool close)
        {
            AppRepository.Dispose();
            base.OnDeactivate(close);
        }
    }
}
