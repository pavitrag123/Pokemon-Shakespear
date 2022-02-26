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
        private PokemonSpecies _pokemonSpecies;
        private Models.Domain.Pokemon _pokemon;


        [SetUp]
        public void SetUp()
        {
            _mockpokemonApiClientWrapper = new Mock<IPokemonApiClientWrapper>();
            _pokemonSpecies = SetupData.GetData<PokemonSpecies>("../../../Inputs/PokemonAPIData.json");
            _pokemon = SetupData.GetData<Models.Domain.Pokemon>("../../../Inputs/PokemonData.json");
          
        }

        [Test]
        public async Task GivenValidPokemonName_ReturnDetailsFromAPIClient()
        {
            //Arrange
            SetupMockPokeApiClientWrapper(_pokemonSpecies);
          
            var service = new PokemonApiService(_mockpokemonApiClientWrapper.Object);

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

            var service = new PokemonApiService(_mockpokemonApiClientWrapper.Object);

            //Act
            Models.Domain.Pokemon actualResult = await service.GetByName("CHariZArd");

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(actualResult.Description, _pokemon.Description);
        }

        [Test]
        public void GivenEmptyPokemonName_ReturnArgumentNullException()
        {
            var service = new PokemonApiService(_mockpokemonApiClientWrapper.Object);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.GetByName(String.Empty));
        }

        [Test]
        public async Task GivenInValidPokemonName_ReturnNullData()
        {
            var service = new PokemonApiService(_mockpokemonApiClientWrapper.Object);

            //Act
            Models.Domain.Pokemon actualResult = await service.GetByName("test");

            //Assert
            Assert.IsNull(actualResult);
        }


        private void SetupMockPokeApiClientWrapper(PokemonSpecies pokemonSpecies = default)
        {
            _mockpokemonApiClientWrapper
                .Setup(wrapper => wrapper.GetResourceAsync(It.IsAny<string>()))
                .ReturnsAsync(() => pokemonSpecies);
        }

    }
}
