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

        // pour les 4 types : Utilisateur, Pokemon, Match, Stade
        // on propose les 3 methodes insert, update, delete.

        // Pour chacun de ces objets et chacunes de ces methodes,
        // on effectue des controles d'existence et de contraintes.

        // Par existence on entend  : on ne rajoute pas un pokemon qui existe deja
        // par contrainte on entend : de la vie negative c'est pas top

        // a noter que certaines contraintes (notamment les pass) sont faibles
        // et que le code en general (c'est une grosse faiblesse de celui-ci)
        // et pas SQLi-safe du tout.

        // -----------------------------------------------------------
        // Pour ajouter il faut : not(exist) and constraints
        // pour updater il faut : exist and constraints
        // pour deleter il faut : exist
        // -----------------------------------------------------------

        // conditions d'acceptation d'un pokemon -> forme de CNF
        // (conjunctive normal form, en utilisant de morgan)
        private bool PokemonConstraints(Pokemon p)
        {
            bool namelength = p.Nom.Length <= 32;
            bool lifeset = p.Vie <= 100 && p.Vie >= 0;
            bool forceset = p.Force <= 100 && p.Force >= 0;
            bool defenseset = p.Defense <= 100 && p.Defense >= 0;
            bool nonemptytypes = p.Types.Count != 0;
            bool cnf = namelength && lifeset && forceset && defenseset && nonemptytypes;
            return cnf;
        }


        // teste l'existence d'un pokemon par sa clef primaire -> l'id.
        private bool PokemonExistence(Pokemon p)
        {
            List<Pokemon> l = GetAllPokemons();
            bool exist = false;
            foreach (Pokemon x in l) { exist ^= x.id == p.id; }
            return exist;
        }


        
        public int AddPokemon(Pokemon p)
        {  
            if(!PokemonExistence(p) && PokemonConstraints(p))
            {
                return dab.AddPokemon(p);
            } else {
                Console.WriteLine("---- AddPokemon : Mauvaises valeurs ou deja existant ");
                Console.WriteLine("Poke # " + p.id + " (" + p.Nom +")" );
                return 0;
            }
            
        }

        public int DeletePokemon(Pokemon p)
        {
            if (PokemonExistence(p))
            {
                return dab.DeletePokemon(p);
            } else {
                Console.WriteLine("---- DeletePokemon : Pokemon Inexistant ");
                Console.WriteLine("Poke # " + p.id + " (" + p.Nom + ")");
                return 0;

            }
            
        }

        public int UpdatePokemon(Pokemon p)
        {
            if (PokemonExistence(p) && PokemonConstraints(p))
            {
                return dab.UpdatePokemon(p);
            }
            else {
                Console.WriteLine("---- UpdatePokemon : Mauvaises valeurs ou inexistant ");
                Console.WriteLine("Poke # " + p.id + " (" + p.Nom + ")");
                return 0;

            }
        }


        private bool MatchExistence(Match m)
        {
            List<Match> l = GetAllMatchs();
            bool exist = false;
            foreach (Match x in l) { exist ^= x.idm == m.idm; }
            return exist;
        }

        
        // match est lourd en contrainte, car si on ne verifie pas la contrainte et l'existence
        // de chacun des objets, on pourrait tenter de creer un match avec des pokemon
        // qui seraient hors cadre. Par exemple on pourrait faire :
        //   new Match(New(Pokemon(1, "Pouet", -4, -4, -4, ....), ...);
        private bool MatchConstraints(Match m)
        {
            bool Pk1e = PokemonExistence(m.Pokemon1);
            bool Pk2e = PokemonExistence(m.Pokemon2);
            bool Pk1v = PokemonConstraints(m.Pokemon1);
            bool Pk2v = PokemonConstraints(m.Pokemon2);
            bool Stde = StadeExistence(m.StadePokemon);
            bool Stdv = StadeConstraints(m.StadePokemon);
            bool pkve = m.IdPokemonVainqueur == m.Pokemon1.id || m.IdPokemonVainqueur == m.Pokemon2.id;
            bool nonequalpk = m.Pokemon2.id != m.Pokemon1.id;

            bool pkconstraints = Pk1e && Pk2e && Pk1v && Pk2v && pkve && nonequalpk;
            bool stdconstraints = Stdv && Stde;
          
            return pkconstraints && stdconstraints;
            
        }

        public int AddMatch(Match m)
        {
            if(!MatchExistence(m) && MatchConstraints(m))
            {
                return dab.AddMatch(m);

            } else {
                Console.WriteLine("---- AddPokemon : Mauvaises valeurs ou existant ");
                Console.WriteLine("Mtc # " + m.idm);
                return 0;
            }
            
        }

        public int UpdateMatch(Match m)
        {
            if(MatchExistence(m) && MatchConstraints(m))
            {
                return dab.UpdateMatch(m);
            } else
            {
                Console.WriteLine("---- UpdateMatch : Mauvaises valeurs ou inexistant ");
                Console.WriteLine("Mtce # " + m.idm);
                return 0;
            }
            
        }

        public int DeleteMatch(Match m)
        {
            if (MatchExistence(m))
            {
                return dab.DeleteMatch(m);
            } else
            {
                Console.WriteLine("---- DeleteMatch : Match Inexistant ");
                Console.WriteLine("Mtc # " + m.idm);
                return 0;
            }
            
        }


        private bool StadeExistence(Stade s)
        {
            List<Stade> l = GetAllStades();
            bool exist = false;
            foreach (Stade x in l) { exist ^= x.ids == s.ids; }
            return exist;
        }

        private bool StadeConstraints(Stade s)
        {
            bool nameval = s.Nom.Length < 32 && !s.Nom.Contains(" ");
            bool nbpval = s.NbPlaces <= 100000 && s.NbPlaces >= 1000;
            bool typeval = s.Types.Count == 1;
            return false;
        }

        public int AddStade(Stade s)
        {
            if(!StadeExistence(s) && StadeConstraints(s))
            {
                return dab.AddStade(s);
            } else
            {
                Console.WriteLine("---- AddStade : Mauvaises valeurs ou existant ");
                Console.WriteLine("Std # " + s.ids + "( " + s.Nom + ")");
                return 0;
            }
            
        }

        public int UpdateStade(Stade s)
        {
            if(StadeExistence(s) && StadeConstraints(s))
            {
                return dab.UpdateStade(s);
            } else
            {
                Console.WriteLine("---- UpdateStade : Mauvaises valeurs ou inexistant ");
                Console.WriteLine("Std # " + s.ids + "( " + s.Nom + ")");
                return 0;
            }
           
        }

        public int DeleteStade(Stade s)
        {
            if(StadeExistence(s))
            {
                return dab.DeleteStade(s);
            }
            else
            {
                Console.WriteLine("---- DeleteStade : Mauvaises valeurs ou inexistant ");
                Console.WriteLine("Std # " + s.ids + "( " + s.Nom + ")");
                return 0;
            }

        }



        private bool UtilisateurExistence(Utilisateur u)
        {
            List<Utilisateur> l = GetAllUtilisateurs();
            bool exist = false;
            foreach (Utilisateur x in l) { exist ^= x.idu == u.idu; }
            return exist;
        }

        private bool UtilisateurConstraints(Utilisateur u)
        {
            bool logval = u.Login.Length < 32;
            bool passval = u.Password.Length < 32;
            bool nomval = u.Nom.Length < 32;
            bool prenomval = u.Prenom.Length < 32;

            return logval && passval && nomval && prenomval;
        }


        public int AddUtilisateur(Utilisateur u)
        {
            if (!UtilisateurExistence(u) && UtilisateurConstraints(u))
            {
                return dab.AddUtilisateur(u);
            }
            else
            {
                Console.WriteLine("---- AddUtilisateur : Mauvaises valeurs ou existant ");
                Console.WriteLine("Usr # " + u.idu + "( " + u.Prenom + ")");
                return 0;
            }
        }



        public int UpdateUtilisateur(Utilisateur u)
        {
            if (UtilisateurExistence(u) && UtilisateurConstraints(u))
            {
                return dab.UpdateUtilisateur(u);
            }
            else
            {
                Console.WriteLine("---- UpdateUtilisateur : Mauvaises valeurs ou inexistant ");
                Console.WriteLine("Usr # " + u.idu + "( " + u.Nom + ")");
                return 0;
            }
        }

        public int DeleteUtilisateur(Utilisateur u)
        {
            if (UtilisateurExistence(u))
            {
                return dab.DeleteUtilisateur(u);
            }
            else
            {
                Console.WriteLine("---- DeleteUtilisateur : Mauvaises valeurs ou inexistant ");
                Console.WriteLine("Usr # " + u.idu + "( " + u.Nom + ")");
                return 0;
            }
        }
    }
}
