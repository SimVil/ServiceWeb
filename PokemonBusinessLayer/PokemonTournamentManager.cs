using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using PokemonDataAccessLayer;
using PokemonDataBaseAccessLayer;

namespace PokemonBusinessLayer
{
    public class PokemonTournamentManager
    {

        private DALAbstraction dab;

        public PokemonTournamentManager()
        {
            dab = new DALAbstraction();
        }

        public Utilisateur getUtilisateurByLogin(String login)
        {
            return DALManager.GetInstance().getUtilisateurByLogin(login);
        }

        /**
         * Retourne les valeurs sous forme d'objets
         */
        public List<Stade> GetAllStades()
        {
            //return DALManager.GetInstance().GetAllStades();
            return dab.GetAllStades();
        }

        public List<Pokemon> GetAllPokemonsFromType(TypeElement type)
        {
            //return DALManager.GetInstance().GetAllPokemonsFromType(type);
            return dab.GetAllPokemonsFromType(type);
        }

        public List<Pokemon> GetAllPokemons()
        {
            //return DALManager.GetInstance().GetAllPokemons();
            return dab.GetAllPokemons();
        }

        public List<Match> GetAllMatchs()
        {
            //return DALManager.GetInstance().GetAllMatchs();
            return dab.GetAllMatchs();
        }

        public List<Utilisateur> GetAllUtilisateurs()
        {
            return dab.GetAllUtilisateurs();
        }

        /**
         * Retourne les valeurs sous forme de String
         */
        public List<String> GetAllStadesString()
        {
            //return DALManager.GetInstance().GetAllStades().Select(s => s.ToString()).ToList();
            return dab.GetAllStades().Select(s => s.ToString()).ToList();
        }

        public List<String> GetAllPokemonsFromTypeString(TypeElement type)
        {
            //return DALManager.GetInstance().GetAllPokemonsFromType(type).Select(p => p.ToString()).ToList();
            return dab.GetAllPokemonsFromType(type).Select(s => s.ToString()).ToList();
        }

        public List<String> GetAllPokemonsString()
        {
            //return DALManager.GetInstance().GetAllPokemons().Select(p => p.ToString()).ToList();
            return dab.GetAllPokemons().Select(s => s.ToString()).ToList();
        }

        public List<String> GetAllMatchsString()
        {
            //return DALManager.GetInstance().GetAllMatchs().Select(m => m.ToString()).ToList();
            return dab.GetAllMatchs().Select(m => m.ToString()).ToList();
        }

        public List<string> GetAllUtilisateursString()
        {
            return dab.GetAllUtilisateurs().Select(m => m.ToString()).ToList();
        }

        // ici faire de la gestion et check de validite pour redescendre dans la bd 
        public int AddPokemon(Pokemon p)
        {
            return dab.AddPokemon(p);

        }

        public int DeletePokemon(Pokemon p)
        {
            return dab.DeletePokemon(p);
        }

        public int UpdatePokemon(Pokemon p)
        {
            return dab.UpdatePokemon(p);
        }

        public int AddMatch(Match m)
        {
            return dab.AddMatch(m);
        }

        public int UpdateMatch(Match m)
        {
            return dab.UpdateMatch(m);
        }

        public int DeleteMatch(Match m)
        {
            return dab.DeleteMatch(m);
        }
    }
}
