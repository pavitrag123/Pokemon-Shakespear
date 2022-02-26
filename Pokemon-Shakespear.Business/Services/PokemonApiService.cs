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
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions memoryCacheEntryOptions;

        public PokemonApiService(IPokemonApiClientWrapper pokemonApiClientWrapper
            , IMemoryCache memoryCache, IConfiguration configuration)
        {
            _pokemonApiClientWrapper = pokemonApiClientWrapper ?? throw new ArgumentNullException(nameof(pokemonApiClientWrapper));
            _memoryCache = memoryCache;
            memoryCacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(Convert.ToInt16(configuration["AbsoluteExpirationInHrs"])),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(Convert.ToInt16(configuration["SlidingExpirationInMinutes"]))
            };
        }

        public async Task<Models.Domain.Pokemon> GetByName(string pokemonName)
        {
            if (string.IsNullOrEmpty(pokemonName))
            {
                throw new ArgumentNullException("Value cannot be null or empty.", nameof(pokemonName));
            }

            string cacheKey = pokemonName.ToLower();
            if (!_memoryCache.TryGetValue(cacheKey, out PokemonSpecies pokemon))
            {
                pokemon = await _pokemonApiClientWrapper.GetResourceAsync(cacheKey);
                _memoryCache.Set(cacheKey, pokemon, memoryCacheEntryOptions);
            }

            return pokemon is null ? default : GetPokemonDetails(pokemon);
        }



        private static Models.Domain.Pokemon GetPokemonDetails(PokemonSpecies pokemonSpecies)
        {
            var parsedDescription = GetDescription(pokemonSpecies);

            return new Models.Domain.Pokemon(pokemonSpecies.Name, parsedDescription);
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
