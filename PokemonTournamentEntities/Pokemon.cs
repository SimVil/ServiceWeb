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

        //Healer le pokemon
        public void Heal()
        {
            this.Vie += 200;
        }

     }
}
