using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon_Shakespear.Models.Domain
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Pokemon()
        {

        }

        public Pokemon(string name, string description,int id)
        {
            Name = name;
            Description = description;
            Id = id;
        }
    }
}
