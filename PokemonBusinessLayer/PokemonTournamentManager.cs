using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using PokemonDataAccessLayer;

namespace PokemonBusinessLayer
{
    public class PokemonTournamentManager
    {
        public Utilisateur getUtilisateurByLogin(String login)
        {
            return DALManager.GetInstance().getUtilisateurByLogin(login);
        }

        /**
         * Retourne les valeurs sous forme d'objets
         */
        public List<Stade> GetAllStades()
        {
            return DALManager.GetInstance().GetAllStades();
        }

        public List<Pokemon> GetAllPokemonsFromType(TypeElement type)
        {
            return DALManager.GetInstance().GetAllPokemonsFromType(type);
        }

        public List<Pokemon> GetAllPokemons()
        {
            return DALManager.GetInstance().GetAllPokemons();
        }

        public List<Match> GetAllMatchs()
        {
            return DALManager.GetInstance().GetAllMatchs();
        }

        public List<TypeElement> GetAllTypes()
        {
            return DALManager.GetInstance().GetAllTypes();
        }

        /**
         * Retourne les valeurs sous forme de String
         */
        public List<String> GetAllStadesString()
        {
            return DALManager.GetInstance().GetAllStades().Select(s => s.ToString()).ToList();
        }

        public List<String> GetAllPokemonsFromTypeString(TypeElement type)
        {
            return DALManager.GetInstance().GetAllPokemonsFromType(type).Select(p => p.ToString()).ToList();
        }

        public List<String> GetAllPokemonsString()
        {
            return DALManager.GetInstance().GetAllPokemons().Select(p => p.ToString()).ToList();
        }

        public List<String> GetAllMatchsString()
        {
            return DALManager.GetInstance().GetAllMatchs().Select(m => m.ToString()).ToList();
        }
    }
}
