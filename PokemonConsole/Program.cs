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
            Console.WriteLine("\n\nLes matchs disponibles :");
            AfficherMatchs();
            Console.WriteLine("\n\nLes users :");
            AfficherUtilisateur();

            /*manager.AddPokemon(new Pokemon("Michou", 100, 100, 100, new List<TypeElement>() { TypeElement.Eau }));
            Console.WriteLine("\n\nLes pokémons disponibles :");
            AfficherPokemons();*/

            List<Pokemon> l = manager.GetAllPokemons();
            manager.DeletePokemon(l.First());
            Console.WriteLine("\n\nLes pokémons disponibles :");
            AfficherPokemons();
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
