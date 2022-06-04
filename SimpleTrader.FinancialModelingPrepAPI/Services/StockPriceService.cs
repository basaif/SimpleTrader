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
        private readonly FinancialModelingPrepHttpClientFactory _httpClientFactory;

        public StockPriceService(FinancialModelingPrepHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<double> GetStockPriceAsync(string symbol)
        {
            using FinancialModelingPrepHttpClient client = _httpClientFactory.CreateHttpClient();

            string uri = $"stock/real-time-price/{symbol}";

            StockPriceResult stockPrice = await client.GetAsync<StockPriceResult>(uri);

            if (stockPrice.Price == 0)
            {
                throw new InvalidSymbolException(symbol);
            }

            return stockPrice.Price;
        }
    }
}
