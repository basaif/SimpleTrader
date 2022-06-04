using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        private readonly FinancialModelingPrepHttpClientFactory _httpClientFactory;

        public MajorIndexService(FinancialModelingPrepHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            using FinancialModelingPrepHttpClient client = _httpClientFactory.CreateHttpClient();
            
            string uri = $"majors-indexes/{GetUriSuffix(indexType)}";

            MajorIndex majorIndex = await client.GetAsync<MajorIndex>(uri);
            majorIndex.Type = indexType;

            return majorIndex;
        }

        private static string GetUriSuffix(MajorIndexType indexType)
        {
            return indexType switch
            {
                MajorIndexType.DowJones => ".DJI",
                MajorIndexType.Nasdaq => ".IXIC",
                MajorIndexType.SP500 => ".INX",
                _ => throw new Exception("Major index type doesn't have a suffix defined."),
            };
        }
    }
}