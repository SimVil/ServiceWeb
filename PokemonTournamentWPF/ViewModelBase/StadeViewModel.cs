using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace PokemonTournamentWPF.ViewModelBase
{
    public class StadeViewModel : ViewModelBase
    {
        public event EventHandler<EventArgs> CloseNotified;
        protected void OnCloseNotified(EventArgs e)
        {
            this.CloseNotified(this, e);
        }

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

            types = new ObservableCollection<TypeElement>();
            if (_stade != null && _stade.Types != null)
            {
                foreach (TypeElement t in _stade.Types)
                {
                    types.Add(t);
                }
            }
            _typesDisponibles = new ObservableCollection<TypeElement>();
            foreach (TypeElement t in Enum.GetValues(typeof(TypeElement)).Cast<TypeElement>())
            {
                if (!types.Contains(t))
                {
                    _typesDisponibles.Add(t);
                }
            }
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


        private TypeElement _selectedItemStade;
        public TypeElement SelectedItemStade
        {
            get { return _selectedItemStade; }
            set
            {
                _selectedItemStade = value;
                OnPropertyChanged("SelectedItemStade");
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

        public string StadeImage
        {
            get { return _stade.StadeImage; }
            set
            {
                if (value == _stade.StadeImage) return;
                _stade.StadeImage = value;
                base.OnPropertyChanged("StadeImage");
            }
        }

        public override string ToString()
        {
            return Nom;
            //return Nom + ", " + NbPlaces + " places, arène de type " + string.Join(" ", Types.ToArray());
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
            if (Types.Contains(SelectedItemStade))
            {
                TypesDisponibles.Add(SelectedItemStade);
                Types.Remove(SelectedItemStade);
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
