using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using PokemonTournamentEntities;

// penser a la jointure pour recuperer la liste de type des pokemon !

/* Methodes existantes (sinon je me perds)
 * 
 * private List<string> DataRequire(string request)
 * private DataTable TableRequire(string r)
 * public DALImplementation() { }
 * public List<string> GetAllPokemons()
 * public List<string> GetAllPokemonType()
 * public List<string> GetPokemonTypeById(int id)
 * public List<string> GetAllStades()
 * public List<string> GetAllElements()
 * public List<string> GetAllMatchs()
 * public List<string> GetAllUtilisateurs()
 * public string GetPokemonById(int id)
 * public string GetPokemonByNom(string n)
 * public string GetStadeById(int id)
 * private DataTable GetPokemonTable()
 * private DataTable GetPokemonTypeTable()
 * private DataTable GetPokemonTypeTableById(int id)
 * private DataTable GetUtilisateurTable()
 * private DataTable GetStadeTable()
 * private DataTable GetMatchTable()
 * private DataTable GetElementTable()
 * private DataTable GetMatchByPokemonId(int id)
 * private DataTable GetPokemonTypeTableByIdType(int id, int type)
 * public int AddPokemon(Pokemon p)
 * public int AddPokemonType(int id, int type)
 * public int DeletePokemon(Pokemon p)
 * public int DeletePokemonType(int id)
 * public int DeleteMatchByPokemonId(int id)
 * 
 * 
 * 
 * */

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
            DataTable t = TableRequire(r);
            t.PrimaryKey = new DataColumn[] { t.Columns["idp"] }; 
            return t;
        }

        private DataTable GetPokemonTypeTable()
        {
            string r = "select * from PokemonType;";
            DataTable t = TableRequire(r);
            t.PrimaryKey = new DataColumn[] { t.Columns["pkm"], t.Columns["type"] };
            return t;
        }

        private DataTable GetPokemonTypeTableById(int id)
        {
            string r = "select pkm, type from PokemonType where pkm = " + id + ";";
            DataTable t = TableRequire(r);
            t.PrimaryKey = new DataColumn[] { t.Columns["pkm"], t.Columns["type"] };
            return t;
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

        private DataTable GetMatchByPokemonId(int id)
        {
            string r = "select * from Match where pk1 = " + id + "or pk2 = " + id + ";";
            return TableRequire(r);
        }

        private DataTable GetPokemonTypeTableByIdType(int id, int type)
        {
            string r = "select * from PokemonType where pkm = " + id + " and type = " + type + ";";
            return TableRequire(r);
        }

        // considere que l'on peut avoir plusieurs fois un pokemon dans la db
       
        public int AddPokemon(Pokemon p)
        {
            int res = 0;
            string r = "select * from Pokemon;";

            DataTable tp = (GetPokemonTable());
            bool exist = tp.Rows.Contains(p.id);

            if (!exist)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(_connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(r, connect);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                        adapter.InsertCommand = builder.GetInsertCommand(true);
                        adapter.UpdateCommand = builder.GetUpdateCommand(true);
                        adapter.DeleteCommand = builder.GetDeleteCommand(true);

                        tp.Rows.Add(p.id, p.Nom, p.Vie, p.Force, p.Defense);
                        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                        res = adapter.Update(tp);
                        tp.AcceptChanges();
                        connect.Close();

                    }
                } catch (Exception e) {
                    Console.WriteLine("Erreur dans AddPokemon");
                    Console.WriteLine(e.ToString());

                }

                foreach(TypeElement x in p.Types)
                {
                   res = AddPokemonType(p.id, (Int32)x + 1);
                }
            }
            else
            {
                Console.WriteLine("Pokemon " + p.id + " deja connu !");

            }
            return res;
        }

        public int UpdatePokemon(Pokemon p)
        {
            int res = 0;
            string r = "select * from Pokemon;";

            DataTable t = GetPokemonTable();
            bool exist = t.Rows.Contains(p.id);

            if (exist)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(_connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(r, connect);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                        adapter.UpdateCommand = builder.GetUpdateCommand(true);
                        adapter.InsertCommand = builder.GetInsertCommand(true);
                        adapter.DeleteCommand = builder.GetDeleteCommand(true);

                        t.Rows.Find(p.id).ItemArray = new object[] { p.id, p.Nom, p.Vie, p.Force, p.Defense };

                        //adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                        res = adapter.Update(t);
                        t.AcceptChanges();
                        connect.Close();


                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur dans UpdatePokemon");
                    //Console.WriteLine(e.ToString());
                    throw e;

                }

                res ^= UpdatePokemonType(p);

            }
            else
            {
                Console.WriteLine("Update : Le pokemon nexiste pas !");

            }
            return res;


        }



        public int DeletePokemon(Pokemon p)
        {
            int res = 0;
            string r = "select * from Pokemon;";
            DataTable t = GetPokemonTable();

            res = DeletePokemonType(p.id);
            res = DeleteMatchByPokemonId(p.id);

            try
            {
                using (SqlConnection connect = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(r, connect);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                    adapter.DeleteCommand = builder.GetDeleteCommand(true);
                    adapter.UpdateCommand = builder.GetUpdateCommand(true);
                    adapter.InsertCommand = builder.GetInsertCommand(true);

                    t.Rows.Find(p.id).Delete();

                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    res = adapter.Update(t);
                    t.AcceptChanges();
                    connect.Close();


                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans DeletePokemon");
                Console.WriteLine(e.ToString());

            }


            return res;
        }

        public int DeletePokemonType(int id)
        {
            int res = 0;
            string r = "select * from PokemonType;";
            DataTable tb = GetPokemonTypeTableById(id);

            try
            {
                using (SqlConnection connect = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(r, connect);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                    adapter.DeleteCommand = builder.GetDeleteCommand(true);
                    adapter.UpdateCommand = builder.GetUpdateCommand(true);
                    adapter.InsertCommand = builder.GetInsertCommand(true);

                    for(int i = 0; i < tb.Rows.Count; i++)
                    {
                        tb.Rows[i].Delete();
                    }

                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    res = adapter.Update(tb);
                    tb.AcceptChanges();
                    connect.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans DeletePokemonType");
                Console.WriteLine(e.ToString());

            }

            return res;
        }

        public int AddPokemonType(int id, int type)
        {
            int res = 0;
            string r = "select * from PokemonType;";
            DataTable t = GetPokemonTypeTableById(id);

            bool exist = t.Rows.Contains(new Object[] { id, type });

            if (!exist)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(_connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(r, connect);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                        adapter.InsertCommand = builder.GetInsertCommand(true);
                        adapter.UpdateCommand = builder.GetUpdateCommand(true);
                        adapter.DeleteCommand = builder.GetDeleteCommand(true);

                        t.Rows.Add(id, type);

                        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                        res = adapter.Update(t);
                        t.AcceptChanges();
                        connect.Close();

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur dans AddPokemon");
                    Console.WriteLine(e.ToString());

                }
            }

            return res;
        }

        public int UpdatePokemonType(Pokemon p)
        {
            int res = 0;
            res = DeletePokemonType(p.id);
            foreach(TypeElement x in p.Types)
            {
                res ^= AddPokemonType(p.id, (Int32)x + 1);
            }

            return res;
        }



        public int DeleteMatchByPokemonId(int id)
        {
            int res = 0;
            string r = "select * from Match;";
            DataTable t = GetMatchByPokemonId(id);

            try
            {
                using (SqlConnection connect = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(r, connect);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                    adapter.DeleteCommand = builder.GetDeleteCommand(true);
                    adapter.UpdateCommand = builder.GetUpdateCommand(true);
                    adapter.InsertCommand = builder.GetInsertCommand(true);

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        t.Rows[i].Delete();
                    }

                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    res = adapter.Update(t);
                    t.AcceptChanges();
                    connect.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans DeleteMatchByPokemonId");
                Console.WriteLine(e.ToString());

            }

            return res;
        }


    }
}






