using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Pokemon_Shakespear.API.Controllers;
using Pokemon_Shakespear.Business.Interfaces;
using Pokemon_Shakespear.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.API.Tests.Controllers
{
    [TestFixture]
    public class PokemonTranslatorControllerTests
    {
        private Mock<IPokemonTranslationService> _mockPokemonTranslationService;

        [SetUp]
        public void SetUp()
        {
            _mockPokemonTranslationService = new Mock<IPokemonTranslationService>();
        }

        [Test]
        public async Task GivenValidPokemonName_ReturnTransaltedDescription()
        {
            string pokemonName = "charizard";
            //Arrange
            SetupMockPokemonTranslationService(ExpectedViewModel());
            var controller = new PokemonTranslatorController(_mockPokemonTranslationService.Object);

            // Act
            ActionResult<PokemonViewModel> actionResult = await controller.Get(pokemonName);


            // Assert           
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(ExpectedViewModel().TranslatedDescription, actionResult.Value.TranslatedDescription);

        }

        [Test]
        public async Task GivenValidPokemonNameCasing_ReturnTransaltedDescription()
        {
            string pokemonName = "CHARIZARD";
            //Arrange
            SetupMockPokemonTranslationService(ExpectedViewModel());
            var controller = new PokemonTranslatorController(_mockPokemonTranslationService.Object);

            // Act
            ActionResult<PokemonViewModel> actionResult = await controller.Get(pokemonName);


            // Assert           
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(ExpectedViewModel().TranslatedDescription, actionResult.Value.TranslatedDescription);

        }

        [Test]
        public async Task GivenEmptyPokemonName_ReturnBadRequestResult()
        {
            //Arrange
            SetupMockPokemonTranslationService(ExpectedViewModel());
            var controller = new PokemonTranslatorController(_mockPokemonTranslationService.Object);

            // Act
            var actionResult = await controller.Get(string.Empty);

            // Assert
            Assert.AreEqual(400, ((ObjectResult)actionResult.Result).StatusCode);
        }


        [Test]
        public async Task GivenInValidPokemonName_ReturnNotFoundResponse()
        {
            //Arrange
            SetupMockPokemonTranslationService(default);
            var controller = new PokemonTranslatorController(_mockPokemonTranslationService.Object);

            // Act
            var actionResult = await controller.Get("test");

            // Assert
            Assert.AreEqual(404, ((StatusCodeResult)actionResult.Result).StatusCode);          
        }


        private void SetupMockPokemonTranslationService(PokemonViewModel model)
        {
            _mockPokemonTranslationService
                .Setup(service => service.Translate(It.IsAny<string>()))
                .ReturnsAsync(() => model);
        }

        private PokemonViewModel ExpectedViewModel()
        {
            return new PokemonViewModel("charizard", "Charizard flies 'round the sky in search of powerful opponents. 't breathes fire of such most wondrous heat yond 't melts aught. However, 't nev'r turns its fiery breath on any opponent weaker than itself.",6);
        }
    }
}
