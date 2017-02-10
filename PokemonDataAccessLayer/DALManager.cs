﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using System.IO;



namespace PokemonDataAccessLayer
{
    public class DALManager
    {
        private List<Pokemon> Pokemons;
        private List<Stade> Stades;
        private List<Match> Matchs;
        private List<Utilisateur> Utilisateurs;
        private List<TypeElement> Types;
        
        public static DALManager Instance { get; private set; } //= null;

        private DALManager()
        {
            Pokemons = new List<Pokemon>();
            Stades = new List<Stade>();
            Matchs = new List<Match>();
            Utilisateurs = new List<Utilisateur>();
            Types = new List<TypeElement>();
            CreerMonde();
            Instance = this;            
        }

        private void CreerMonde()
        {
            string dir = System.IO.Directory.GetCurrentDirectory().ToString();
            dir = System.IO.Directory.GetParent(dir).ToString();
            dir = System.IO.Directory.GetParent(dir).ToString();
            dir = System.IO.Directory.GetParent(dir).ToString();
            Pokemon p1 = new Pokemon("Electhor", 200, 75, 30, new List<TypeElement>() { TypeElement.Electrique, TypeElement.Vol },  dir+@"\Images\electhor.png");
            Pokemon p2 = new Pokemon("Brasegali", 160, 90, 25, new List<TypeElement>() { TypeElement.Feu }, dir + @"\Images\Brasegali.png");
            Pokemon p3 = new Pokemon("Onigali", 180, 55, 45, new List<TypeElement>() { TypeElement.Glace }, dir + @"\Images\Onigali.png");
            Pokemon p4 = new Pokemon("Lucario", 165, 70, 35, new List<TypeElement>() { TypeElement.Sol }, dir + @"\Images\Lucario.png");
            Pokemon p5 = new Pokemon("Tortank", 220, 60, 30, new List<TypeElement>() { TypeElement.Eau, TypeElement.Sol }, dir + @"\Images\Tortank.png");
            Pokemon p6 = new Pokemon("Germinion", 140, 65, 25, new List<TypeElement>() { TypeElement.Plante }, dir + @"\Images\Germinion.png");
            Pokemon p7 = new Pokemon("Groudon", 200, 85, 35, new List<TypeElement>() { TypeElement.Feu, TypeElement.Sol}, dir + @"\Images\Groudon.png");
            Pokemon p8 = new Pokemon("Kyogre", 210, 75, 35, new List<TypeElement>() { TypeElement.Eau, TypeElement.Vol }, dir + @"\Images\Kyogre.png");
            Pokemons.Add(p1);
            Pokemons.Add(p2);
            Pokemons.Add(p3);
            Pokemons.Add(p4);
            Pokemons.Add(p5);
            Pokemons.Add(p6);
            Pokemons.Add(p7);
            Pokemons.Add(p8);

            Types.Add(TypeElement.Eau);
            Types.Add(TypeElement.Electrique);
            Types.Add(TypeElement.Feu);
            Types.Add(TypeElement.Glace);
            Types.Add(TypeElement.Plante);
            Types.Add(TypeElement.Sol);
            Types.Add(TypeElement.Vol);


            Stade s1 = new Stade("Stade neutre", 75000, new List<TypeElement>(), dir+@"\Images\stade_neutre.jpg");
            Stade s2 = new Stade("Stade éclair", 50000, new List<TypeElement>() { TypeElement.Electrique }, dir+@"\Images\stade_eclair.jpg");
            Stade s3 = new Stade("Stade aquatique", 90000, new List<TypeElement>() { TypeElement.Eau }, dir+@"\Images\stade_aquatique.jpg");
            Stade s4 = new Stade("Stade volcan", 60000, new List<TypeElement>() { TypeElement.Feu }, dir+@"\Images\stade_volcan.jpg");

            Stades.Add(s1);
            Stades.Add(s2);
            Stades.Add(s3);
            Stades.Add(s4);

            Match m1 = new Match(p1, p2, p1.Id, PhaseTournoi.QuartFinale, s2);
            Match m2 = new Match(p3, p4, p3.Id, PhaseTournoi.QuartFinale, s1);
            Match m3 = new Match(p5, p6, p5.Id, PhaseTournoi.QuartFinale, s3);
            Match m4 = new Match(p7, p8, p7.Id, PhaseTournoi.QuartFinale, s4);
            Match m5 = new Match(p1, p3, p1.Id, PhaseTournoi.DemiFinale, s4);
            Match m6 = new Match(p5, p7, p5.Id, PhaseTournoi.DemiFinale, s2);
            Match m7 = new Match(p1, p5, p5.Id, PhaseTournoi.Finale, s1);
            Matchs.Add(m1);
            Matchs.Add(m2);
            Matchs.Add(m3);
            Matchs.Add(m4);
            Matchs.Add(m5);
            Matchs.Add(m6);
            Matchs.Add(m7);

            Utilisateur u1 = new Utilisateur("Boyboy", "Alex", "", "");
            Utilisateurs.Add(u1);
        }

        public static DALManager GetInstance()
        {
            if(Instance == null)
            {
                Instance = new DALManager();
            }
            return Instance;
        }

        public List<Pokemon> GetAllPokemons()
        {
            return Pokemons;
        }

        public List<TypeElement> GetAllTypes()
        {
            return Types;
        }

        public List<Stade> GetAllStades()
        {
            return Stades;
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
