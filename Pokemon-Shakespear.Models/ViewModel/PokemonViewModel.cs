using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon_Shakespear.Models.ViewModel
{
    public class PokemonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string TranslatedDescription { get; set; }

        public PokemonViewModel()
        {

        }
        public PokemonViewModel(string name, string translatedDescription,int id)
        {
            Id = id;
            Name = name;
            TranslatedDescription = translatedDescription;
        }
    }
}
