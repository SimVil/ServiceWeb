﻿
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

        string GetPokemonById(int id);

        string GetStadeById(int id);


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
