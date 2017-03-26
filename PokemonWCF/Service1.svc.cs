using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace PokemonWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

       
        public List<Pokemon> getAllPokemons()
        {
            List<PokemonTournamentEntities.Pokemon> pokemons = PokemonDataAccessLayer.DALManager.GetInstance().GetAllPokemons();
            List<Pokemon> pokemonWCF = new List<Pokemon>();
            int i = 1;
            foreach(PokemonTournamentEntities.Pokemon poke in pokemons)
            {
                Pokemon temp = new Pokemon();
                temp.Nom = poke.Nom;
                temp.Types = poke.Types;
                temp.Defense = poke.Defense;
                temp.Vie = poke.Vie;
                temp.Id = i;
                i++;
                pokemonWCF.Add(temp);
            }

            return pokemonWCF;
        
        }
        
        public List<Stade> getAllStades()
        {
            List<PokemonTournamentEntities.Stade> stades = PokemonDataAccessLayer.DALManager.GetInstance().GetAllStades();
            List<Stade> stadeWCF = new List<Stade>();
            foreach (PokemonTournamentEntities.Stade stade in stades)
            {
                Stade temp = new Stade();
                temp.Nom = stade.Nom;
                temp.Types = stade.Types;
                temp.NbPlaces = stade.NbPlaces;
                stadeWCF.Add(temp);
            }

            return stadeWCF;
        }

        public List<Match> getAllMatchs()
        {
            List<PokemonTournamentEntities.Match> matchs = PokemonDataAccessLayer.DALManager.GetInstance().GetAllMatchs();
            List<Match> matchWCF = new List<Match>();
            int i = 1;
            foreach (PokemonTournamentEntities.Match match in matchs)
            {

                Match temp = new Match();
                Pokemon pokemon1 = new Pokemon();
                Pokemon pokemon2 = new Pokemon();
                Stade stade = new Stade();

                pokemon1.Nom = match.Pokemon1.Nom;
                pokemon1.Vie = match.Pokemon1.Vie;
                pokemon1.Force = match.Pokemon1.Force;
                pokemon1.Defense = match.Pokemon1.Defense;

                pokemon2.Nom = match.Pokemon2.Nom;
                pokemon2.Vie = match.Pokemon2.Vie;
                pokemon2.Force = match.Pokemon2.Force;
                pokemon2.Defense = match.Pokemon2.Defense;

                stade.Nom = match.StadePokemon.Nom;
                stade.NbPlaces = match.StadePokemon.NbPlaces;
                stade.Types = match.StadePokemon.Types;

                temp.Pokemon1 = new Pokemon();
                temp.Pokemon2 = new Pokemon();
                temp.StadePokemon = new Stade();

                temp.Pokemon1 = pokemon1;
                temp.Pokemon2 = pokemon2;
                temp.StadePokemon = stade;
                temp.Id = i;
                i++;
                matchWCF.Add(temp);
            }

            return matchWCF;
        }

        public void addPokemon(Pokemon pokemon)
        {
            getAllPokemons().Add(pokemon);           
        }

        public void addStade(Stade stade)
        {
            getAllStades().Add(stade);
        }

        public void addMatch(Match match)
        {
            getAllMatchs().Add(match);
        }


    }
}
