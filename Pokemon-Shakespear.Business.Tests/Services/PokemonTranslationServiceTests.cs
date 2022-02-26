﻿using Moq;
using NUnit.Framework;
using Pokemon_Shakespear.Business.Interfaces;
using Pokemon_Shakespear.Business.Services;
using Pokemon_Shakespear.Models.Domain;
using Pokemon_Shakespear.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.Business.Tests.Services
{
    public class PokemonTranslationServiceTests
    {
        private Mock<IPokemonApiService> _mockpokemonApiService;
        private Mock<IShakespeareApiService> _mockShakespeareApiService;
        private Pokemon _pokemon;
        private PokemonViewModel _pokemonViewModel;

        [SetUp]
        public void SetUp()
        {
            _mockpokemonApiService = new Mock<IPokemonApiService>();
            _mockShakespeareApiService = new Mock<IShakespeareApiService>();
            _pokemon = SetupData.GetData<Pokemon>("../../../Inputs/PokemonData.json");
            _pokemonViewModel = SetupData.GetData<PokemonViewModel>("../../../Inputs/PokemonViewModel.json");
        }

        [Test]
        public async Task GivenValidPokemonName_ReturnTranslatedDescription()
        {
            SetupMockPokemonApiService(_pokemon);
            SetupMockShakespeareApiService(_pokemonViewModel);

            var service = new PokemonTranslationService(_mockpokemonApiService.Object, _mockShakespeareApiService.Object);

            PokemonViewModel actualResult = await service.Translate("charizard");
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(actualResult.TranslatedDescription, _pokemonViewModel.TranslatedDescription);
        }

        [Test]
        public async Task GivenValidPokemonNameCasing_ReturnTranslatedDescription()
        {
            //Arrange
            SetupMockPokemonApiService(_pokemon);
            SetupMockShakespeareApiService(_pokemonViewModel);

            var service = new PokemonTranslationService(_mockpokemonApiService.Object, _mockShakespeareApiService.Object);
            //Act
            PokemonViewModel actualResult = await service.Translate("CHariZard");

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(actualResult.TranslatedDescription, _pokemonViewModel.TranslatedDescription);
        }



        [Test]
        public void GivenEmptyPokemonName_ReturnNullException()
        {
            var service = new PokemonTranslationService(_mockpokemonApiService.Object, _mockShakespeareApiService.Object);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.Translate(String.Empty));
        }

        [Test]
        public async Task GivenInvalidPokemonName_ReturnNullData()
        {           
            var service = new PokemonTranslationService(_mockpokemonApiService.Object, _mockShakespeareApiService.Object);

            PokemonViewModel actualResult = await service.Translate("test");
            Assert.IsNull(actualResult);
        }


        private void SetupMockPokemonApiService(Pokemon expectedPokemon)
        {
            _mockpokemonApiService
                .Setup(service => service.GetByName(It.IsAny<string>()))
                .ReturnsAsync(() => expectedPokemon);
        }

        private void SetupMockShakespeareApiService(PokemonViewModel expectedPokemon)
        {
            _mockShakespeareApiService
                .Setup(service => service.GetTranslation(It.IsAny<string>()))
                .ReturnsAsync(() => expectedPokemon.TranslatedDescription);
        }
    }
}
