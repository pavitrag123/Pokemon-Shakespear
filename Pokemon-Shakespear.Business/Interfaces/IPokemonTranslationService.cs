using Pokemon_Shakespear.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.Business.Interfaces
{
    public interface IPokemonTranslationService
    {
        Task<PokemonViewModel> Translate(string pokemonName);
    }
}
