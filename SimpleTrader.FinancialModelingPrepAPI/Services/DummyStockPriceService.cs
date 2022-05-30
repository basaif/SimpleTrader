using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class DummyStockPriceService : IStockPriceService
    {
        public async Task<double> GetStockPriceAsync(string symbol)
        {
            Dictionary<string, double> stockPrices = await GetStockPriceDictionaryAsync();

            if (stockPrices.TryGetValue(symbol, out double price))
            {
                return price;
            }
            else
            {
                throw new InvalidSymbolException(symbol);
            }

        }

        private async Task<Dictionary<string, double>> GetStockPriceDictionaryAsync()
        {
            Dictionary<string, double> stockPrices = new();
            await Task.Run(() =>
            {
                
                stockPrices.Add("AAPL", 59.0);
                stockPrices.Add("GOOG", 120.0);
                stockPrices.Add("YHOO", 180.0);
                stockPrices.Add("Dow Jones", 350.0);
                stockPrices.Add("VZ", 102.0);
                stockPrices.Add("T", 30.0);
            });
            return stockPrices;
        }
    }
}
