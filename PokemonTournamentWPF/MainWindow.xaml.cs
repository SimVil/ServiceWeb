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
            list_informations.ItemsSource = controller.GetAllPokemons();
            List<String> list_types = new List<String>();
            list_types.Add("");            
            list_types.AddRange(Enum.GetNames(typeof(TypeElement)));
            combo_filtrage.ItemsSource = list_types;

            grid_view_pokemons.Visibility = Visibility.Visible;
            grid_view_stades.Visibility = Visibility.Collapsed;
            grid_view_matchs.Visibility = Visibility.Collapsed;
        }

        private void btn_stades_Click(object sender, RoutedEventArgs e)
        {

            list_informations.ItemsSource = controller.GetAllStades();
            grid_view_pokemons.Visibility = Visibility.Collapsed;
            grid_view_stades.Visibility = Visibility.Visible;
            grid_view_matchs.Visibility = Visibility.Collapsed;

        }

        private void btn_matchs_Click(object sender, RoutedEventArgs e)
        {
            list_informations.ItemsSource = controller.GetAllMatchs();
            grid_view_pokemons.Visibility = Visibility.Collapsed;
            grid_view_stades.Visibility = Visibility.Collapsed;
            grid_view_matchs.Visibility = Visibility.Visible;
        }

         private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            pdlg.PrintVisual(this.list_informations,"");
            pdlg.ShowDialog();
        }


        private void  button_ajouter_Click(object sender, RoutedEventArgs e)
        {
            wrap_panel_stade_ajout.Visibility = Visibility.Visible;
            wrap_panel_stade_ajout2.Visibility = Visibility.Visible;
            button_ok_stade.Visibility = Visibility.Visible;

            list_informations.SelectedItem = null;
        }

        private void button_supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (list_informations.SelectedItem != null)
            {
                Stade stade = (Stade)list_informations.SelectedItem;

                controller.GetAllStades().Remove(stade);
                list_informations.Items.Refresh();
            }
            else
            {
                MessageBoxResult result =  MessageBox.Show("Veuillez sélectionner un élément à supprimer.");
            }
           

        }




        private void list_informations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if(list_informations.SelectedItem != null)
           {
               if (list_informations.SelectedItem.GetType().ToString() == "PokemonTournamentEntities.Stade")
               {
                   wrap_panel_stade_ajout.Visibility = Visibility.Visible;
                   wrap_panel_stade_ajout2.Visibility = Visibility.Visible;
                   button_ok_stade.Visibility = Visibility.Visible;

                   Stade stade = (Stade)list_informations.SelectedItem;

                   if (stade != null)
                   {
                       text_box_nom_stade.Text = stade.Nom;
                       text_box_nbplaces_stade.Text = stade.NbPlaces.ToString();
                   }
               }            
           }          
        }


        private void button_ok_ajouter_Click(object sender, RoutedEventArgs e)
        {
            if(text_box_nbplaces_stade.Text != "" && text_box_nom_stade.Text != "")
            {
                int nbPlaces;
                bool is_int = Int32.TryParse(text_box_nbplaces_stade.Text, out nbPlaces);
            
                Stade stade = new Stade(text_box_nom_stade.Text, nbPlaces, null);
                if (list_informations.SelectedItem == null)
                {
                    controller.GetAllStades().Add(stade);
                }
                else
                {
                    Stade existant = (Stade) list_informations.SelectedItem;
                    controller.GetAllStades().Remove(existant);
                    controller.GetAllStades().Add(stade);
                }
               
                list_informations.Items.Refresh();

                text_box_nom_stade.Text = "";
                text_box_nbplaces_stade.Text = "";               
            } 
            else
            {
                MessageBoxResult result = MessageBox.Show("Veuillez remplir tous les champs.");
            }
            wrap_panel_stade_ajout.Visibility = Visibility.Collapsed;
            wrap_panel_stade_ajout2.Visibility = Visibility.Collapsed;
            button_ok_stade.Visibility = Visibility.Collapsed;
        }

        private void button_filtrage_Click(object sender, RoutedEventArgs e)
        {
            String filtre = combo_filtrage.Text;
            if(filtre == "")
            {
                list_informations.ItemsSource = controller.GetAllPokemons();
            }
            else
            {
                TypeElement type = (TypeElement)Enum.Parse(typeof(TypeElement),filtre);
                list_informations.ItemsSource = controller.GetAllPokemonsFromType(type);
            }
        }

        private void MenuItem_save_Click(object sender, RoutedEventArgs e)
        {
            XMLSave fenetre = new XMLSave();
            fenetre.Show();
        }

        /*private void clearGridView()
        {
            grid_view.Children.Clear();
            grid_view.RowDefinitions.Clear();
            grid_view.ColumnDefinitions.Clear();
        }

        private void createGridView(int rows, int columns)
        {
            int i;
            for(i = 0; i < rows; ++i)
            {
                grid_view.RowDefinitions.Add(new RowDefinition());
            }
            for (i = 0; i < columns; ++i)
            {
                grid_view.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }*/
    }
}
