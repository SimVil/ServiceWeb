using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public class Utilisateur
    {
        public String Nom { get; private set; }
        public String Prenom { get; set; }
        public String Login { get; private set; }
        public String Password { get; private set; }
        private static int nb = 10; // 10 pour eviter de recreer par defaut des id existants
        public int idu { get; private set; }

        public Utilisateur(String nom, String prenom, String login, String password)
        {
            Nom = nom;
            Prenom = prenom;
            Login = login;
            Password = password;
            idu = nb;
            nb++;
        }

        public Utilisateur(int i, String nom, String prenom, String login, String password)
        {
            Nom = nom;
            Prenom = prenom;
            Login = login;
            Password = password;
            idu = i;
        }

        public override String ToString()
        {
            return Prenom;
           
        }
    }
}
