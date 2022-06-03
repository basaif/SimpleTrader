using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly LoginViewModel _loginViewModel;

        public event EventHandler? CanExecuteChanged;

        public LoginCommand(IAuthenticator authenticator, LoginViewModel loginViewModel)
        {
            _authenticator = authenticator;
            _loginViewModel = loginViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            if (parameter is string password)
            {
                bool success = await _authenticator.Login(_loginViewModel.Username, password); 
            }
        }
    }
}
