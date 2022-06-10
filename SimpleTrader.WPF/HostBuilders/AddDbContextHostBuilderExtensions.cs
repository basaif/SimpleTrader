using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString("sqlite");
                void configureDbContext(DbContextOptionsBuilder o) => o.UseSqlite(connectionString);

                services.AddDbContext<SimpleTraderDbContext>(configureDbContext);
                services.AddSingleton<SimpleTraderDbContextFactory>(new SimpleTraderDbContextFactory(configureDbContext));
            });

            return host;
        }
    }
}
