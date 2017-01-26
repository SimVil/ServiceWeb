using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public class Tournoi : EntityObject
    {
        public List<Match> Matchs { get; private set; }

        public String Nom { get; set; }

        public Tournoi(String nom):base()
        {
            Nom = nom;
        }
    }
}
