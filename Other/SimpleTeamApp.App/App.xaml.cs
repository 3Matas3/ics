using Caliburn.Micro;
using SimpleTeamApp.App.Services;
using SimpleTeamApp.App.Services.MessageBoxService;
using SimpleTeamApp.App.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SimpleTeamApp.App
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App
    {
        private WinRTContainer Container { get; set; }

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            Container = new WinRTContainer();
            Container.RegisterWinRTServices();

            // Services
            Container
                .Singleton<IoCProxy>()
                .PerRequest<IMessageBoxService, MessageBoxService>();

            // ViewModels
            Container
                .PerRequest<MainViewModel>()
                .PerRequest<LoginPageViewModel>()
                .PerRequest<MainPageViewModel>()
                .PerRequest<RegisterPageViewModel>()
                .PerRequest<TeamSettingsViewModel>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            Container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            DisplayRootViewFor<MainViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return Container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            Container.BuildUp(instance);
        }

        protected override void OnUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            IoC.Get<IMessageBoxService>().ShowErrorMessageBox(e.Exception);
        }
    }
}
