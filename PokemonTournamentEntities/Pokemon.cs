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
        public List<TypeElement> Types { get; set; }

        public String Nom {get; set;}
        public int Vie { get; set; }
        public int Force { get; set; }
        public int Defense { get; set; }
        public int id { get; set; }
        public String PokeImage { get; set; }
        private static int nb = 10;

                
        public Pokemon()
        {
            
        }

        public Pokemon(String nom, int vie, int force, int defense, List<TypeElement> types) : base()
        {
            Nom = nom;
            Vie = vie;
            Force = force;
            Defense = defense;
            Types = types;
            id = nb;
            nb++;

        }

        public Pokemon(int i, String nom, int vie, int force, int defense, List<TypeElement> types) : base()
        {
            Nom = nom;
            Vie = vie;
            Force = force;
            Defense = defense;
            Types = types;
            id = i;

        }

        public Pokemon(String nom, int vie, int force, int defense, List<TypeElement> types, String chemin) : base()
        {
            Nom = nom;
            Vie = vie;
            Force = force;
            Defense = defense;
            Types = types;
            PokeImage = chemin;
            id = nb;
            nb++;
        }

        //la fonction attaquer un autre pokemon : soustrait la valeur de force de la vie
        public void Attaquer(Pokemon Pokemon2)
        {
            if (this.Force <= Pokemon2.Vie)
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

        public override String ToString()
        {
            return Nom;
            /*string t = "";
            foreach(TypeElement x in Types)
            {
                t = t + x.ToString() + " ";
            }
            return Nom + ", vie = " + Vie + ", force = " + Force + ", défense = " + Defense + " types = " + t;*/
        }

        public string PokemonFormat()
        {
            return "'" + Nom + "', " + Vie + ", " + Force + ", " + Defense;
        }
    }
}
