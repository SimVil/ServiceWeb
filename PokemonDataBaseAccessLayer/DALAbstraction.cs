using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;

namespace PokemonDataBaseAccessLayer
{
    public class DALAbstraction
    {
        private DALInterface idal = new DALImplementation();
        

        public List<Pokemon> GetAllPokemons()
        {
            List<Pokemon> resu = new List<Pokemon>();
            List<string> pokestring = new List<string>();

            pokestring = idal.GetPokemon();

            foreach(string s in pokestring)
            {
                Pokemon poke = new Pokemon();
                string[] subStrings = s.Split(' ');

                resu.Add(new Pokemon(subStrings[0],
                    Int32.Parse(subStrings[1]),
                    Int32.Parse(subStrings[2]), 
                    Int32.Parse(subStrings[3]), 
                    new List<TypeElement>(1)));
            }

            return resu;
        }
          

        public List<Stade> GetAllStades()
        {
           
        }

        public List<Match> GetAllMatchs()
        {
            return Matchs;
        }

        public List<Pokemon> GetAllPokemonsFromType(TypeElement type)
        {
            return GetAllPokemons().Where(p => p.Types.Contains(type)).ToList();
        }

        public List<String> GetAllElements()
        {
            return Enum.GetValues(typeof(TypeElement)).Cast<TypeElement>().Select(v => v.ToString()).ToList();
        }

        public Utilisateur getUtilisateurByLogin(String login)
        {
            return Utilisateurs.Where(u => u.Login == login).FirstOrDefault();
        }



    }
}
