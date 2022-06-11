using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _registerRenavigator;

        public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator,
            IRenavigator registerRenavigator)
        {
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _registerRenavigator = registerRenavigator;

            _registerViewModel.PropertyChanged += RegisterViewModel_PropertyChanged;
        }
        public override bool CanExecute(object? parameter)
        {
            return _registerViewModel.CanRegister && base.CanExecute(parameter);
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            _registerViewModel.ErrorMessage = string.Empty;
            try
            {
                RegistrationResult result = await _authenticator.Register(
                        _registerViewModel.Email,
                        _registerViewModel.Username,
                        _registerViewModel.Password,
                        _registerViewModel.ConfirmPassword);

                switch (result)
                {
                    case RegistrationResult.EmailAlreadyExists:
                        _registerViewModel.ErrorMessage = "An account with this email already exists.";
                        break;
                    case RegistrationResult.UsernameAlreadyExists:
                        _registerViewModel.ErrorMessage = "An account with this username already exists.";
                        break;
                    case RegistrationResult.PasswordsDoNotMatch:
                        _registerViewModel.ErrorMessage = "Passoword does not match confirm password.";
                        break;
                    case RegistrationResult.Success:
                        _registerRenavigator.Renavigate();
                        break;
                    default:
                        _registerViewModel.ErrorMessage = "Registration failed.";
                        break;
                }
            }
            catch (Exception)
            {
                _registerViewModel.ErrorMessage = "Registration failed.";
            }
        }
        private void RegisterViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RegisterViewModel.CanRegister))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
