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

        //determine qui va attaquer le premier alétoirement (toss a coin)
        public Pokemon Begins(Pokemon pok1, Pokemon pok2)
        {
            Random rnd = new Random();
            if (rnd.Next(1, 3) % 2 == 0)
            {
                return pok1;
            }
            return pok2;
        }

        //fonction qui simule le deroulement d'un combat et renvoie le gagnant
        public Pokemon Duel()
        {
            Pokemon winner = null;
            if (Pokemon1 != null && Pokemon2 != null)
            {
                if(Pokemon1 == Pokemon2)
                {
                    winner = Pokemon1;
                }
                else
                {
                    //determiner qui va commencer
                    Pokemon first = Begins(Pokemon1, Pokemon2);
                    Pokemon second = new Pokemon();
                    if (Pokemon1 == first)
                    {
                        second = Pokemon2;
                    }
                    else
                    {
                        second = Pokemon1;
                    }

                    //Booster les pokemon selon les stades
                    if (StadePokemon != null)
                    {
                        first.Boost(StadePokemon);
                        second.Boost(StadePokemon);
                    }

                    //lancer le combat
                    while (first.Vie > 0 && second.Vie > 0)
                    {
                        first.Attaquer(second);
                        if (second.Vie > 0)
                        {
                            second.Attaquer(first);
                        }
                    }

                    if (Pokemon1.Vie == 0 && Pokemon2.Vie != 0)
                    {
                        winner = Pokemon2;
                    }
                    else
                    {
                        winner = Pokemon1;
                    }
                }
            }
            else
            {
                if(Pokemon1 == null && Pokemon2 != null)
                {
                    winner = Pokemon2;
                }
                else
                {
                    if(Pokemon1 != null && Pokemon2 == null)
                    {
                        winner = Pokemon1;
                    }
                }
            }
            //anoncer le vainqueur
            return winner;
        }

    public override String ToString()
        {
            return Pokemon1.Nom + " vs " + Pokemon2.Nom;
           /*return @"Match : " + Pokemon1.Nom + " vs " + Pokemon2.Nom + " - " + PhaseTournoi.ToString() + " - " + StadePokemon.ToString() + "\n"
                 + "Le vainqueur est " + (IdPokemonVainqueur == Pokemon1.Id ? Pokemon1.Nom : Pokemon2.Nom);*/

        }
    }
}
