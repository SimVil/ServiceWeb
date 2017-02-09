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

            Stade std = new Stade("Chez-toi", 50000, new List<TypeElement>() { TypeElement.Eau });
            manager.AddStade(std);
            Console.WriteLine("Les stades disponibles : ");
            AfficherStades();

            std.Nom = "Bonjourhan";
            std.NbPlaces = 20000;
            manager.UpdateStade(std);
            Console.WriteLine("Les stades disponibles : ");
            AfficherStades();

            manager.DeleteStade(std);
            Console.WriteLine("Les stades disponibles : ");
            AfficherStades();




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
