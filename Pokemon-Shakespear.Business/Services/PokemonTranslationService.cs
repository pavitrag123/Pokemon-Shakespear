using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
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
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions memoryCacheEntryOptions;

        public PokemonTranslationService(IPokemonApiService pokemonApiService, IShakespeareApiService shakespeareApiService
            , IMemoryCache memoryCache, AppSettings appSettings)
        {
            _pokemonApiService = pokemonApiService ?? throw new ArgumentNullException(nameof(pokemonApiService));
            _shakespeareApiService = shakespeareApiService ?? throw new ArgumentNullException(nameof(shakespeareApiService));
            _memoryCache = memoryCache;
            memoryCacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(appSettings.AbsoluteExpirationInHrs),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(appSettings.SlidingExpirationInMinutes)
            };
        }

        public async Task<PokemonViewModel> Translate(string pokemonName)
        {
            if (string.IsNullOrEmpty(pokemonName))
            {
                throw new ArgumentNullException("Value cannot be null or empty.", nameof(pokemonName));
            }
            try
            {
                string cacheKey = pokemonName.ToLower();
                if (!_memoryCache.TryGetValue(cacheKey, out PokemonViewModel result))
                {
                    Pokemon pokemon = await _pokemonApiService.GetByName(pokemonName);

                    if (pokemon is null)
                    {
                        return default;
                    }

                    string translation = await _shakespeareApiService.GetTranslation(pokemon.Description);
                    result = new PokemonViewModel(pokemon.Name, translation, pokemon.Id);

                    _memoryCache.Set(cacheKey, result, memoryCacheEntryOptions);
                }
                return result;              
            }
            catch (Exception e)
            {
                throw new ArgumentNullException($"Error while translating {pokemonName} - {e.Message}", nameof(pokemonName));
            }

        }
    }
}
