
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;

namespace PokemonDataBaseAccessLayer
{
    interface DALInterface
    {
        List<string> GetAllPokemons();

        List<string> GetAllStades();

        List<string> GetAllMatchs();

        List<string> GetAllElements();

        List<string> GetAllUtilisateurs();

        List<string> GetPokemonTypeById(int id);

        string GetPokemonById(int id);

        string GetStadeById(int id);

        string GetPokemonByNom(string n);

        int AddPokemon(Pokemon p);

        int DeletePokemon(Pokemon p);

        int DeletePokemonType(int id);

        int UpdatePokemon(Pokemon p);

        int AddMatch(Match m);

        int UpdateMatch(Match m);

        int DeleteMatch(Match m);

        int AddStade(Stade s);

        int UpdateStade(Stade s);

        int DeleteStade(Stade s);


    }
}

/*
 *         void AddElement();

        void DeleteElement();

        void UpdateElement();
 * 
 *         void AddMatch();

        void DeleteMatch();

        void UpdateMatch();
 * 
 * 
 *         void AddStade();

        void DeleteStade();

        void UpdateStade();
 * 
 * 
 *         void AddPokemon(Pokemon n);

        void DeletePokemon(Pokemon d);

        void UpdatePokemon(Pokemon u);
 * 
 * */
