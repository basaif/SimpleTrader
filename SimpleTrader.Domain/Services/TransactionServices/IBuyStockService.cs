﻿using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public interface IBuyStockService
    {
        /// <summary>
        /// Purchase a stock for an account.
        /// </summary>
        /// <param name="buyer">The account of the buyer.</param>
        /// <param name="symbol">The symbol purchsed.</param>
        /// <param name="shares">The amount of shares.</param>
        /// <returns>The updated account.</returns>
        /// <exception cref="InsufficientFundsException">Thrown if the account has an insufficient balance.</exception>
        /// <exception cref="InvalidSymbolException">Thrown if purchased symbol is invalid</exception>
        /// <exception cref="Exception">Thrown if the transaction fails.</exception>
        Task<Account> BuyStock(Account buyer, string symbol, int shares);
    }
}
