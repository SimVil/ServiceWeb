using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokemonBusinessLayer;
using PokemonTournamentEntities;
using PokemonTournamentWPF.ViewModelBase;

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PokemonTournamentManager controller;

        private PokemonsViewModel pvm;
        private StadesViewModel svm;
        private MatchsViewModel mvm;
        private List<Pokemon> pokemonsSelected;
        private List<Pokemon> pokemons;

        private List<ComboBox> comboBoxes;

        public MainWindow()
        {
            InitializeComponent();
            controller = new PokemonTournamentManager();
            btn_pokemons.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void btn_pokemons_Click(object sender, RoutedEventArgs e)
        {

            IList<PokemonTournamentEntities.Pokemon> pokemons = controller.GetAllPokemons();

            pvm = new ViewModelBase.PokemonsViewModel(pokemons);
            list_pokemons.DataContext = pvm;

            List<String> list_types = new List<String>();
            list_types.Add("");
            list_types.AddRange(Enum.GetNames(typeof(TypeElement)));
            combo_filtrage.ItemsSource = list_types;

            list_pokemons.Visibility = Visibility.Visible;
            list_stades.Visibility = Visibility.Collapsed;
            //list_matchs.Visibility = Visibility.Collapsed;

            grid_view_pokemons.Visibility = Visibility.Visible;
            grid_view_stades.Visibility = Visibility.Collapsed;
            grid_view_matchs.Visibility = Visibility.Collapsed;
            //grid_view_tournoi.Visibility = Visibility.Collapsed;
        }

        private void btn_stades_Click(object sender, RoutedEventArgs e)
        {
            IList<PokemonTournamentEntities.Stade> stades = controller.GetAllStades();
            svm = new ViewModelBase.StadesViewModel(stades);
            list_stades.DataContext = svm;

            list_pokemons.Visibility = Visibility.Collapsed;
            list_stades.Visibility = Visibility.Visible;
            //list_matchs.Visibility = Visibility.Collapsed;

            grid_view_pokemons.Visibility = Visibility.Collapsed;
            grid_view_stades.Visibility = Visibility.Visible;
            grid_view_matchs.Visibility = Visibility.Collapsed;
            //grid_view_tournoi.Visibility = Visibility.Collapsed;

        }

        private void btn_matchs_Click(object sender, RoutedEventArgs e)
        {
            controller = new PokemonTournamentManager();
            pokemons = new List<Pokemon>(controller.GetAllPokemons());
            List<Stade> stades = new List<Stade>(controller.GetAllStades());
            stade_prem_phase.ItemsSource = stades;
            stade_sec_phase.ItemsSource = stades;
            stade_troi_phase.ItemsSource = stades;


            comboBoxes = new List<ComboBox>()
            {combattant1, combattant2, combattant3, combattant4, combattant5,
            combattant6, combattant7, combattant8};

            foreach (ComboBox cb in comboBoxes)
            {
                cb.ItemsSource = pokemons;
            }

            pokemonsSelected = new List<Pokemon>();

            list_pokemons.Visibility = Visibility.Collapsed;
            list_stades.Visibility = Visibility.Collapsed;

            grid_view_pokemons.Visibility = Visibility.Collapsed;
            grid_view_stades.Visibility = Visibility.Collapsed;
            grid_view_matchs.Visibility = Visibility.Visible;
            grid_view_tournoi.Visibility = Visibility.Collapsed;
        }

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            pdlg.PrintVisual(this.list_pokemons, ""); //attention, a modifier : ajouter fenetre de choix d'objet a printer
            pdlg.ShowDialog();
        }

        private void button_filtrage_Click(object sender, RoutedEventArgs e)
        {
            String filtre = combo_filtrage.Text;
            if (filtre == "")
            {
                IList<PokemonTournamentEntities.Pokemon> pokemons = controller.GetAllPokemons();

                pvm = new ViewModelBase.PokemonsViewModel(pokemons);
                list_pokemons.DataContext = pvm;
            }
            else
            {
                TypeElement type = (TypeElement)Enum.Parse(typeof(TypeElement), filtre);
                IList<Pokemon> pokemons = controller.GetAllPokemonsFromType(type);
                pvm = new ViewModelBase.PokemonsViewModel(pokemons);
                list_pokemons.DataContext = pvm;
            }
        }

        private void MenuItem_save_Click(object sender, RoutedEventArgs e)
        {
            XMLSave fenetre = new XMLSave();
            fenetre.Show();
        }

       /* private void btn_tournoi_Click(object sender, RoutedEventArgs e)
        {
            list_pokemons.Visibility = Visibility.Collapsed;
            list_stades.Visibility = Visibility.Visible;           

            grid_view_pokemons.Visibility = Visibility.Collapsed;
            grid_view_stades.Visibility = Visibility.Collapsed;
            grid_view_matchs.Visibility = Visibility.Collapsed;
            grid_view_tournoi.Visibility = Visibility.Visible;
        }*/

        private void combattant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*ComboBox cb = (ComboBox)sender;
            if(cb != null)
            {
                Pokemon p = (Pokemon)cb.SelectedItem;
                if(p != null)
                {
                    
                    if(pokemonsSelected.Count >= 8)
                    {

                    }
                    pokemons.Remove(p);
                    pokemonsSelected.Add(p);
                    
                    for(int i = 0; i < comboBoxes.Count; ++i)
                    {
                        Int32 selectedIndex = comboBoxes.ElementAt(i).SelectedIndex;
                        comboBoxes.ElementAt(i).SelectedIndex = -1;
                        comboBoxes.ElementAt(i).Items.Refresh();
                        comboBoxes.ElementAt(i).SelectedIndex = selectedIndex;
                        ++i;
                    }
                }

                
            }*/

        }

        private void Tournoi_Click(object sender, RoutedEventArgs e)
        {
            //on ne peut lancer le tournoi que si il ya un poekmon unique choisie par combobox
            comboBoxes = new List<ComboBox>() { combattant1, combattant2, combattant3, combattant4, combattant5, combattant6, combattant7, combattant8 };
            bool duplicate = false;
            /*for (int i = 0; i < comboBoxes.Count(); i++)
            {
                for (int j = i + 1; j < comboBoxes.Count(); j++)
                {
                    if (comboBoxes[i] == null || comboBoxes[i].SelectedItem == comboBoxes[j].SelectedItem)
                    {
                        MessageBox.Show("Please choose one unique pokemon per box !");
                        duplicate = true;
                        break;
                    }
                }
                if (duplicate)
                {
                    break;
                }
            }*/
            
            if (!duplicate)
            {
                string content = (sender as Button).Content.ToString();
                if (content == "Tournoi Automatique")
                {
                    //First Duel phase
                    Match m1 = new Match((Pokemon)combattant1.SelectedItem, (Pokemon)combattant2.SelectedItem, 0,PhaseTournoi.QuartFinale, (Stade)stade_prem_phase.SelectedItem);
                    Match m2 = new Match((Pokemon)combattant3.SelectedItem, (Pokemon)combattant4.SelectedItem, 0, PhaseTournoi.QuartFinale, (Stade)stade_prem_phase.SelectedItem);
                    Match m3 = new Match((Pokemon)combattant5.SelectedItem, (Pokemon)combattant6.SelectedItem, 0, PhaseTournoi.QuartFinale, (Stade)stade_prem_phase.SelectedItem);
                    Match m4 = new Match((Pokemon)combattant7.SelectedItem, (Pokemon)combattant8.SelectedItem, 0, PhaseTournoi.QuartFinale, (Stade)stade_prem_phase.SelectedItem);

                    Pokemon winner1 = m1.Duel();
                    vainqueur1.Content = winner1;
                    Pokemon winner2 = m2.Duel();
                    vainqueur4.Content = winner2;
                    Pokemon winner3 = m3.Duel();
                    vainqueur2.Content = winner3;
                    Pokemon winner4 = m4.Duel();
                    vainqueur3.Content = winner4;

                    //Healing phase
                    if(winner1 != null)
                    {
                        winner1.Heal();
                    }
                    if (winner2 != null)
                    {
                        winner2.Heal();
                    }
                    if (winner3 != null)
                    {
                        winner3.Heal();
                    }
                    if (winner4 != null)
                    {
                        winner4.Heal();
                    }
                   
                    Match m5 = new Match(winner1, winner2, 0, PhaseTournoi.DemiFinale, (Stade)stade_sec_phase.SelectedItem);
                    Match m6 = new Match(winner3, winner4, 0, PhaseTournoi.DemiFinale, (Stade)stade_sec_phase.SelectedItem);

                    winner1 = m5.Duel();
                    vainqueur5.Content = winner1;
                    winner2 = m6.Duel();
                    vainqueur6.Content = winner2;

                    //healing phase
                    if (winner1 != null)
                    {
                        winner1.Heal();
                    }
                    if (winner2 != null)
                    {
                        winner2.Heal();
                    }

                    //Final
                    Match m7 = new Match(winner1, winner2, 0, PhaseTournoi.Finale, (Stade)stade_troi_phase.SelectedItem);
                    winner1 = m7.Duel();
                    vainqueur7.Content = winner1;
                }
                else
                {
                    MessageBox.Show("Tournoi Jouable");
                }
            }

        }
    }
    /*
    private void lancer_duel_Click(object sender, RoutedEventArgs e)
    {
        PokemonViewModel pokemon1 = (PokemonViewModel)list_pokemons1_tournament.SelectedItem;

        PokemonViewModel pokemon2 = (PokemonViewModel)list_pokemons2_tournament.SelectedItem;

        if (pokemon1 != null && pokemon2 != null)
        {
            if (pokemon1.Vie == 0 || pokemon2.Vie == 0)
            {
                if (pokemon1.Vie == 0 && pokemon2.Vie == 0)
                {
                    MessageBox.Show("Both Pokemons has fainted !");
                }

                else
                {
                    if (pokemon1.Vie == 0 && pokemon2.Vie != 0)
                    {
                        MessageBox.Show(pokemon1.Nom + " has fainted !");
                    }
                    else
                    {
                        MessageBox.Show(pokemon2.Nom + " has fainted !");
                    }
                }
            }

            else
            {
                Pokemon _pokemon1 = pokemon1.Pokemon;
                Pokemon _pokemon2 = pokemon2.Pokemon;
                //determiner qui va commencer
                Pokemon first = Pokemon.Begins(_pokemon1, _pokemon2);
                Pokemon second = null;
                if (_pokemon1 == first)
                {
                    second = _pokemon2;
                }
                else
                {
                    second = _pokemon1;
                }

                //Booster les pokemon selon les stades
                StadeViewModel chosenstadium = (StadeViewModel)list_stades.SelectedItem;
                if (chosenstadium != null)
                {
                    Stade _chosenstadium = chosenstadium.Stade;
                    first.Boost(_chosenstadium);
                    second.Boost(_chosenstadium);
                }

                //lancer le combat
                while (first.Vie > 0 && second.Vie > 0)
                {
                    first.Attaquer(second);
                    if (second.Vie > 0)
                    {
                        second.Attaquer(first);
                    }
                    MessageBox.Show(first.Nom + " has :" + first.Vie.ToString() + " HP left & " + second.Nom + " has : " + second.Vie.ToString() + " HP left");

                }

                //anoncer le vainqueur
                if (first.Vie == 0 && second.Vie > 0)
                {
                    MessageBox.Show("The winner is : " + second.Nom);

                }
                else
                {
                    if (second.Vie == 0 && first.Vie > 0)
                    {
                        MessageBox.Show("The winner is : " + first.Nom);
                    }
                    else
                    {
                        MessageBox.Show("Draw !");
                    }

                }
            }
        }
    }*/
}

