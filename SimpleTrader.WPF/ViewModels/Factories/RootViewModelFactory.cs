using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class RootViewModelFactory : IRootViewModelFactory
    {
        private readonly IViewModelFactory<HomeViewModel> _homeViewModelFactory;
        private readonly IViewModelFactory<PortfolioViewModel> _portfolioViewModelFactory;
        private readonly IViewModelFactory<LoginViewModel> _loginViewModelFactory;
        private readonly BuyViewModel _buyViewModel;

        public RootViewModelFactory(IViewModelFactory<HomeViewModel> homeViewModelFactory,
            IViewModelFactory<PortfolioViewModel> portfolioViewModelFactory,
            IViewModelFactory<LoginViewModel> loginViewModelFactory,
            BuyViewModel buyViewModel)
        {
            _homeViewModelFactory = homeViewModelFactory;
            _portfolioViewModelFactory = portfolioViewModelFactory;
            _loginViewModelFactory = loginViewModelFactory;
            _buyViewModel = buyViewModel;
        }
        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Home => _homeViewModelFactory.CreateViewModel(),
                ViewType.Portfolio => _portfolioViewModelFactory.CreateViewModel(),
                ViewType.Buy => _buyViewModel,
                ViewType.Login => _loginViewModelFactory.CreateViewModel(),
                _ => throw new ArgumentException("The View Type doesn't have a View Model", nameof(viewType)),
            };
        }
    }
}
