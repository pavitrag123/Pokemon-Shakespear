using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using PokeApiNet;
using Pokemon_Shakespear.Business.Interfaces;
using Pokemon_Shakespear.Business.Services;
using Pokemon_Shakespear.Business.Wrappers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pokemon_Shakespear.Models.ViewModel;

namespace Pokemon_Shakespear.Business.Tests.Services
{
    public class ShakespeareAPIServiceTests
    {
        private readonly IServiceCollection _services;
        private IShakespeareApiService _shakespeareApiService;
        private IConfigurationRoot configuration;

        public ShakespeareAPIServiceTests()
        {
            _services = new ServiceCollection();


        }

        [SetUp]
        public void SetUp()
        {
            var configurationValues = new Dictionary<string, string>
            {
                {"ShakespeareApi", "https://api.funtranslations.com/translate/shakespeare"}
            };

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configurationValues)
                .Build();

            _services
              .AddHttpClient()
              .AddSingleton<IHttpClientWrapper, HttpClientWrapper>()
              .AddSingleton<IShakespeareApiService, ShakespeareApiService>()
              .AddSingleton<IConfiguration>(configuration);

            var provider = _services.BuildServiceProvider();
            _shakespeareApiService = provider.GetService<IShakespeareApiService>();
        }

        [Test]
        public async Task GivenValidPokemonName_ReturnDetailsFromAPIClient()
        {
            var actualResult = await _shakespeareApiService.GetTranslation("Charizard flies around the sky in search of powerful opponents.\nIt breathes fire of such great heat that it melts anything.\nHowever, it never turns its fiery breath on any opponent\nweaker than itself.");
            var expectedResult = SetupData.GetData<PokemonViewModel>("../../../Inputs/PokemonViewModel.json");

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(actualResult, expectedResult.TranslatedDescription);
        }



    }
}
