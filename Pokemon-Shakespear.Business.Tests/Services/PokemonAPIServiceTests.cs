using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using PokeApiNet;
using Pokemon_Shakespear.Business.Interfaces;
using Pokemon_Shakespear.Business.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.Business.Tests.Services
{
    public class PokemonAPIServiceTests
    {
        private Mock<IPokemonApiClientWrapper> _mockpokemonApiClientWrapper;
        private Mock<IMemoryCache> _mockMemoryCache;
        private PokemonSpecies _pokemonSpecies;
        private Models.Domain.Pokemon _pokemon;
        private IConfigurationRoot configuration;


        [SetUp]
        public void SetUp()
        {
            _mockpokemonApiClientWrapper = new Mock<IPokemonApiClientWrapper>();
            _mockMemoryCache = new Mock<IMemoryCache>();
            _pokemonSpecies = SetupData.GetData<PokemonSpecies>("../../../Inputs/PokemonAPIData.json");
            _pokemon = SetupData.GetData<Models.Domain.Pokemon>("../../../Inputs/PokemonData.json");

            var myConfiguration = new Dictionary<string, string>
            {
                {"AbsoluteExpirationInHrs", "1"},
                {"SlidingExpirationInMinutes", "15"}
            };

            configuration = new ConfigurationBuilder()
               .AddInMemoryCollection(myConfiguration)
               .Build();
        }

        [Test]
        public async Task GivenValidPokemonName_ReturnDetailsFromAPIClient()
        {
            //Arrange
            SetupMockPokeApiClientWrapper(_pokemonSpecies);
            SetupMockMemoryCache(null);

            var service = new PokemonApiService(_mockpokemonApiClientWrapper.Object, _mockMemoryCache.Object, configuration);

            //Act
            Models.Domain.Pokemon actualResult = await service.GetByName("charizard");

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(actualResult.Description, _pokemon.Description);
        }

        [Test]
        public async Task GivenValidPokemonNameCasing_ReturnDetailsFromAPIClient()
        {
            //Arrange
            SetupMockPokeApiClientWrapper(_pokemonSpecies);
            SetupMockMemoryCache(null);

            var service = new PokemonApiService(_mockpokemonApiClientWrapper.Object, _mockMemoryCache.Object, configuration);

            //Act
            Models.Domain.Pokemon actualResult = await service.GetByName("CHariZArd");

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(actualResult.Description, _pokemon.Description);
        }

        [Test]
        public void GivenEmptyPokemonName_ReturnArgumentNullException()
        {
            var service = new PokemonApiService(_mockpokemonApiClientWrapper.Object, _mockMemoryCache.Object, configuration);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.GetByName(String.Empty));
        }

        [Test]
        public async Task GivenInValidPokemonName_ReturnNullData()
        {
            SetupMockMemoryCache(null);
            var service = new PokemonApiService(_mockpokemonApiClientWrapper.Object, _mockMemoryCache.Object, configuration);

            //Act
            Models.Domain.Pokemon actualResult = await service.GetByName("test");

            //Assert
            Assert.IsNull(actualResult);
        }


        private void SetupMockMemoryCache(PokemonSpecies pokemonSpecies)
        {
            _mockMemoryCache.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(new Mock<ICacheEntry>().Object);
        }

        private void SetupMockPokeApiClientWrapper(PokemonSpecies pokemonSpecies = default)
        {
            _mockpokemonApiClientWrapper
                .Setup(wrapper => wrapper.GetResourceAsync(It.IsAny<string>()))
                .ReturnsAsync(() => pokemonSpecies);
        }

    }
}
