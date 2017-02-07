using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace PokemonDataBaseAccessLayer
{
    class DALImplementation : DALInterface
    {
        private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Simon\\Desktop\\Cours\\ISIMA\\ZZ-2\\Service Web\\PokemonTournament\\pif.mdf;Integrated Security = True; Connect Timeout = 30";
        //private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=H:\\ZZ2\\Service\\ServiceWeb\\pif.mdf;Integrated Security=True;Connect Timeout=30";

        private List<string> DataRequire(string request)
        {
            List<string> res = new List<string>();
            string[] tmp = null;
            int i = 0;

            using (SqlConnection SqlCo = new SqlConnection(_connectionString))
            {
                SqlCommand select = new SqlCommand(request, SqlCo);
                SqlCo.Open();

                SqlDataReader sqlDaRe = select.ExecuteReader();

                while (sqlDaRe.Read())
                {
                    for(i = 1; i <= sqlDaRe.FieldCount; i++)
                    {
                        tmp.SetValue(sqlDaRe.GetString(i), i - 1);
                    }
                    res.Add(string.Join(" ", tmp));
                    // faudra ptet rajouter un clear, mais vu qu'on remplace les id ca
                    // devrait passer
                     
                }

                SqlCo.Close();
            }

            return res;

        }

        public DALImplementation() { }

        public List<string> GetPokemon()
        {
            string r = "select nom, vie, force, defense from Pokemon;";
            return DataRequire(r);
        }

        public List<string> GetStade()
        {
            string r = "select nom, nbp from Stade;";
            return DataRequire(r);
        }

        public List<string> GetElement()
        {
            string r = "select type from Element;";
            return DataRequire(r);
        }

        public List<string> GetMatch()
        {
            string r = "select pk1, pk2, pkv, std, phs from Match;";
            return DataRequire(r);
        }

        public List<string> GetUtilisateur()
        {
            string r = "select nom, prenom, login, from Utilisateur;";
            return DataRequire(r);

        }

        public string GetPokemonById(int id)
        {
            string r = "select nom, vie, force, defense from Pokemon where idp = " + id + ";";
            return string.Format(DataRequire(r).First());
        }
        
        public string GetStadeById(int id)
        {
            string r = "select nom, nbp from Stade where ids = " + id + ";";
            return string.Format(DataRequire(r).First());
        }
       

    }
}







/*
       public List<string> GetPokemon()
       {
           List<string> results = new List<string>();

           using(SqlConnection SqlCo = new SqlConnection(_connectionString))
           {
               SqlCommand selectPoke = new SqlCommand(
                   "select nom, vie, force, defense from Pokemon;",
                   SqlCo);
               SqlCo.Open();

               SqlDataReader sqlDaRe = selectPoke.ExecuteReader();

               while (sqlDaRe.Read())
               {
                   results.Add(string.Format("{0} {1} {2} {3}",
                       sqlDaRe.GetString(1), sqlDaRe.GetString(2), sqlDaRe.GetString(3), sqlDaRe.GetString(4)));

               }

               SqlCo.Close();
           }

           return results;

       }

       public List<string> GetStade()
       {
           List<string> results = new List<string>();

           using (SqlConnection SqlCo = new SqlConnection(_connectionString))
           {
               SqlCommand selectPoke = new SqlCommand(
                   "select nom, nbp from Stade;",
                   SqlCo);
               SqlCo.Open();

               SqlDataReader sqlDaRe = selectPoke.ExecuteReader();

               while (sqlDaRe.Read())
               {
                   results.Add(string.Format("{0} {1}", sqlDaRe.GetString(1), sqlDaRe.GetString(2)));
               }

               SqlCo.Close();

           }

           return results;

       }

       public List<string> GetElement()
       {
           List<string> results = new List<string>();

           using (SqlConnection SqlCo = new SqlConnection(_connectionString))
           {
               SqlCommand selectPoke = new SqlCommand(
                   "select type from Element;",
                   SqlCo);
               SqlCo.Open();

               SqlDataReader sqlDaRe = selectPoke.ExecuteReader();

               while (sqlDaRe.Read())
               {
                   results.Add(string.Format("{0}", sqlDaRe.GetString(1)));
               }

               SqlCo.Close();

           }

           return results;

       }

       public string GetPokemonById(int id)
       {
           string res = null;
           using (SqlConnection SqlCo = new SqlConnection(_connectionString))
           {
               SqlCommand selectPoke = new SqlCommand(
                   "select nom, vie, force, defense from Pokemon\n" +
                   "where idp = id;",
                   SqlCo);

               SqlCo.Open();

               SqlDataReader sqlDaRe = selectPoke.ExecuteReader();

               while (sqlDaRe.Read())
               {
                   res = string.Format("{0} {1} {2} {3}",
                       sqlDaRe.GetString(1), sqlDaRe.GetString(2), sqlDaRe.GetString(3), sqlDaRe.GetString(4));
               }

               SqlCo.Close();
           }
           return res;

       }

       public string GetStadeById(int id)
       {
           string res = null;
           using (SqlConnection SqlCo = new SqlConnection(_connectionString))
           {
               SqlCommand selectPoke = new SqlCommand(
                   "select nom, nbp from Pokemon\n" +
                   "where ids = id;",
                   SqlCo);

               SqlCo.Open();

               SqlDataReader sqlDaRe = selectPoke.ExecuteReader();

               while (sqlDaRe.Read())
               {
                   res = string.Format("{0} {1}",
                       sqlDaRe.GetString(1), sqlDaRe.GetString(2));
               }

               SqlCo.Close();
           }
           return res;


       }

       public List<string> GetMatch()
       {
           List<string> results = new List<string>();

           using (SqlConnection SqlCo = new SqlConnection(_connectionString))
           {
               SqlCommand selectPoke = new SqlCommand(
                   "select pk1, pk2, pkv, std, phs from Match;",
                   SqlCo);
               SqlCo.Open();

               SqlDataReader sqlDaRe = selectPoke.ExecuteReader();

               while (sqlDaRe.Read())
               {
                   results.Add(string.Format("{0} {1} {2} {3} {4}",
                       sqlDaRe.GetString(1),
                       sqlDaRe.GetString(2),
                       sqlDaRe.GetString(3),
                       sqlDaRe.GetString(4),
                       sqlDaRe.GetString(5)));
               }

               SqlCo.Close();

           }

           return results;

       }

       public List<string> GetUtilisateur()
       {
           {
               List<string> results = new List<string>();

               using (SqlConnection SqlCo = new SqlConnection(_connectionString))
               {
                   SqlCommand selectUser = new SqlCommand(
                       "select nom, prenom, login, from Utilisateur;",
                       SqlCo);
                   SqlCo.Open();

                   SqlDataReader sqlDaRe = selectUser.ExecuteReader();

                   while (sqlDaRe.Read())
                   {
                       results.Add(string.Format("{0} {1} {2}",
                           sqlDaRe.GetString(1),
                           sqlDaRe.GetString(2),
                           sqlDaRe.GetString(3)));
                   }

                   SqlCo.Close();

               }

               return results;

           }

       }

   */
