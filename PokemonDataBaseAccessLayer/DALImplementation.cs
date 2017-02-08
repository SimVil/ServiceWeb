using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using PokemonTournamentEntities;

// penser a la jointure pour recuperer la liste de type des pokemon !


namespace PokemonDataBaseAccessLayer
{
    class DALImplementation : DALInterface
    {
        private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Simon\\Desktop\\Cours\\ISIMA\\ZZ-2\\Service Web\\PokemonTournament\\pif.mdf;Integrated Security = True; Connect Timeout = 30";
        //private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=H:\\ZZ2\\Service\\ServiceWeb\\pif.mdf;Integrated Security=True;Connect Timeout=30";
        //private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=H:\\ZZ2\\Service\\ServiceWeb\\Downgraded.mdf;Integrated Security = True; Connect Timeout = 30";


        /* fonction recuperant de maniere generale une liste de string de donnee */
        private List<string> DataRequire(string request)
        {
            List<string> res = new List<string>();
            List<string> tmp = new List<string>();
            int i = 0;

            using (SqlConnection SqlCo = new SqlConnection(_connectionString))
            {
                SqlCommand select = new SqlCommand(request, SqlCo);
                SqlCo.Open();

                SqlDataReader sqlDaRe = select.ExecuteReader();

                while (sqlDaRe.Read())
                {
                    for(i = 0; i < sqlDaRe.FieldCount; i++)
                    {
                        tmp.Add(sqlDaRe.GetValue(i).ToString());
                   
                    }
                    res.Add(string.Join(" ", tmp.ToArray()));
                    tmp.Clear();
                    // faudra ptet rajouter un clear, mais vu qu'on remplace les id ca
                    // devrait passer
                     
                }

                SqlCo.Close();
            }

            return res;

        }

        /* recupere les datatable */
        private DataTable TableRequire(string r)
        {
            DataTable res = new DataTable();

            using (SqlConnection connect = new SqlConnection(_connectionString))
            {
                SqlCommand com = new SqlCommand(r, connect);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(res);

            }

            return res;

        }

        public DALImplementation() { }

        public List<string> GetAllPokemons()
        {
            string r = "select idp, nom, vie, force, defense from Pokemon;";
            return DataRequire(r);
        }

        public List<string> GetAllPokemonType()
        {
            string r = "select pkm, type from PokemonType;";
            return DataRequire(r);

        }

        public List<string> GetPokemonTypeById(int id)
        {
            string r = "select pkm, type from PokemonType where pkm = " + id + ";";
            return DataRequire(r);
        }

        public List<string> GetAllStades()
        {
            string r = "select nom, nbp from Stade;";
            return DataRequire(r);
        }

        public List<string> GetAllElements()
        {
            string r = "select type from Element;";
            return DataRequire(r);
        }

        public List<string> GetAllMatchs()
        {
            string r = "select pk1, pk2, pkv, std, phs from Match;";
            return DataRequire(r);
        }

        public List<string> GetAllUtilisateurs()
        {
            string r = "select nom, prenom, login, password from Utilisateur;";
            return DataRequire(r);
        }

        public string GetPokemonById(int id)
        {
            string r = "select idp, nom, vie, force, defense from Pokemon where idp = " + id + ";";
            return string.Format(DataRequire(r).First());
        }

        public string GetPokemonByNom(string n)
        {
            string r = "select nom, vie, force, defense from Pokemon where Nom = " + n + ";";
            return string.Format(DataRequire(r).First());
        }
        
        public string GetStadeById(int id)
        {
            string r = "select nom, nbp from Stade where ids = " + id + ";";
            return string.Format(DataRequire(r).First());
        }

        private DataTable GetPokemonTable()
        {
            string r = "select * from Pokemon;";
            return TableRequire(r);
        }

        private DataTable GetPokemonTypeTable()
        {
            string r = "select * from PokemonType;";
            return TableRequire(r);
        }

        private DataTable GetUtilisateurTable()
        {
            string r = "select * from Utilisateur;";
            return TableRequire(r);
        }

        private DataTable GetStadeTable()
        {
            string r = "select * from Stade;";
            return TableRequire(r);
        }

        private DataTable GetMatchTable()
        {
            string r = "select * from Match;";
            return TableRequire(r);
        }

        private DataTable GetElementTable()
        {
            string r = "select * from Element;";
            return TableRequire(r);
        }


        // considere que l'on peut avoir plusieurs fois un pokemon dans la db
        public int AddPokemon(Pokemon p)
        {
            int res = 0;
            string r = "INSERT INTO Pokemon(idp, nom, vie, force, defense)" +
                       "VALUES (@idp, @nom, @vie, @force, @defense)";

            DataTable t = (GetPokemonTable());

            try
            {
                using (SqlConnection connect = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(r, connect);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                    adapter.InsertCommand = cmd;
                    adapter.SelectCommand = new SqlCommand("select * from Pokemon", connect);
                    adapter.UpdateCommand = builder.GetUpdateCommand(true);
                    adapter.DeleteCommand = builder.GetDeleteCommand(true);

                    adapter.InsertCommand.Parameters.AddWithValue("@idp", p.id);
                    adapter.InsertCommand.Parameters.AddWithValue("@nom", p.Nom);
                    adapter.InsertCommand.Parameters.AddWithValue("@vie", p.Vie);
                    adapter.InsertCommand.Parameters.AddWithValue("@force", p.Force);
                    adapter.InsertCommand.Parameters.AddWithValue("@defense", p.Defense);

                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    DataRow nr = t.Rows.Add();
                    nr.SetField("idp", p.id);
                    nr.SetField("nom", p.Nom);
                    nr.SetField("vie", p.Vie);
                    nr.SetField("force", p.Force);
                    nr.SetField("defense", p.Defense);

                    res = adapter.Update(t);
                    

                }
            } catch(Exception e)
            {
                Console.WriteLine("Erreur dans AddPokemon");
                Console.WriteLine(e.ToString());
                
            }

            return res;
        }

        public int DeletePokemon(Pokemon p)
        {
            int res = 0;
            string r = "delete from Pokemon Where idp = @idp";
            DataTable t = (GetPokemonTable());

            try
            {
                using (SqlConnection connect = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(r, connect);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                    adapter.DeleteCommand = cmd;
                    adapter.SelectCommand = new SqlCommand("select * from Pokemon", connect);
                    adapter.UpdateCommand = builder.GetUpdateCommand(true);
                    adapter.InsertCommand = builder.GetInsertCommand(true);

                    adapter.DeleteCommand.Parameters.AddWithValue("@idp", p.id);

                    for(int i = 0; i < t.Rows.Count; i++)
                    {
                        DataRow dr = t.Rows[i];
                        if(dr["idp"].Equals(p.id))
                        {
                            dr.Delete();
                        }
                    }

                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    res = adapter.Update(t);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans DeletePokemon");
                Console.WriteLine(e.ToString());

            }


            return res;
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
