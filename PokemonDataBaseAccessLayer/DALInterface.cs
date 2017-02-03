using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;

namespace PokemonDataBaseAccessLayer
{
    public abstract class DALInterface
    {
        public abstract List<Pokemon> GetPokemonList();

        public abstract void AddPokemon(Pokemon n);

        public abstract void DeletePokemon(Pokemon d);

        public abstract void UpdatePokemon(Pokemon u);

        public abstract List<Stade> GetStade();

        public abstract void a();

        public abstract List<Match> GetMatch();

        public abstract void List<Element>();

        

        public abstract void ShowAllRecords();

    }
}
