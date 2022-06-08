using Moq;
using NUnit.Framework;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Tests.Services.TransactionServices
{
    [TestFixture]
    public class BuyStockServiceTests
    {

        private IBuyStockService _buyStockService;

        private Mock<IStockPriceService> _mockStockPriceService = new();
        private Mock<IDataService<Account>> _mockAccountService = new();

        [SetUp]
        public void SetUp()
        {
            _mockStockPriceService = new Mock<IStockPriceService>();
            _mockAccountService = new Mock<IDataService<Account>>();
            _buyStockService = new BuyStockService(_mockStockPriceService.Object, _mockAccountService.Object);
        }

        [Test]
        public void BuyStock_WithInvalidSymbol_ThrowsInvalidSymbolExceptionForSymbol()
        {
            string expectedInvalidSymbol = "bad_symbol";
            _mockStockPriceService.Setup(s => s.GetStockPriceAsync(expectedInvalidSymbol)).ThrowsAsync(new InvalidSymbolException(expectedInvalidSymbol));

            if (Assert.ThrowsAsync<InvalidSymbolException>(
                        () => _buyStockService.BuyStock(It.IsAny<Account>(), expectedInvalidSymbol, It.IsAny<int>()))
                is InvalidSymbolException excpetion)
            {
                string actualInvalidSymbol = excpetion.Symbol;

                Assert.AreEqual(expectedInvalidSymbol, actualInvalidSymbol); 
            }
        }

        [Test]
        public void BuyStock_WithGetPriceFailure_ThrowsException()
        {
            _mockStockPriceService.Setup(s => s.GetStockPriceAsync(It.IsAny<string>())).ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(
                () => _buyStockService.BuyStock(It.IsAny<Account>(), It.IsAny<string>(), It.IsAny<int>()));
        }

        [Test]
        public void BuyStock_WithInsufficientFunds_ThrowsInsufficientFundsExceptionForBalances()
        {
            double expectedAccountBalance = 0;
            double expectedRequiredBalance = 100;
            Account buyer = CreateAccount(expectedAccountBalance);
            _mockStockPriceService.Setup(s => s.GetStockPriceAsync(It.IsAny<string>())).ReturnsAsync(expectedRequiredBalance);

            if (Assert.ThrowsAsync<InsufficientFundsException>(
                        () => _buyStockService.BuyStock(buyer, It.IsAny<string>(), 1)) is InsufficientFundsException exception)
            {
                double actualAccountBalance = exception.AccountBalance;
                double actualRequiredBalance = exception.RequiredBalance;

                Assert.AreEqual(expectedAccountBalance, actualAccountBalance);
                Assert.AreEqual(expectedRequiredBalance, actualRequiredBalance); 
            }
        }

        [Test]
        public void BuyStock_WithAccountUpdateFailure_ThrowsException()
        {
            Account buyer = CreateAccount(1000);
            _mockStockPriceService.Setup(s => s.GetStockPriceAsync(It.IsAny<string>())).ReturnsAsync(100);
            _mockAccountService.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<Account>())).Throws(new Exception());

            Assert.ThrowsAsync<Exception>(() => _buyStockService.BuyStock(buyer, It.IsAny<string>(), 1));
        }

        [Test]
        public async Task BuyStock_WithSuccessfulPurchase_ReturnsAccountWithNewTransaction()
        {
            int expectedTransactionCount = 1;
            Account buyer = CreateAccount(1000);
            _mockStockPriceService.Setup(s => s.GetStockPriceAsync(It.IsAny<string>())).ReturnsAsync(100);

            buyer = await _buyStockService.BuyStock(buyer, It.IsAny<string>(), 1);
            int actualTransactionCount = buyer.AssetTransactions.Count;

            Assert.AreEqual(expectedTransactionCount, actualTransactionCount);
        }

        [Test]
        public async Task BuyStock_WithSuccessfulPurchase_ReturnsAccountWithNewBalance()
        {
            double expectedBalance = 0;
            Account buyer = CreateAccount(100);
            _mockStockPriceService.Setup(s => s.GetStockPriceAsync(It.IsAny<string>())).ReturnsAsync(50);

            buyer = await _buyStockService.BuyStock(buyer, It.IsAny<string>(), 2);
            double actualBalance = buyer.Balance;

            Assert.AreEqual(expectedBalance, actualBalance);
        }

        private static Account CreateAccount(double balance)
        {
            return new Account()
            {
                Balance = balance,
                AssetTransactions = new List<AssetTransaction>()
            };
        }
    }
}
