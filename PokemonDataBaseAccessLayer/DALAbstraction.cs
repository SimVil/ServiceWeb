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

        // on peut essayer de passer par une methode generique
        // et des delegate pour eviter la duplication de code degueulasse comme la

        public List<Pokemon> GetAllPokemons()
        {
            List<Pokemon> resu = new List<Pokemon>();
            List<string> pokestring = new List<string>();

            pokestring = idal.GetAllPokemons();

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
            List<Stade> resu = new List<Stade>();
            List<string> stades = new List<string>();
            string[] subs;
            stades = idal.GetAllStades();

            foreach (string s in stades)
            {
                subs = s.Split(' ');

                resu.Add(new Stade(subs[0],
                    Int32.Parse(subs[1]),
                    new List<TypeElement>(1)));
            }

            return resu;
        }

        public List<Match> GetAllMatchs()
        {
            List<Match> resu = new List<Match>();
            List<string> matchs = new List<string>();
            string[] subs;
            matchs = idal.GetAllMatchs();

            foreach (string s in matchs)
            {
                subs = s.Split(' ');
                resu.Add(new Match(
                    definePokemon(idal.GetPokemonById(Int32.Parse(subs[0]))),
                    definePokemon(idal.GetPokemonById(Int32.Parse(subs[1]))),
                    Int32.Parse(subs[2]),
                    constTournoi(Int32.Parse(subs[3])),
                    defineStade(idal.GetStadeById(Int32.Parse(subs[3])))));
            }

            return resu;
        }

        public List<Utilisateur> GetAllUtilisateurs()
        {
            List<Utilisateur> res = new List<Utilisateur>();
            List<string> user = new List<string>();
            string[] subs;

            user = idal.GetAllUtilisateurs();

            foreach (string s in user)
            {
                subs = s.Split(' ');
                res.Add(new Utilisateur(
                    subs[0],
                    subs[1],
                    subs[2],
                    subs[3]));
            }

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
            Pokemon p = new Pokemon(
                    sub[0],
                    Int32.Parse(sub[1]),
                    Int32.Parse(sub[2]),
                    Int32.Parse(sub[3]),
                    new List<TypeElement>(1));

            return p;
        }

        public Stade defineStade(string s)
        {
            string[] sub = s.Split(' ');
            Stade std = new Stade(
                sub[0],
                Int32.Parse(sub[1]),
                new List<TypeElement>(1));

            return std;

        }

        public PhaseTournoi constTournoi(int i)
        {
            PhaseTournoi pt = PhaseTournoi.HuitiemeFinale;

            switch (i)
            {
                case (2):
                    pt = PhaseTournoi.QuartFinale;
                    break;

                case (3):
                    pt = PhaseTournoi.DemiFinale;
                    break;

                case (4):
                    pt = PhaseTournoi.Finale;
                    break;
            }

            return pt;
        }

        public int AddPokemon(Pokemon p)
        {
            return idal.AddPokemon(p);
        }

    }
}
