using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using System.Collections.ObjectModel;

namespace PokemonTournamentWPF.ViewModelBase
{
    public class PokemonViewModel : ViewModelBase
    {

        public event EventHandler<EventArgs> CloseNotified;
        protected void OnCloseNotified(EventArgs e)
        {
            this.CloseNotified(this, e);
        }
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

            types = new ObservableCollection<TypeElement>();
            if(_pokemon != null && _pokemon.Types != null)
            {
                foreach (TypeElement t in _pokemon.Types)
                {
                    types.Add(t);
                }
            }
            _typesDisponibles = new ObservableCollection<TypeElement>();
            foreach (TypeElement t in Enum.GetValues(typeof(TypeElement)).Cast<TypeElement>())
            {
                _typesDisponibles.Add(t);
            }
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


        public ObservableCollection<PokemonTournamentEntities.TypeElement> Types
        {
            get { return types; }
            set
            {
                if (value == types) return;
                types = value;
                base.OnPropertyChanged("Types");
            }
        }
        private ObservableCollection<TypeElement> types;

       
        private ObservableCollection<TypeElement> _typesDisponibles;
        public ObservableCollection<TypeElement> TypesDisponibles
        {
            get { return _typesDisponibles; }
            private set
            {
                _typesDisponibles = value;
                OnPropertyChanged("TypesDisponibles");
            }
        }


        private TypeElement _selectedItemPokemon;
        public TypeElement SelectedItemPokemon
        {
            get { return _selectedItemPokemon; }
            set
            {
                _selectedItemPokemon = value;
                OnPropertyChanged("SelectedItemPokemon");
            }
        }

        private TypeElement _selectedItemDisponibles;
        public TypeElement SelectedItemDisponibles
        {
            get { return _selectedItemDisponibles; }
            set
            {
                _selectedItemDisponibles = value;
                OnPropertyChanged("SelectedItemDisponibles");
            }
        }

        public Uri PokeImage
        {
            get { return _pokemon.PokeImage; }
            set
            {
                if (value == _pokemon.PokeImage) return;
                _pokemon.PokeImage = value;
                base.OnPropertyChanged("PokeImage");
            }
        }

        public override String ToString()
        {
            return Nom;
            //return Nom + ", vie = " + Vie + ", force = " + Force + ", défense = " + Defense;
        }

        #endregion

        #region "Commandes du formulaire"

        // Commande Add
        private RelayCommand _addCommand;
        public System.Windows.Input.ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(
                        () => this.Add(),
                        () => this.CanAdd()
                        );
                }
                return _addCommand;
            }
        }

        private bool CanAdd()
        {
            return true;
        }

        private void Add()
        {
            if (!Types.Contains(SelectedItemDisponibles))
            {
                Types.Add(SelectedItemDisponibles);
                TypesDisponibles.Remove(SelectedItemDisponibles);
            }
        }

        // Commande Remove
        private RelayCommand _removeCommand;
        public System.Windows.Input.ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                {
                    _removeCommand = new RelayCommand(
                        () => this.Remove(),
                        () => this.CanRemove()
                        );
                }
                return _removeCommand;
            }
        }

        private bool CanRemove()
        {
            return true;
        }

        private void Remove()
        {
            if (Types.Contains(SelectedItemPokemon))
            {
                TypesDisponibles.Add(SelectedItemPokemon);
                Types.Remove(SelectedItemPokemon);
            }
        }

        // Commande Close
        private RelayCommand _closeCommand;
        public System.Windows.Input.ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(
                        () => this.Close(),
                        () => this.CanClose()
                        );
                }
                return _closeCommand;
            }
        }

        private bool CanClose()
        {
            return true;
        }

        private void Close()
        {
            OnCloseNotified(new EventArgs());
        }

        #endregion

    }
}
