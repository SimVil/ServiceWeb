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
        private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=H:\\ZZ2\\Service\\ServiceWeb\\pif.mdf;Integrated Security=True;Connect Timeout=30";

        public DALImplementation() { }

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

        public List<string> GetMatch()
        {
            List<string> results = new List<string>();

            using (SqlConnection SqlCo = new SqlConnection(_connectionString))
            {
                SqlCommand selectPoke = new SqlCommand(
                    "select idm from Pokemon;",
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

        public List<string> getElement()
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

    }
}
