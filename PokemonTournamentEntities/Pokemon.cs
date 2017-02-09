using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace PokemonTournamentEntities
{
    [Serializable]
    public class Pokemon : EntityObject
    {
        public List<TypeElement> Types

        { get; set; }

        public String Nom {get; set;}
        public int Vie { get; set; }
        public int Force { get; set; }
        public int Defense { get; set; }
        public Uri PokeImage { get; set; }
        
        public Pokemon()
        {

        }

        public Pokemon(String nom, int vie, int force, int defense, List<TypeElement> types):base()
        {
            Nom = nom;
            Vie = vie;
            Force = force;
            Defense = defense;
            Types = types;
        }

        public Pokemon(String nom, int vie, int force, int defense, List<TypeElement> types, Uri chemin) : base()
        {
            Nom = nom;
            Vie = vie;
            Force = force;
            Defense = defense;
            Types = types;
            PokeImage = chemin;
        }

        public override String ToString()
        {
            return Nom;
            //return Nom + ", vie = " + Vie + ", force = " + Force + ", défense = " + Defense;
        }

        //la fonction attaquer un autre pokemon : soustrait la valeur de force de la vie
        public void Attaquer(Pokemon Pokemon2)
        {
            if(this.Force <= Pokemon2.Vie)
            {
                Pokemon2.Vie -= this.Force;
            }
            else
            {
                Pokemon2.Vie = 0;
            }
        }

        //la fonction boost, ajout +50 points à la force d'un pokemon s'il a le meme type que le stade
        public void Boost(Stade stadium)
        {
            int i;
            if (stadium != null)
            {
                for (i = 0; i < Types.Count(); i++)
                {
                    if (stadium.Types.Contains(Types[i]))
                    {
                        this.Force += 50;
                    }
                }
            }

        }

        //determine qui va attaquer le premier alétoirement (toss a coin)
        public static Pokemon Begins(Pokemon pok1, Pokemon pok2)
        {
            Random rnd = new Random();
            if(rnd.Next(1,3)%2 == 0)
            {
                return pok1;
            }
            return pok2;
        }

        //Healer le pokemon
        public void Heal()
        {
            this.Vie += 200;
        }


        //fonction qui simule le deroulement d'un combat et renvoie le gagnant
        static public Pokemon Duel(Pokemon pokemon1, Pokemon pokemon2, Stade chosenstadium)
        {
            Pokemon winner = new Pokemon();
            if (pokemon1 != null && pokemon2 != null)
            {
                    //determiner qui va commencer
                    Pokemon first = Pokemon.Begins(pokemon1, pokemon2);
                    Pokemon second = new Pokemon();
                    if (pokemon1 == first)
                    {
                        second = pokemon2;
                    }
                    else
                    {
                        second = pokemon1;
                    }

                    //Booster les pokemon selon les stades
                    if (chosenstadium != null)
                    {
                        first.Boost(chosenstadium);
                        second.Boost(chosenstadium);
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

                    if (pokemon1.Vie == 0 && pokemon2.Vie != 0)
                    {
                        winner = pokemon2;
                    }
                    else
                    {
                        winner = pokemon2;
                    }
                    


            }
            //anoncer le vainqueur
            return winner;
        }
     }
}
