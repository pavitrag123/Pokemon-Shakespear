using Pokemon_Shakespear.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.Business.Interfaces
{
    public interface IPokemonApiService
    {
        Task<Pokemon> GetByName(string pokemonName);
    }
}
