using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public class Match : EntityObject
    {
        public int IdPokemonVainqueur { get; set; }
        public PhaseTournoi PhaseTournoi { get; set; }
        public Pokemon Pokemon1 { get; set; }
        public Pokemon Pokemon2 { get; set; }
        public Stade StadePokemon { get; set; }

        public Match(Pokemon pokemon1, Pokemon pokemon2, int idPokemonVainqueur, PhaseTournoi phaseTournoi, Stade stade) : base()
        {
            Pokemon1 = pokemon1;
            Pokemon2 = pokemon2;
            IdPokemonVainqueur = idPokemonVainqueur;
            PhaseTournoi = phaseTournoi;
            StadePokemon = stade;
        }

        public override String ToString()
        {
            return Pokemon1.Nom + " vs " + Pokemon2.Nom;
           /*return @"Match : " + Pokemon1.Nom + " vs " + Pokemon2.Nom + " - " + PhaseTournoi.ToString() + " - " + StadePokemon.ToString() + "\n"
                 + "Le vainqueur est " + (IdPokemonVainqueur == Pokemon1.Id ? Pokemon1.Nom : Pokemon2.Nom);*/

        }
    }
}
