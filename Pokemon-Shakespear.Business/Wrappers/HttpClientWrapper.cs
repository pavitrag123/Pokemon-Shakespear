using Pokemon_Shakespear.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.Business.Wrappers
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientWrapper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public virtual async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            using var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync(requestUri, content);

            return response.EnsureSuccessStatusCode();
        }
    }
}
