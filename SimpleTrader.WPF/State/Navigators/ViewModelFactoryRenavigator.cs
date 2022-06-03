using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.State.Navigators
{
    public class ViewModelFactoryRenavigator<TViewModel> : IRenavigator where TViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory<TViewModel> _viewModelFactory;

        public ViewModelFactoryRenavigator(INavigator navigator, IViewModelFactory<TViewModel> viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }
        public void Renavigate()
        {
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel();
        }
    }
}
