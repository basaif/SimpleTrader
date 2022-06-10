using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Serivces;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IDataService<Account>, AccountDataService>();
                services.AddSingleton<IAccountService, AccountDataService>();
                services.AddSingleton<IAuthenticationService, AuthenticationService>();
                services.AddSingleton<IStockPriceService, DummyStockPriceService>();
                services.AddSingleton<IBuyStockService, BuyStockService>();
                services.AddSingleton<ISellStockService, SellStockService>();
                services.AddSingleton<IMajorIndexService, MajorIndexService>();

                services.AddSingleton<IPasswordHasher, PasswordHasher>();
            });

            return host;
        }
    }
}
