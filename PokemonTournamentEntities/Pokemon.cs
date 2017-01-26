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

        public override String ToString()
        {
            return Nom;
            //return Nom + ", vie = " + Vie + ", force = " + Force + ", défense = " + Defense;
        }
    }
}
