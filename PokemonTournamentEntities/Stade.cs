using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public class Stade : EntityObject
    {
        public String Nom { get; set; }
        public int NbPlaces { get; set; }
        public List<TypeElement> Types { get; set; }

        public Stade(String nom, int nbPlaces, List<TypeElement> types) : base()
        {
            Nom = nom;
            NbPlaces = nbPlaces;
            Types = types;
        }

        public override string ToString()
        {
            return Nom;
            //return Nom + ", " + NbPlaces + " places, arène de type " + string.Join(" ", Types.ToArray());
        }
    }
}
