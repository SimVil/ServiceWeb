using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentWPF.ViewModelBase
{
    public class MatchViewModel : ViewModelBase
    {
        // Model encapsulé dans le ViewModel
        private PokemonTournamentEntities.Match _match;

        public PokemonTournamentEntities.Match Match
        {
            get { return _match; }
            private set { _match = value; }
        }

        public MatchViewModel(PokemonTournamentEntities.Match pokemonModel)
        {
            _match = pokemonModel;
        }

        #region "Propriétés accessibles, mappables par la View"

        public PokemonTournamentEntities.Pokemon Pokemon1
        {
            get { return _match.Pokemon1; }
            set
            {
                if (value == _match.Pokemon1) return;
                _match.Pokemon1 = value;
                base.OnPropertyChanged("Pokemon1");
            }
        }

        public PokemonTournamentEntities.Pokemon Pokemon2
        {
            get { return _match.Pokemon2; }
            set
            {
                if (value == _match.Pokemon2) return;
                _match.Pokemon2 = value;
                base.OnPropertyChanged("Pokemon2");
            }
        }

        public int IdPokemonVainqueur
        {
            get { return _match.IdPokemonVainqueur; }
            set
            {
                if (value == _match.IdPokemonVainqueur) return;
                _match.IdPokemonVainqueur = value;
                base.OnPropertyChanged("IdPokemonVainqueur");
            }
        }

        public PhaseTournoi PhaseTournoi
        {
            get { return _match.PhaseTournoi; }
            set
            {
                if (value == _match.PhaseTournoi) return;
                _match.PhaseTournoi = value;
                base.OnPropertyChanged("PhaseTournoi");
            }
        }

        public Stade StadePokemon
        {
            get { return _match.StadePokemon; }
            set
            {
                if (value == _match.StadePokemon) return;
                _match.StadePokemon = value;
                base.OnPropertyChanged("StadePokemon");
            }
        }

        public override String ToString()
        {
            return Pokemon1 + " vs " + Pokemon2;
            /*return @"Match : " + Pokemon1.Nom + " vs " + Pokemon2.Nom + " - " + PhaseTournoi.ToString() + " - " + StadePokemon.ToString() + "\n"
                  + "Le vainqueur est " + (IdPokemonVainqueur == Pokemon1.Id ? Pokemon1.Nom : Pokemon2.Nom);*/

        }

        #endregion

    }
}
