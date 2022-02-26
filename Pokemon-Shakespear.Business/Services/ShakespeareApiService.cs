using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pokemon_Shakespear.Business.Interfaces;
using Pokemon_Shakespear.Models.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.Business.Services
{
    public class ShakespeareApiService : IShakespeareApiService
    {
        private readonly string _apiUrl;

        private readonly IHttpClientWrapper _httpClientWrapper;

        public ShakespeareApiService(IHttpClientWrapper httpClientWrapper, IConfiguration configuration)
        {
            _httpClientWrapper = httpClientWrapper ?? throw new ArgumentNullException(nameof(httpClientWrapper));
            _apiUrl = configuration["ShakespeareApi"];
            
        }

        public async Task<string> GetTranslation(string textToTranslate)
        {
            if (string.IsNullOrEmpty(textToTranslate))
            {
                throw new ArgumentNullException("Value cannot be null or empty", nameof(textToTranslate));
            }

            var payload = GeneratePostPayload(textToTranslate);
            var response = await _httpClientWrapper.PostAsync(_apiUrl, payload);
            var responseJson = await response.Content.ReadAsStringAsync();

            var translationResponse = JsonConvert.DeserializeObject<ShakespeareApiResponse>(responseJson);

            return translationResponse?.Contents is null ? default : translationResponse.Contents.Translated;
        }

        private static FormUrlEncodedContent GeneratePostPayload(string textToTranslate)
        {
            var parametersDictionary = new Dictionary<string, string>
            {
                {"text", textToTranslate}
            };
            return new FormUrlEncodedContent(parametersDictionary);
        }


    }
}
