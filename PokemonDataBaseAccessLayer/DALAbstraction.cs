﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;



/* DALAbstraction -------------------------------------------------------------
 * Attr
 *   ~ private DALInterface idal : une Interface pour une implementation 
 *     d'un DAL pour SBGD SQL.
 * 
 * Meth
 *   ~ public List<Pokemon> GetAllPokemons()
 *   ~ public List<Stade> GetAllStades()
 *   ~ public List<Match> GetAllMatchs()
 *   ~ public List<Utilisateur> GetAllUtilisateurs()
 *   ~ public List<Pokemon> GetAllPokemonsFromType(TypeElement type)
 *   ~ public List<String> GetAllElements()
 *   ~ public Utilisateur getUtilisateurByLogin(String login)
 *   ~ public Pokemon definePokemon(string s)
 *   ~ public Stade defineStade(string s)
 *   ~ public Utilisateur defineUtilisateur(string s)
 *   ~ public Match defineMatch(string s)
 *   ~ public int DeletePokemon(Pokemon p)
 *   ~ public int DeletePokemon(Pokemon p)
 * 
 * ------------------------------------------------------------------------- */

namespace PokemonDataBaseAccessLayer
{
    public class DALAbstraction
    {
        private DALInterface idal = new DALImplementation();

        public List<Pokemon> GetAllPokemons()
        {
            List<Pokemon> resu = new List<Pokemon>();
            List<string> pokestring = idal.GetAllPokemons();

            foreach(string s in pokestring) { resu.Add(definePokemon(s)); }

            return resu;
        }
         

        public List<Stade> GetAllStades()
        {
            List<Stade> resu = new List<Stade>();
            List<string> stades = idal.GetAllStades();

            foreach (string s in stades) { resu.Add(defineStade(s)); }
 
            return resu;
        }

        public List<Match> GetAllMatchs()
        {
            List<Match> resu = new List<Match>();
            List<string> matchs = idal.GetAllMatchs();

            foreach (string s in matchs) { resu.Add(defineMatch(s)); }

            return resu;
        }

        public List<Utilisateur> GetAllUtilisateurs()
        {
            List<Utilisateur> res = new List<Utilisateur>();
            List<string> user = idal.GetAllUtilisateurs();

            foreach (string s in user) { res.Add(defineUtilisateur(s)); }

            return res;
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
            return GetAllUtilisateurs().Where(u => u.Login == login).FirstOrDefault();
        }


        public Pokemon definePokemon(string s)
        {

            string[] sub = s.Split(' ');
            List<string> strt = idal.GetPokemonTypeById(Int32.Parse(sub[0]));
            List<TypeElement> types = new List<TypeElement>();

            foreach(string x in strt)
            {
                types.Add((TypeElement)Int32.Parse(x.Split(' ')[1]) - 1);
            }

            Pokemon p = new Pokemon(
                    Int32.Parse(sub[0]), sub[1],
                    Int32.Parse(sub[2]), Int32.Parse(sub[3]),
                    Int32.Parse(sub[4]), types);

            return p;
        }


        public Stade defineStade(string s)
        {
            string[] sub = s.Split(' ');
            Stade std = new Stade(sub[0], Int32.Parse(sub[1]), new List<TypeElement>(1));

            return std;

        }


        public Utilisateur defineUtilisateur(string s)
        {
            string[] sub = s.Split(' ');
            Utilisateur u = new Utilisateur(sub[0], sub[1], sub[2], sub[3]);
            return u;
        }


        public Match defineMatch(string s)
        {
            string[] sub = s.Split(' ');
            Match res = new Match(
                definePokemon(idal.GetPokemonById(Int32.Parse(sub[0]))),
                definePokemon(idal.GetPokemonById(Int32.Parse(sub[1]))),
                Int32.Parse(sub[2]),
                (PhaseTournoi)Int32.Parse(sub[3]),
                defineStade(idal.GetStadeById(Int32.Parse(sub[3]))));

            return res;
        }


        public int AddPokemon(Pokemon p)
        {
            return idal.AddPokemon(p);
        }

        public int DeletePokemon(Pokemon p)
        {
            return idal.DeletePokemon(p);
        }

        

    }
}