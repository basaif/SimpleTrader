using SimpleTrader.FinancialModelingPrepAPI.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepHttpClient : HttpClient
    {
        public FinancialModelingPrepHttpClient()
        {
            BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await GetAsync(uri);

            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }

        public static string GetApiKey()
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + @"Sensitive/sensitive.txt";
            string apiKey = File.ReadAllText(file);
            return apiKey;
        } 
    }
}
