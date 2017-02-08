using PokemonBusinessLayer;
using PokemonTournamentEntities;
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

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Interaction logic for PokemonControl.xaml
    /// </summary>
    public partial class PokemonControl : UserControl
    {
        private PokemonTournamentManager controller;

        private bool is_modif;

        public PokemonControl()
        {
            controller = new PokemonTournamentManager();
            InitializeComponent();

            is_modif = true;
            Button_Click_Modifier(null, null);
        }

        private void Button_Click_Modifier(object sender, RoutedEventArgs e)
        {
            if(is_modif)
            {
                button_modification.Content = "Modifier";
                pokemon_force.IsEnabled = false;
                pokemon_def.IsEnabled = false;
                pokemon_vie.IsEnabled = false;
                pokemon_name.IsEnabled = false;

                label_types_disponibles.Visibility = Visibility.Collapsed;
                list_types_disponibles.Visibility = Visibility.Collapsed;
                list_types_pokemon.Visibility = Visibility.Visible;

                button_ajout_type.Visibility = Visibility.Collapsed;
                button_remove_type.Visibility = Visibility.Collapsed;

                is_modif = false;
            }
            else
            {
                button_modification.Content = "Terminé";
                pokemon_force.IsEnabled = true;
                pokemon_def.IsEnabled = true;
                pokemon_vie.IsEnabled = true;
                pokemon_name.IsEnabled = true;


                label_types_disponibles.Visibility = Visibility.Visible;
                list_types_disponibles.Visibility = Visibility.Visible;
                
                button_ajout_type.Visibility = Visibility.Visible;
                button_remove_type.Visibility = Visibility.Visible;

                is_modif = true;
            }
            
        }
    }
}
