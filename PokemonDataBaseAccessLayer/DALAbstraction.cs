using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;


/* cette classe sert a faire le lien entre la DAL et la BL
 * Elle contient une interface pour recuperer les donnees "brutes"
 * de la base de donnees, les formater sous forme d'entite,
 * et les fournir a la business layer pour qu'elle fasse du
 * traitement dessus.
 * 
 * */

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


        // recupere tout les pokemon sous forme d'une liste de poke
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


        // pour recuperer les types, on recupere les poke et on filtre par type
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

        
        // methode permettant de reconstruire un pokemon a partir d'une chaine resultant
        // d'un appel a la base de donnee.
        public Pokemon definePokemon(string s)
        {
            // la chaine contient toues les valeurs que l'on veut separee par un ' '
            // on split donc sur ' ' et on remplit notre pokemon par la suite.
            string[] sub = s.Split(' ');
            List<string> strt = idal.GetPokemonTypeById(Int32.Parse(sub[0]));
            List<TypeElement> types = new List<TypeElement>();

            // sa liste de type
            foreach(string x in strt)
            {
                // on met -1 pour corriger le decalage entre le SQL et le C#
                // On redonne l'exemple de construction. On peut voir dans
                // AddPokemon (Implementation) pour l'exemple en sens inverse
                //
                //  En C#  : Eau <--> 0
                //  En SQL : Eau <--> 1
                types.Add((TypeElement)Int32.Parse(x.Split(' ')[1]) - 1);
            }

            // et ses valeurs.
            Pokemon p = new Pokemon(
                    Int32.Parse(sub[0]), sub[1],
                    Int32.Parse(sub[2]), Int32.Parse(sub[3]),
                    Int32.Parse(sub[4]), types);

            return p;
        }

        // meme chose pour les states
        public Stade defineStade(string s)
        {
            string[] sub = s.Split(' ');
            Stade std = new Stade(Int32.Parse(sub[0]), sub[2], Int32.Parse(sub[1]), new List<TypeElement>(1));
      
            return std;

        }

        // pour les users
        public Utilisateur defineUtilisateur(string s)
        {
            string[] sub = s.Split(' ');
            Utilisateur u = new Utilisateur(sub[1], sub[2], sub[3], sub[4]);
            return u;
        }

        // et pour les match. Un match est lui carrement construit a partir
        // d'autres objets.
        public Match defineMatch(string s)
        {
            string[] sub = s.Split(' ');
            Match res = new Match(
                Int32.Parse(sub[0]),
                definePokemon(idal.GetPokemonById(Int32.Parse(sub[1]))),
                definePokemon(idal.GetPokemonById(Int32.Parse(sub[2]))),
                Int32.Parse(sub[3]),
                (PhaseTournoi)Int32.Parse(sub[4]),
                defineStade(idal.GetStadeById(Int32.Parse(sub[5]))));

            return res;
        }


        // toutes les methodes ecrivant dans la base de donnee 
        public int AddPokemon(Pokemon p) { return idal.AddPokemon(p); }

        public int DeletePokemon(Pokemon p) { return idal.DeletePokemon(p); }

        public int UpdatePokemon(Pokemon p) { return idal.UpdatePokemon(p); }

        public int AddMatch(Match m) { return idal.AddMatch(m); }

        public int UpdateMatch(Match m) { return idal.UpdateMatch(m); }

        public int DeleteMatch(Match m) { return idal.DeleteMatch(m); }

        public int AddStade(Stade s) { return idal.AddStade(s); }

        public int UpdateStade(Stade s) { return idal.UpdateStade(s); }

        public int DeleteStade(Stade s) { return idal.DeleteStade(s); }

        public int AddUtilisateur(Utilisateur u) { return idal.AddUtilisateur(u); }

        public int UpdateUtilisateur(Utilisateur u) { return idal.UpdateUtilisateur(u); }

        public int DeleteUtilisateur(Utilisateur u) { return idal.DeleteUtilisateur(u); }

    }
}
