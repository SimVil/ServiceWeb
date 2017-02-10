using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
        //initialisation du controlleur ,des viewmodels et des listes
        private PokemonTournamentManager controller;
        private PokemonsViewModel pvm;
        private StadesViewModel svm;
        private List<Pokemon> pokemonsSelected;
        private List<Pokemon> pokemons;
        private List<ComboBox> comboBoxes;


        public MainWindow()
        {
            InitializeComponent();
            controller = new PokemonTournamentManager();
            btn_pokemons.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        /*bouton qui : * remplie la liste des pokemons,
                       * cache les vues stades et matchs,
                       * met la vue des pokemon visible             */
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

        /*bouton qui : * remplie la liste des stades,
                       * cache les vues stades et pokemons,
                       * met la vue des stade visible             */
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

        /*bouton qui : * met la vue de matchs visible
                       * propose à l'utilisateur d'organiser le tournoi
                       * propose à l'utilisateur de lancer le tournoi en deux modes
                                                            */
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

        }


        /*bouton qui : imprime la liste des pokemons et stades
                                                            */
        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            pdlg.PrintVisual(this.list_pokemons, ""); //attention, a modifier : ajouter fenetre de choix d'objet a printer
            pdlg.ShowDialog();
        }


        /* bouton qui filtre les pokemons selon leurs types */
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


        private void combattant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        /* fonction qui selon le bouton clické :
         *  lance le tournoi automatique (1er choix)
         *  lance un mode jouable ou le joueur chosis son pokemon et participe au tournoi
         */
        private void Tournoi_Click(object sender, RoutedEventArgs e)
        {
            //on ne peut lancer le tournoi que si il ya un poekmon unique choisie par combobox
            comboBoxes = new List<ComboBox>() { combattant1, combattant2, combattant3, combattant4, combattant5, combattant6, combattant7, combattant8 };

            string content = (sender as Button).Content.ToString();
            //mode automatique
            if (content == "Tournoi Automatique")
            {
                //First Duel phase
                Match m1 = new Match((Pokemon)combattant1.SelectedItem, (Pokemon)combattant2.SelectedItem, 0, PhaseTournoi.QuartFinale, (Stade)stade_prem_phase.SelectedItem);
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
                if (winner1 != null)
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
                //mode jouable
                if (content == "Tournoi Jouable")
                {
                    List<Pokemon> poke = new List<Pokemon>();
                    foreach (ComboBox cb in comboBoxes)
                    {
                        Pokemon p = (Pokemon)cb.SelectedItem;
                        if (p != null)
                        {
                            poke.Add(p);
                        }
                    }
                    ModeJouable mj = ModeJouable.getInstance(poke);
                    mj.Show();
                }
            }

        }
        /*bouton qui : * permet d'importer une image
                       * la lié à un pokemon
                                                    */
        private void button_import_picture_Click(object sender, RoutedEventArgs e)
        {
            if (list_pokemons.SelectedItem != null)
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {

                    PokemonViewModel pokemon = (PokemonViewModel)list_pokemons.SelectedItem;
                    pokemon.PokeImage = op.FileName;

                }
            }
            else
            {
                //si aucun pokemon n'est selectionné
                MessageBox.Show("Choisi un Pokemon d'abord !");
            }



        }

        private void sauvegarder_pokemon_Click(object sender, RoutedEventArgs e)
        {
            //envoyer infos à la base de donnée
            List<Pokemon> pokemons = controller.GetAllPokemons();
            PokemonViewModel p = (PokemonViewModel)list_pokemons.SelectedItem;
            Pokemon _p = p.Pokemon;

            pokemons.Add(_p);
        }
    }

}

