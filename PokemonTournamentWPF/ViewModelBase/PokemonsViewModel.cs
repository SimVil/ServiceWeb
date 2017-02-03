﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentWPF.ViewModelBase
{
    class PokemonsViewModel : ViewModelBase
    {
        // Event destiné à réclamer la fermeture du conteneur;
        public event EventHandler<EventArgs> CloseNotified;
        protected void OnCloseNotified(EventArgs e)
        {
            this.CloseNotified(this, e);
        }

        // Model encapsulé dans le ViewModel
        private ObservableCollection<PokemonViewModel> _pokemons;

        public ObservableCollection<PokemonViewModel> Pokemons
        {
            get { return _pokemons; }
            private set
            {
                _pokemons = value;
                OnPropertyChanged("Pokemons");
            }
        }

        private PokemonViewModel _selectedItem;
        public PokemonViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public PokemonsViewModel(IList<PokemonTournamentEntities.Pokemon> pokemonsModel)
        {
            _pokemons = new ObservableCollection<PokemonViewModel>();
            foreach (PokemonTournamentEntities.Pokemon a in pokemonsModel)
            {
                _pokemons.Add(new PokemonViewModel(a));
            }
        }

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
            PokemonTournamentEntities.Pokemon a = new PokemonTournamentEntities.Pokemon("<Nom>", 0, 0, 0, null);

            this.SelectedItem = new PokemonViewModel(a);
            Pokemons.Add(this.SelectedItem);
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
            return (this.SelectedItem != null);
        }

        private void Remove()
        {
            if (this.SelectedItem != null) Pokemons.Remove(this.SelectedItem);
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
