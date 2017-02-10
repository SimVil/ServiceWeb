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
        public String StadeImage { get; set; }
        private static int nb = 10;
        public int ids { get; private set; }

        public Stade(String nom, int nbPlaces, List<TypeElement> types) : base()
        {
            Nom = nom;
            NbPlaces = nbPlaces;
            Types = types;
            ids = nb;
            nb++;
        }

        public Stade(int i, String nom, int nbPlaces, List<TypeElement> types) : base()
        {
            Nom = nom;
            NbPlaces = nbPlaces;
            Types = types;
            ids = i;
            
        }

        public Stade(String nom, int nbPlaces, List<TypeElement> types, String chemin) : base()
        {
            Nom = nom;
            NbPlaces = nbPlaces;
            Types = types;
            StadeImage = chemin;
            ids = nb;
            nb++;
        }

        public override string ToString()
        {
            return Nom;
            //return Nom + ", " + NbPlaces + " places, arène de type " + string.Join(" ", Types.ToArray());
        }
    }
}
