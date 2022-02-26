using Microsoft.AspNetCore.Mvc;
using Pokemon_Shakespear.Business.Interfaces;
using Pokemon_Shakespear.Models.ViewModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.API.Controllers
{
    public class PokemonTranslatorController : Controller
    {
        private readonly IPokemonTranslationService _pokemonTranslationService;

        public PokemonTranslatorController(IPokemonTranslationService pokemonTranslationService)
        {
            _pokemonTranslationService = pokemonTranslationService ?? throw new ArgumentNullException(nameof(pokemonTranslationService));
        }

        [HttpGet]
        [Route("pokemon/{pokemonName}")]
        public async Task<ActionResult<PokemonViewModel>> Get([FromRoute] string pokemonName)
        {
            if (string.IsNullOrEmpty(pokemonName))
            {
                return BadRequest("Value cannot be null or empty.");
            }
            var model = await _pokemonTranslationService.Translate(pokemonName);

            if (model == null)
                return NotFound();

            return model;
        }
    }
}
