using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class MajorIndexService : IMajorIndexService
    {
        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            using HttpClient client = new();
            string file = AppDomain.CurrentDomain.BaseDirectory + @"Sensitive/sensitive.txt";
            string apiKey = File.ReadAllText(file);

            string uri = $"https://financialmodelingprep.com/api/v3/majors-indexes/{GetUriSuffix(indexType)}?apikey={apiKey}";

            HttpResponseMessage response = await client.GetAsync(uri);

            MajorIndex majorIndex = await response.Content.ReadAsAsync<MajorIndex>();
            majorIndex.Type = indexType;
            return majorIndex;
        }

        private static string GetUriSuffix(MajorIndexType indexType)
        {
            return indexType switch
            {
                MajorIndexType.DowJones => ".DIJ",
                MajorIndexType.Nasdaq => ".IXIC",
                MajorIndexType.SP500 => ".INX",
                _ => ".DIJ",
            };
        }
    }
}