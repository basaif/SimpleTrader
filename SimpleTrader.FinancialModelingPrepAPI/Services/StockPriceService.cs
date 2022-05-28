using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        public async Task<double> GetStockPriceAsync(string symbol)
        {
            using FinancialModelingPrepHttpClient client = new();

            string uri = $"stock/real-time-price/{symbol}?apikey={FinancialModelingPrepHttpClient.GetApiKey()}";

            StockPriceResult stockPrice = await client.GetAsync<StockPriceResult>(uri);

            if (stockPrice.Price == 0)
            {
                throw new InvalidSymbolException(symbol);
            }

            return stockPrice.Price;
        }
    }
}
