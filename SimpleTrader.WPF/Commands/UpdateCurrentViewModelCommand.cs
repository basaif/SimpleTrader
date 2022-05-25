using SimpleTrader.FinancialModelingPrepAPI;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        private readonly INavigator _navigator;

        public event EventHandler? CanExecuteChanged;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType viewType)
            {
                switch (viewType)
                {
                    case ViewType.Home:
                        _navigator.CurrentViewModel = new HomeViewModel(MajorIndexListingViewModel.LoadMajorIndexViewModel(new MajorIndexService()));
                        break;
                    case ViewType.Portfolio:
                        _navigator.CurrentViewModel = new PortfolioViewModel();
                        break;
                }
            }
        }
    }
}