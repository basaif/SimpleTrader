using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly LoginViewModel _loginViewModel;
        private readonly IRenavigator _renavigator;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _authenticator = authenticator;
            _loginViewModel = loginViewModel;
            _renavigator = renavigator;
        }

        protected override async Task ExecuteAsync(object? parameter)
        {
            _loginViewModel.ErrorMessage = string.Empty;

            try
            {
                await _authenticator.Login(_loginViewModel.Username, _loginViewModel.Password);

                _renavigator.Renavigate();
            }
            catch (UserNotFoundException)
            {
                _loginViewModel.ErrorMessage = "Username does not exist.";
            }
            catch (InvalidPasswordException)
            {
                _loginViewModel.ErrorMessage = "Incorrect password.";
            }
            catch (Exception)
            {
                _loginViewModel.ErrorMessage = "Login failed.";
            }


        }
    }
}
