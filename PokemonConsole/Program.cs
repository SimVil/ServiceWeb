using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonBusinessLayer;
using PokemonTournamentEntities;

namespace PokemonConsole
{
    public class Program
    {
        private PokemonTournamentManager manager;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.AfficherMonde();
            Console.ReadKey();
        }

        public void AfficherMonde()
        {
            manager = new PokemonTournamentManager();
            Console.WriteLine("Les stades disponibles : ");
            AfficherStades();
            Console.WriteLine("\n\nLes pokémons disponibles :");
            AfficherPokemons();
            Console.WriteLine("\n\nLes matchs disponibles : ");
            AfficherMatchs();

            Pokemon Onigali = manager.GetAllPokemons().Find(x => x.Nom.Equals("Onigali"));
            Pokemon Tortank = manager.GetAllPokemons().Find(x => x.Nom.Equals("Tortank"));
            Stade std = manager.GetAllStades().Find(x => x.Nom.Equals("Stade-neutre"));
            Match mat = new Match(Onigali, Tortank, Onigali.id, PhaseTournoi.DemiFinale, std);
            manager.AddMatch(mat);

            Console.WriteLine("\n\nLes matchs disponibles : ");
            AfficherMatchs();

            manager.DeleteMatch(mat);
            Console.WriteLine("\n\nLes matchs disponibles : ");
            AfficherMatchs();




        }

        public void AfficherStades()
        {
            manager.GetAllStades().ForEach(s => Console.WriteLine(s));
        }

        public void AfficherPokemons()
        {
            manager.GetAllPokemons().ForEach(p => Console.WriteLine(p));
        }

        public void AfficherMatchs()
        {
            manager.GetAllMatchs().ForEach(m => Console.WriteLine(m));
        }

        public void AfficherUtilisateur()
        {
            manager.GetAllUtilisateurs().ForEach(m => Console.WriteLine(m));
        }
    }
}
