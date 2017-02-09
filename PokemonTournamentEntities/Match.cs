using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public class Match : EntityObject
    {
        public int IdPokemonVainqueur { get; private set; }
        public PhaseTournoi PhaseTournoi { get; private set; }
        public Pokemon Pokemon1 { get; private set; }
        public Pokemon Pokemon2 { get; private set; }
        public Stade StadePokemon { get; private set; }
        private static int nb = 10;
        public int idm {get; private set;}

        public Match(Pokemon pokemon1, Pokemon pokemon2, int idPokemonVainqueur, PhaseTournoi phaseTournoi, Stade stade) : base()
        {
            Pokemon1 = pokemon1;
            Pokemon2 = pokemon2;
            IdPokemonVainqueur = idPokemonVainqueur;
            PhaseTournoi = phaseTournoi;
            StadePokemon = stade;
            idm = nb;
            nb++;
        }

        public Match(int i, Pokemon pokemon1, Pokemon pokemon2, int idPokemonVainqueur, PhaseTournoi phaseTournoi, Stade stade) : base()
        {
            Pokemon1 = pokemon1;
            Pokemon2 = pokemon2;
            IdPokemonVainqueur = idPokemonVainqueur;
            PhaseTournoi = phaseTournoi;
            StadePokemon = stade;
            idm = i;
        }

        public override String ToString()
        {
            return Pokemon1.Nom + " vs " + Pokemon2.Nom;
           /*return @"Match : " + Pokemon1.Nom + " vs " + Pokemon2.Nom + " - " + PhaseTournoi.ToString() + " - " + StadePokemon.ToString() + "\n"
                 + "Le vainqueur est " + (IdPokemonVainqueur == Pokemon1.Id ? Pokemon1.Nom : Pokemon2.Nom);*/

        }
    }
}
