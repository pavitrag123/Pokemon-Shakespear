using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon_Shakespear.Models.ViewModel
{
    public class PokemonViewModel
    {
        public string Name { get; set; }

        public string TranslatedDescription { get; set; }

        public PokemonViewModel()
        {

        }
        public PokemonViewModel(string name, string translatedDescription)
        {
            Name = name;
            TranslatedDescription = translatedDescription;
        }
    }
}
