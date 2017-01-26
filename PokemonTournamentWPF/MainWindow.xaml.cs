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

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PokemonTournamentManager controller;

        public MainWindow()
        {
            InitializeComponent();
            controller = new PokemonTournamentManager();
            btn_pokemons.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void btn_pokemons_Click(object sender, RoutedEventArgs e)
        {

            IList<PokemonTournamentEntities.Pokemon> pokemons = controller.GetAllPokemons();

            ViewModelBase.PokemonsViewModel pvm = new ViewModelBase.PokemonsViewModel(pokemons);
            list_pokemons.DataContext = pvm;

            List<String> list_types = new List<String>();
            list_types.Add("");            
            list_types.AddRange(Enum.GetNames(typeof(TypeElement)));
            combo_filtrage.ItemsSource = list_types;

            list_pokemons.Visibility = Visibility.Visible;
            list_stades.Visibility = Visibility.Collapsed;
            list_matchs.Visibility = Visibility.Collapsed;

            grid_view_pokemons.Visibility = Visibility.Visible;
            grid_view_stades.Visibility = Visibility.Collapsed;
            grid_view_matchs.Visibility = Visibility.Collapsed;
        }

        private void btn_stades_Click(object sender, RoutedEventArgs e)
        {
            IList<PokemonTournamentEntities.Stade> stades = controller.GetAllStades();

            ViewModelBase.StadesViewModel svm = new ViewModelBase.StadesViewModel(stades);
            list_stades.DataContext = svm;
         
            list_pokemons.Visibility = Visibility.Collapsed;
            list_stades.Visibility = Visibility.Visible;
            list_matchs.Visibility = Visibility.Collapsed;

            grid_view_pokemons.Visibility = Visibility.Collapsed;
            grid_view_stades.Visibility = Visibility.Visible;
            grid_view_matchs.Visibility = Visibility.Collapsed;

        }

        private void btn_matchs_Click(object sender, RoutedEventArgs e)
        {
            IList<PokemonTournamentEntities.Match> matchs = controller.GetAllMatchs();

            ViewModelBase.MatchsViewModel mvm = new ViewModelBase.MatchsViewModel(matchs);
            list_matchs.DataContext = mvm;

            list_pokemons.Visibility = Visibility.Collapsed;
            list_stades.Visibility = Visibility.Collapsed;
            list_matchs.Visibility = Visibility.Visible;

            grid_view_pokemons.Visibility = Visibility.Collapsed;
            grid_view_stades.Visibility = Visibility.Collapsed;
            grid_view_matchs.Visibility = Visibility.Visible;
        }

         private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            pdlg.PrintVisual(this.list_pokemons,""); //attention, a modifier : ajouter fenetre de choix d'objet a printer
            pdlg.ShowDialog();
        }
      
        private void button_ok_ajouter_Click(object sender, RoutedEventArgs e)
        { //ce code peut servir pour contrôler l'entrée d'un stade dans la liste
            int nbPlaces;

            if (text_box_nbplaces_stade.Text != "" && text_box_nom_stade.Text != "" 
                && Int32.TryParse(text_box_nbplaces_stade.Text, out nbPlaces))
            {                                                                                  
            } 
            else
            {
                MessageBoxResult result = MessageBox.Show("Veuillez remplir tous les champs.");
            }
        }

        private void button_filtrage_Click(object sender, RoutedEventArgs e)
        {
            String filtre = combo_filtrage.Text;
            if(filtre == "")
            {
                list_stades.ItemsSource = controller.GetAllPokemons();
            }
            else
            {
                TypeElement type = (TypeElement)Enum.Parse(typeof(TypeElement),filtre);
                list_pokemons.ItemsSource = controller.GetAllPokemonsFromType(type);
            }
        }

        private void MenuItem_save_Click(object sender, RoutedEventArgs e)
        {
            XMLSave fenetre = new XMLSave();
            fenetre.Show();
        }
    }
}
