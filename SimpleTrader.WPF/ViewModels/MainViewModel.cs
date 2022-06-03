using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }
        public IAuthenticator Authenticator { get; }

        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator,IViewModelFactory rootViewModelFactory, IAuthenticator authenticator)
        {
            Navigator = navigator;
            Authenticator = authenticator;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, rootViewModelFactory);

            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
