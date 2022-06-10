using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels
{
    public interface ISearchSymbolViewModel : INotifyPropertyChanged
    {
        string ErrorMessage { set; }
        string SearchResultSymbol { set; }
        double StockPrice { set; }
        string Symbol { get; }
        bool CanSearchSymbol { get; }
    }
}
