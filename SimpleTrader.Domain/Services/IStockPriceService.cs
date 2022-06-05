﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Services
{
    public interface IStockPriceService
    {
        /// <summary>
        /// Get the share price for a symbol.
        /// </summary>
        /// <param name="symbol">The symbol to get the price of.</param>
        /// <returns>The price of symbol.</returns>
        /// <exception cref="InvalidSymbolException">Thrown if symbol does not exist.</exception>
        /// <exception cref="Exception">Thrown if getting the symbol fails.</exception>
        Task<double> GetStockPriceAsync(string symbol);
    }
}
