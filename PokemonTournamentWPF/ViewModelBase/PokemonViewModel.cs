using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentWPF.ViewModelBase
{
    public class PokemonViewModel : ViewModelBase
    {
        // Model encapsulé dans le ViewModel
        private PokemonTournamentEntities.Pokemon _pokemon;

        public PokemonTournamentEntities.Pokemon Pokemon
        {
            get { return _pokemon; }
            private set { _pokemon = value; }
        }

        public PokemonViewModel(PokemonTournamentEntities.Pokemon pokemonModel)
        {
            _pokemon = pokemonModel;
        }

        #region "Propriétés accessibles, mappables par la View"

        public string Nom
        {
            get { return _pokemon.Nom; }
            set
            {
                if (value == _pokemon.Nom) return;
                _pokemon.Nom = value;
                base.OnPropertyChanged("Nom");
            }
        }

        public int Vie
        {
            get { return _pokemon.Vie; }
            set
            {
                if (value == _pokemon.Vie) return;
                _pokemon.Vie = value;
                base.OnPropertyChanged("Vie");
            }
        }

        public int Force
        {
            get { return _pokemon.Force; }
            set
            {
                if (value == _pokemon.Force) return;
                _pokemon.Force = value;
                base.OnPropertyChanged("Force");
            }
        }

        public int Defense
        {
            get { return _pokemon.Defense; }
            set
            {
                if (value == _pokemon.Defense) return;
                _pokemon.Defense = value;
                base.OnPropertyChanged("Defense");
            }
        }

        public List<PokemonTournamentEntities.TypeElement> Types 
        {
            get { return _pokemon.Types; }
            set
            {
                if (value == _pokemon.Types) return;
                _pokemon.Types = value;
                base.OnPropertyChanged("Types");
            }
        }

        public override String ToString()
        {
            return Nom;
            //return Nom + ", vie = " + Vie + ", force = " + Force + ", défense = " + Defense;
        }

        #endregion

    }
}
