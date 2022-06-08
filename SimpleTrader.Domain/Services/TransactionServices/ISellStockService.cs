using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public interface ISellStockService
    {
        /// <summary>
        /// Sell a stock for an account.
        /// </summary>
        /// <param name="seller">The account of the seller.</param>
        /// <param name="symbol">The symbol sold.</param>
        /// <param name="shares">The amount of shares to sell.</param>
        /// <returns>The updated account.</returns>
        /// <exception cref="InsufficientSharesException">Thrown if the seller has insufficient shares for the symbol.</exception>
        /// <exception cref="InvalidSymbolException">Thrown if purchased symbol is invalid</exception>
        /// <exception cref="Exception">Thrown if the transaction fails.</exception>
        Task<Account> SellStock(Account seller, string symbol, int shares);
    }
}
