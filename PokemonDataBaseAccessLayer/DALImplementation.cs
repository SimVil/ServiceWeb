using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using PokemonTournamentEntities;

/* Classe faisant le lien avec le SGBD */

namespace PokemonDataBaseAccessLayer
{
    class DALImplementation : DALInterface
    {
        private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Simon\\Desktop\\Cours\\ISIMA\\ZZ-2\\Service Web\\PokemonTournament\\pif.mdf;Integrated Security = True; Connect Timeout = 30";
        //private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=H:\\ZZ2\\Service\\ServiceWeb\\pif.mdf;Integrated Security=True;Connect Timeout=30";
        //private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=H:\\ZZ2\\Service\\ServiceWeb\\Downgraded.mdf;Integrated Security = True; Connect Timeout = 30";


/* Methodes Generale ----------------------------------------------------------
*  + DALImplementation ~ constructeur 
*  + DataRequire       ~ recupere tout type d'objet sous forme de
*                        string
*  + TableRequire      ~ recupere tout type d'obj sous DataTable                                 
* -------------------------------------------------------------------------- */


        public DALImplementation() { }

        private List<string> DataRequire(string request)
        {
            List<string> res = new List<string>();
            List<string> tmp = new List<string>();
            int i = 0;

            using (SqlConnection connect = new SqlConnection(_connectionString))
            {
                SqlCommand select = new SqlCommand(request, connect);
                connect.Open();

                SqlDataReader sqlDaRe = select.ExecuteReader();

                while (sqlDaRe.Read())
                {
                    for(i = 0; i < sqlDaRe.FieldCount; i++) { tmp.Add(sqlDaRe.GetValue(i).ToString()); }
                    res.Add(string.Join(" ", tmp.ToArray()));
                    tmp.Clear();
                     
                }

                connect.Close();
            }

            return res;

        }


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

        

/* Methodes Pokemon -----------------------------------------------------------
*  + GetAllPokemon   ~ recupere tous les pokemons sous forme de string
*  + GetPokemonById  ~ recupere un pokemon en string par son id 
*  + GetPokemonByNom ~ meme chose, par nom
*  
*  - PkBuild         ~ Methode de construction de DataTable sous contraintes
*  - GetPokemonTable ~ Recupere tous les pokemons sous DataTable
*  
*  + AddPokemon      ~ Ajoute un pokemon en base
*  + UpdatePokemon   ~ Met un jour un pokemon
*  + DeletePokemon   ~ Efface un pokemon
*  
*  Rmq : comme les pokemons dependent d'une liste de types, il est 
*  necessaire de manipuler des PokemonType quand on veut modifier des Pks.
* -------------------------------------------------------------------------- */

        public List<string> GetAllPokemons() { return DataRequire("select * from Pokemon;"); }

        public string GetPokemonById(int id) { return string.Format(DataRequire("select * from Pokemon where idp = " + id + ";").First()); }

        public string GetPokemonByNom(string n) { return string.Format(DataRequire("select * from Pokemon where Nom = " + n + ";").First()); }

        private DataTable PkBuild(string s)
        {
            DataTable t = TableRequire(s);
            t.PrimaryKey = new DataColumn[] { t.Columns["idp"] };
            return t;
        }

        private DataTable GetPokemonTable() { return PkBuild("select * from Pokemon;"); }

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
                        connect.Open();
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
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur dans AddPokemon");
                    Console.WriteLine(e.ToString());

                }

                foreach (TypeElement x in p.Types) { res = AddPokemonType(p.id, (Int32)x + 1); }

            }
            else { Console.WriteLine("Pokemon " + p.id + " deja connu !"); }

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
                        connect.Open();
                        SqlCommand cmd = new SqlCommand(r, connect);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                        adapter.UpdateCommand = builder.GetUpdateCommand(true);
                        adapter.InsertCommand = builder.GetInsertCommand(true);
                        adapter.DeleteCommand = builder.GetDeleteCommand(true);

                        t.Rows.Find(p.id).ItemArray = new object[] { p.id, p.Nom, p.Vie, p.Force, p.Defense };

                        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                        res = adapter.Update(t);
                        t.AcceptChanges();
                        connect.Close();


                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur dans UpdatePokemon");
                    Console.WriteLine(e.ToString());

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

            bool exist = t.Rows.Contains(p.id);

            if (exist)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(_connectionString))
                    {
                        connect.Open();
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
            }

            return res;
        }



/* Methodes PokemonType -------------------------------------------------------
* - GetAllPokemonType           ~ recupere tous les PokemonTypes en string.
* + GetPokemonTypeById          ~ recup un PokeType par son id
* - PkTypeBuild                 ~ constructeur de DataTable generique pour
*                                 PokemonType
* - GetPokemonTypeTable         ~ recup des PokemonType en DataTable
* - GetPokemonTypeTableByIdType ~ recup par couple e clef (id, type)
* 
* +  AddPokemonType             ~ Ajoute un PokemonType (cf AddPokemon)
* +  DeletePokemonType          ~ Supprime un PokeType (cf DeletePokemon)
* +  UpdatePokemonType          ~ Met a jour un PokeType
* -------------------------------------------------------------------------- */

        private List<string> GetAllPokemonType() { return DataRequire("select * from PokemonType;"); }

        public List<string> GetPokemonTypeById(int id) { return DataRequire("select * from PokemonType where pkm = " + id + ";"); }

        private DataTable PkTypeBuild(string s)
        {
            DataTable t = TableRequire(s);
            t.PrimaryKey = new DataColumn[] { t.Columns["pkm"], t.Columns["type"] };
            return t;
        }

        private DataTable GetPokemonTypeTable() { return PkTypeBuild("select * from PokemonType;"); }
        
        private DataTable GetPokemonTypeTableById(int id) { return PkTypeBuild("select * from PokemonType where pkm = " + id + ";"); }

        private DataTable GetPokemonTypeTableByIdType(int id, int type) { return PkTypeBuild("select * from PokemonType where pkm = " + id + " and type = " + type + ";"); }

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
                        connect.Open();
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

        public int DeletePokemonType(int id)
        {
            int res = 0;
            string r = "select * from PokemonType;";
            DataTable tb = GetPokemonTypeTableById(id);

            try
            {
                using (SqlConnection connect = new SqlConnection(_connectionString))
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand(r, connect);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                    adapter.DeleteCommand = builder.GetDeleteCommand(true);
                    adapter.UpdateCommand = builder.GetUpdateCommand(true);
                    adapter.InsertCommand = builder.GetInsertCommand(true);

                    for (int i = 0; i < tb.Rows.Count; i++)
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

        public int UpdatePokemonType(Pokemon p)
        {
            int res = 0;
            res = DeletePokemonType(p.id);
            foreach (TypeElement x in p.Types)
            {
                res ^= AddPokemonType(p.id, (Int32)x + 1);
            }

            return res;
        }



/* Methodes Stade -----------------------------------------------------
*  + GetAllStades  ~
*  + GetStadeById  ~
*  
*  - GetStadeTable ~ 
* 
*  + AddStade      ~
*  + UpdateStade   ~
*  + DeleteStade   ~
* 
* ----------------------------------------------------------------- */

        public List<string> GetAllStades() { return DataRequire("select * from Stade;"); }

        public string GetStadeById(int id)
        {
            string r = "select * from Stade where ids = " + id + ";";
            List<string> t = DataRequire(r);

            return string.Format(t.First());
        }

        private DataTable GetStadeTable()
        {
            string r = "select * from Stade;";
            DataTable t = TableRequire(r);
            t.PrimaryKey = new DataColumn[] { t.Columns["ids"] };
            return t;
        }

        public int AddStade(Stade s)
        {
            int res = 0;
            string r = "select * from Stade;";

            DataTable tp = GetStadeTable();
            bool exist = tp.Rows.Contains(s.ids);

            if (!exist)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(_connectionString))
                    {
                        connect.Open();
                        SqlCommand cmd = new SqlCommand(r, connect);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                        adapter.InsertCommand = builder.GetInsertCommand(true);
                        adapter.UpdateCommand = builder.GetUpdateCommand(true);
                        adapter.DeleteCommand = builder.GetDeleteCommand(true);

                        tp.Rows.Add(s.ids, s.NbPlaces, s.Nom, (Int32)s.Types.First() + 1);
                        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                        res = adapter.Update(tp);
                        tp.AcceptChanges();
                        connect.Close();

                    }
                } catch (Exception e) {
                    Console.WriteLine("Erreur dans AddStade");
                    Console.WriteLine(e.ToString());
                }

            }
            else { Console.WriteLine("Stade " + s.ids + " deja connu !"); }

            return res;
        }

        public int UpdateStade(Stade s) {
            int res = 0;
            string r = "select * from Stade;";

            DataTable t = GetStadeTable();
            bool exist = t.Rows.Contains(s.ids);

            if (exist)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(_connectionString))
                    {
                        connect.Open();
                        SqlCommand cmd = new SqlCommand(r, connect);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                        adapter.UpdateCommand = builder.GetUpdateCommand(true);
                        adapter.InsertCommand = builder.GetInsertCommand(true);
                        adapter.DeleteCommand = builder.GetDeleteCommand(true);

                        t.Rows.Find(s.ids).ItemArray = new object[] {s.ids, s.NbPlaces, s.Nom, (Int32)s.Types.First() + 1};

                        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                        res = adapter.Update(t);
                        t.AcceptChanges();
                        connect.Close();


                    }
                } catch (Exception e) {
                    Console.WriteLine("Erreur dans UpdateStade");
                    Console.WriteLine(e.ToString());

                }

            }
            else { Console.WriteLine("Update : Le stade nexiste pas !"); }
            return res;
        }

        public int DeleteStade(Stade s)
        {
            int res = 0;
            string r = "select * from Stade;";
            DataTable t = GetStadeTable();

            res = DeleteMatchByStadeId(s.ids);

            bool exist = t.Rows.Contains(s.ids);

            if (exist)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(_connectionString))
                    {
                        connect.Open();
                        SqlCommand cmd = new SqlCommand(r, connect);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                        adapter.DeleteCommand = builder.GetDeleteCommand(true);
                        adapter.UpdateCommand = builder.GetUpdateCommand(true);
                        adapter.InsertCommand = builder.GetInsertCommand(true);

                        t.Rows.Find(s.ids).Delete();

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
            }

            return res;
        }


        /* Methodes Match -----------------------------------------------------
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * ----------------------------------------------------------------- */


        public List<string> GetAllMatchs() { return DataRequire("select * from Match;"); }

        private DataTable MtBuild(string s)
        {
            DataTable t = TableRequire(s);
            t.PrimaryKey = new DataColumn[] { t.Columns["idm"] };
            return t;
        }

        private DataTable GetMatchTable() { return MtBuild("select * from Match;"); }

        private DataTable GetMatchByStadeId(int id) { return MtBuild("select * from Match where std = " + id + ";"); }

        private DataTable GetMatchByPokemonId(int id) { return MtBuild("select * from Match where pk1 = " + id + "or pk2 = " + id + ";"); }

        public int AddMatch(Match m)
        {
            int res = 0;
            string r = "select * from Match;";

            DataTable tp = GetMatchTable();
            bool exist = tp.Rows.Contains(m.idm);

            if (!exist)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(_connectionString))
                    {
                        connect.Open();
                        SqlCommand cmd = new SqlCommand(r, connect);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                        adapter.InsertCommand = builder.GetInsertCommand(true);
                        adapter.UpdateCommand = builder.GetUpdateCommand(true);
                        adapter.DeleteCommand = builder.GetDeleteCommand(true);

                        tp.Rows.Add(m.idm, m.Pokemon1.id, m.Pokemon2.id, m.IdPokemonVainqueur, m.StadePokemon.ids, (Int32)m.PhaseTournoi + 1);
                        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                        res = adapter.Update(tp);
                        tp.AcceptChanges();
                        connect.Close();

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur dans AddMatch");
                    Console.WriteLine(e.ToString());

                }

            }
            else { Console.WriteLine("Match " + m.idm + " deja enregistre !"); }
            return res;
        }


        public int DeleteMatch(Match m)
        {

            int res = 0;
            string r = "select * from Match;";
            DataTable t = GetMatchTable();

            bool exist = t.Rows.Contains(m.idm);

            if (exist)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(_connectionString))
                    {
                        connect.Open();
                        SqlCommand cmd = new SqlCommand(r, connect);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                        adapter.DeleteCommand = builder.GetDeleteCommand(true);
                        adapter.UpdateCommand = builder.GetUpdateCommand(true);
                        adapter.InsertCommand = builder.GetInsertCommand(true);

                        t.Rows.Find(m.idm).Delete();

                        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        res = adapter.Update(t);
                        t.AcceptChanges();
                        connect.Close();


                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur dans DeleteMatch");
                    Console.WriteLine(e.ToString());

                }
            }

            return res;

        }

        public int UpdateMatch(Match m)
        {
            int res = 0;
            string r = "select * from Match;";

            DataTable t = GetMatchTable();
            bool exist = t.Rows.Contains(m.idm);

            if (exist)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(_connectionString))
                    {
                        connect.Open();
                        SqlCommand cmd = new SqlCommand(r, connect);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                        adapter.UpdateCommand = builder.GetUpdateCommand(true);
                        adapter.InsertCommand = builder.GetInsertCommand(true);
                        adapter.DeleteCommand = builder.GetDeleteCommand(true);

                        t.Rows.Find(m.idm).ItemArray = new object[] {
                            m.idm,
                            m.Pokemon1.id,
                            m.Pokemon2.id,
                            m.IdPokemonVainqueur,
                            m.StadePokemon.ids,
                            (Int32)m.PhaseTournoi + 1 };

                        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                        res = adapter.Update(t);
                        t.AcceptChanges();
                        connect.Close();


                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur dans UpdateMatch");
                    Console.WriteLine(e.ToString());

                }

            }
            else { Console.WriteLine("Update : Le match nexiste pas !"); }
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
                    connect.Open();
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

        public int DeleteMatchByStadeId(int id)
        {

            int res = 0;
            string r = "select * from Match;";
            DataTable t = GetMatchByStadeId(id);

            try
            {
                using (SqlConnection connect = new SqlConnection(_connectionString))
                {
                    connect.Open();
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
                Console.WriteLine("Erreur dans DeleteStadeByPokemonId");
                Console.WriteLine(e.ToString());

            }

            return res;
        }




        /* Methodes Utilisateur -----------------------------------------------
         *  + GetAllUtilisateur   ~ recupere tous les users
         *  - GetUtilisateurTable ~ recupere la table des users
         *  - UtilisateurCall     ~ methode generale de connexion a la bd
         *  + AddUtilisateur      ~ ajoute un user
         *  + UpdateUtilisateur   ~ met a jour un user
         *  + DeleteUtilisateur   ~ supprime un user
         *  - AdderUtilisateur    ~ one-liner d'ajout en datatable
         *  - UpdaterUtilisareur  ~ mise a jour
         *  - DeleterUtilisateur  ~ suppression
         *  
         *  Rmq : on utilise une methode un peu generique avec un delegate
         *  our eviter a nouveau la duplication du code de connexion.
         * ----------------------------------------------------------------- */
        
        public List<string> GetAllUtilisateurs() { return DataRequire("select * from Utilisateur;");  }

        private DataTable GetUtilisateurTable()
        {
            string r = "select * from Utilisateur;";
            DataTable t = TableRequire(r);
            t.PrimaryKey = new DataColumn[] { t.Columns["idu"] };
            return t;
        }

        private delegate void AppliedMethod(Utilisateur u, DataTable t);

        private int UtilisateurCall(Utilisateur u, AppliedMethod meth, DataTable t)
        {
            int res = 0;
            string r = "select * from Utilisateur;";

            try
            {
                using (SqlConnection connect = new SqlConnection(_connectionString))
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand(r, connect);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                    adapter.InsertCommand = builder.GetInsertCommand(true);
                    adapter.UpdateCommand = builder.GetUpdateCommand(true);
                    adapter.DeleteCommand = builder.GetDeleteCommand(true);

                    meth(u, t);
                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    res = adapter.Update(t);
                    t.AcceptChanges();
                    connect.Close();

                }

            } catch (Exception e) {
                Console.WriteLine("Erreur dans Fonctions Utilisateur");
                Console.WriteLine(e.ToString());

            }

            return res;
        }

        public int AddUtilisateur(Utilisateur u)
        {
            DataTable tp = GetUtilisateurTable();

            if (!tp.Rows.Contains(u.idu)) { return UtilisateurCall(u, AdderUtilisateur, tp); }
            Console.WriteLine("AddUtilisateur : utilisateur " + u.idu + " existant.");

            return 0;
            
        }

        public int UpdateUtilisateur(Utilisateur u)
        {
            DataTable tp = GetUtilisateurTable();

            if (tp.Rows.Contains(u.idu)) { return UtilisateurCall(u, UpdaterUtilisateur, tp); }
            Console.WriteLine("UpdateUtilisateur : utilisateur " + u.idu + " NON existant.");

            return 0;
        }

        public int DeleteUtilisateur(Utilisateur u)
        {
            DataTable tp = GetUtilisateurTable();
            if (tp.Rows.Contains(u.idu)) { return UtilisateurCall(u, DeleterUtilisateur, tp); }
            Console.WriteLine("UpdateUtilisateur : utilisateur " + u.idu + " NON existant.");

            return 0;
        }

        private void AdderUtilisateur(Utilisateur u, DataTable t) { t.Rows.Add(u.idu, u.Nom, u.Prenom, u.Login, u.Password); }

        private void UpdaterUtilisateur(Utilisateur u, DataTable t) { t.Rows.Find(u.idu).ItemArray = new object[] { u.idu, u.Nom, u.Prenom, u.Login, u.Password }; }

        private void DeleterUtilisateur(Utilisateur u, DataTable t) { t.Rows.Find(u.idu).Delete(); }



        /* Methodes Element ---------------------------------------------------
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * ----------------------------------------------------------------- */

        private DataTable GetElementTable()
        {
            string r = "select * from Element;";
            DataTable t = TableRequire(r);
            t.PrimaryKey = new DataColumn[] { t.Columns["ide"] };
            return TableRequire(r);
        }


        public List<string> GetAllElements()
        {
            string r = "select type from Element;";
            return DataRequire(r);
        }







    }
}






