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
        public String Prenom { get; private set; }
        public String Login { get; private set; }
        public String Password { get; private set; }

        public Utilisateur(String nom, String prenom, String login, String password)
        {
            Nom = nom;
            Prenom = prenom;
            Login = login;
            Password = password;
        }
    }
}
