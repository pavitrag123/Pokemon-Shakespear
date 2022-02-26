using PokeApiNet;
using Pokemon_Shakespear.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.Business.Wrappers
{
    public class PokemonAPIClientWrapper : IPokemonApiClientWrapper
    {
        public virtual async Task<PokemonSpecies> GetResourceAsync(string pokemonName)
        {
            try
            {
                using var client = new PokeApiClient();
                return await client.GetResourceAsync<PokemonSpecies>(pokemonName);
            }
            catch (HttpRequestException)
            {
                return default;
            }
        }
    }
}
