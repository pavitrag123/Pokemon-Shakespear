using Pokemon_Shakespear.Business.Interfaces;
using Pokemon_Shakespear.Models.Domain;
using Pokemon_Shakespear.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.Business.Services
{
    public class PokemonTranslationService : IPokemonTranslationService
    {
        private readonly IPokemonApiService _pokemonApiService;
        private readonly IShakespeareApiService _shakespeareApiService;

        public PokemonTranslationService(IPokemonApiService pokemonApiService, IShakespeareApiService shakespeareApiService
        )
        {
            _pokemonApiService = pokemonApiService ?? throw new ArgumentNullException(nameof(pokemonApiService));
            _shakespeareApiService = shakespeareApiService ?? throw new ArgumentNullException(nameof(shakespeareApiService));
        }

        public async Task<PokemonViewModel> Translate(string pokemonName)
        {
            if (string.IsNullOrEmpty(pokemonName))
            {
                throw new ArgumentNullException("Value cannot be null or empty.", nameof(pokemonName));
            }

            Pokemon pokemon = await _pokemonApiService.GetByName(pokemonName);

            if (pokemon is null)
            {
                return default;
            }
            string translation = await _shakespeareApiService.GetTranslation(pokemon.Description);

            return new PokemonViewModel(pokemon.Name, translation);

        }
    }
}
