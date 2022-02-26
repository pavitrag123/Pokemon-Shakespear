using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.Business.Interfaces
{
    public interface IPokemonApiClientWrapper
    {
        Task<PokemonSpecies> GetResourceAsync(string pokemonName);
    }
}
