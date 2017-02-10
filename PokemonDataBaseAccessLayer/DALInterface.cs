
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;

namespace PokemonDataBaseAccessLayer
{

    // a quoi sert cette classe ?
    // elle est ici pour faire le pont entre la DALAbstraction et l'implementation en dur
    // d'un dal gerant un SGBD specifique. Du coup, en fournissant une interface a un DAlabstraction
    // (cf DALAbstraction) on peut ensuite instancier plein de dalsgbd differents.

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

        int AddUtilisateur(Utilisateur u);

        int UpdateUtilisateur(Utilisateur u);

        int DeleteUtilisateur(Utilisateur u);


    }
}
