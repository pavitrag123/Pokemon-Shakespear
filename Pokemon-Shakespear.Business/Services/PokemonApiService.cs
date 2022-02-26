using Microsoft.Extensions.Caching.Memory;
using PokeApiNet;
using Pokemon_Shakespear.Business.Interfaces;
using Pokemon_Shakespear.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Pokemon_Shakespear.Business.Services
{
    public class PokemonApiService : IPokemonApiService
    {
        private readonly IPokemonApiClientWrapper _pokemonApiClientWrapper;

        public PokemonApiService(IPokemonApiClientWrapper pokemonApiClientWrapper)
        {
            _pokemonApiClientWrapper = pokemonApiClientWrapper ?? throw new ArgumentNullException(nameof(pokemonApiClientWrapper));
          
        }

        public async Task<Models.Domain.Pokemon> GetByName(string pokemonName)
        {
            if (string.IsNullOrEmpty(pokemonName))
            {
                throw new ArgumentNullException("Value cannot be null or empty.", nameof(pokemonName));
            }
            PokemonSpecies pokemon = await _pokemonApiClientWrapper.GetResourceAsync(pokemonName);            

            return pokemon is null ? default : GetPokemonDetails(pokemon);
        }



        private static Models.Domain.Pokemon GetPokemonDetails(PokemonSpecies pokemonSpecies)
        {
            var parsedDescription = GetDescription(pokemonSpecies);

            return new Models.Domain.Pokemon(pokemonSpecies.Name, parsedDescription, pokemonSpecies.Id);
        }

        private static string GetDescription(PokemonSpecies pokemonSpecies)
        {
            return pokemonSpecies.FlavorTextEntries
                .Where(flavorTexts => flavorTexts.Language.Name == "en")
                .Select(flavorTexts => flavorTexts.FlavorText)
                .FirstOrDefault();
        }
    }
}
