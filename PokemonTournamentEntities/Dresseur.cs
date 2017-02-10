using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public class Dresseur : EntityObject
    {
        public String Nom
        {
            get
            {
                return nom;
            }
            set
            {
                if(nom.Length > 2)
                {
                    nom = value;
                }                
            }
        }
        private String nom;

        public int Score { get; set; }

        public Dresseur(String nom, int score) : base()
        {
            Nom = nom;
            Score = score;
        }

        public override string ToString()
        {
            return "Dresseur " + Nom + ", score = " + Score;
        }
    }
}
