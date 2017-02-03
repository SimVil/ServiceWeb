using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PokemonTournamentWPF.ViewModelBase
{
    class StadeViewModel : ViewModelBase
    {
        // Model encapsulé dans le ViewModel
        private PokemonTournamentEntities.Stade _stade;

        public PokemonTournamentEntities.Stade Stade
        {
            get { return _stade; }
            private set { _stade = value; }
        }

        public StadeViewModel(PokemonTournamentEntities.Stade stadeModel)
        {
            _stade = stadeModel;
        }

        #region "Propriétés accessibles, mappables par la View"

        public string Nom
        {
            get { return _stade.Nom; }
            set
            {
                if (value == _stade.Nom) return;
                _stade.Nom = value;
                base.OnPropertyChanged("Nom");
            }
        }

        public int NbPlaces
        {
            get { return _stade.NbPlaces; }
            set
            {
                if (value == _stade.NbPlaces) return;
                _stade.NbPlaces = value;
                base.OnPropertyChanged("NbPlaces");
            }
        }

        public List<PokemonTournamentEntities.TypeElement> Types
        {
            get { return _stade.Types; }
            set
            {
                if (value == _stade.Types) return;
                _stade.Types = value;
                base.OnPropertyChanged("Types");
            }
        }

        public override string ToString()
        {
            return Nom;
            //return Nom + ", " + NbPlaces + " places, arène de type " + string.Join(" ", Types.ToArray());
        }

        #endregion
    }
}
