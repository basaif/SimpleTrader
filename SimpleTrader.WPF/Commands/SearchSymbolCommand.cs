using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class SearchSymbolCommand : AsyncCommandBase
    {
        private readonly BuyViewModel _buyViewModel;
        private readonly IStockPriceService _stockPriceService;

        public SearchSymbolCommand(BuyViewModel buyViewModel, IStockPriceService stockPriceService)
        {
            _buyViewModel = buyViewModel;
            _stockPriceService = stockPriceService;
        }
        protected override async Task ExecuteAsync(object? parameter)
        {
            _buyViewModel.ErrorMessage = string.Empty;
            try
            {
                double stockPrice = await _stockPriceService.GetStockPriceAsync(_buyViewModel.Symbol);
                _buyViewModel.StockPrice = stockPrice;
                _buyViewModel.SearchResultSymbol = _buyViewModel.Symbol.ToUpper();
            }
            catch (InvalidSymbolException)
            {
                _buyViewModel.ErrorMessage = "Symbol does not exist.";
            }
            catch (Exception)
            {
                _buyViewModel.ErrorMessage = "Failed to get symbol information.";
            }
        }
    }
}
