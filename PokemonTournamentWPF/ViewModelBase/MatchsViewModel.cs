using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//INUTILE
namespace PokemonTournamentWPF.ViewModelBase
{
    class MatchsViewModel : ViewModelBase
    {
        // Event destiné à réclamer la fermeture du conteneur;
        public event EventHandler<EventArgs> CloseNotified;
        protected void OnCloseNotified(EventArgs e)
        {
            this.CloseNotified(this, e);
        }

        // Model encapsulé dans le ViewModel
        private ObservableCollection<MatchViewModel> _matchs;

        public ObservableCollection<MatchViewModel> Matchs
        {
            get { return _matchs; }
            private set
            {
                _matchs = value;
                OnPropertyChanged("Matchs");
            }
        }

        private MatchViewModel _selectedItem;
        public MatchViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public MatchsViewModel(IList<PokemonTournamentEntities.Match> matchsModel)
        {
            /*_matchs = new ObservableCollection<MatchViewModel>();
            foreach (PokemonTournamentEntities.Match a in matchsModel)
            {
                _matchs.Add(new MatchViewModel(a));
            }*/
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
             /* PokemonTournamentEntities.Match a = new PokemonTournamentEntities.Match(null, null, 0, PokemonTournamentEntities.PhaseTournoi.HuitiemeFinale, null);

              this.SelectedItem = new MatchViewModel(a);
              Matchs.Add(this.SelectedItem);
              */
            
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
          //  if (this.SelectedItem != null) Matchs.Remove(this.SelectedItem);
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
