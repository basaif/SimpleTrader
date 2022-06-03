using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<PortfolioViewModel> _createPortfolioViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<BuyViewModel> _createBuyViewModel;

        public ViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel,
                                CreateViewModel<PortfolioViewModel> createPortfolioViewModel,
                                CreateViewModel<LoginViewModel> createLoginViewModel,
                                CreateViewModel<BuyViewModel> createBuyViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createPortfolioViewModel = createPortfolioViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createBuyViewModel = createBuyViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Home => _createHomeViewModel(),
                ViewType.Portfolio => _createPortfolioViewModel(),
                ViewType.Buy => _createBuyViewModel(),
                ViewType.Login => _createLoginViewModel(),
                _ => throw new ArgumentException("The View Type doesn't have a View Model", nameof(viewType)),
            };
        }
    }
}
