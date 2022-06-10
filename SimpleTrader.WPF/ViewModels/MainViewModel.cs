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
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator,IViewModelFactory rootViewModelFactory, IAuthenticator authenticator)
        {
            _navigator = navigator;
            _authenticator = authenticator;

            _navigator.StateChanged += OnCurrentViewModelChanged;
            _authenticator.StateChanged += OnUserLoggedIn;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator, rootViewModelFactory);

            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }

        private void OnUserLoggedIn()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public override void Dispose()
        {
            _navigator.StateChanged -= OnCurrentViewModelChanged;
            _authenticator.StateChanged -= OnUserLoggedIn;

            base.Dispose();
        }
    }
}
