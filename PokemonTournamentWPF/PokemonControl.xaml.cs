using Microsoft.Win32;
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

        /*  permet la modification du pokemon :
         *  donner un nom, type vie etc... au pokemon
         *  selectionnée                            */
        private void Button_Click_Modifier(object sender, RoutedEventArgs e)
        {
            if(is_modif)
            {
                button_modification.Content = "Modifier";
                pokemon_force.Visibility = Visibility.Collapsed;
                pokemon_force_label.Visibility = Visibility.Visible;

                pokemon_def.Visibility = Visibility.Collapsed;
                pokemon_def_label.Visibility = Visibility.Visible;

                pokemon_vie.Visibility = Visibility.Collapsed;
                pokemon_vie_label.Visibility = Visibility.Visible;

                pokemon_name.Visibility = Visibility.Collapsed;
                pokemon_name_label.Visibility = Visibility.Visible;

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
                pokemon_force.Visibility = Visibility.Visible;
                pokemon_force_label.Visibility = Visibility.Collapsed;

                pokemon_def.Visibility = Visibility.Visible;
                pokemon_def_label.Visibility = Visibility.Collapsed;

                pokemon_vie.Visibility = Visibility.Visible;
                pokemon_vie_label.Visibility = Visibility.Collapsed;

                pokemon_name.Visibility = Visibility.Visible;
                pokemon_name_label.Visibility = Visibility.Collapsed;


                label_types_disponibles.Visibility = Visibility.Visible;
                list_types_disponibles.Visibility = Visibility.Visible;
                
                button_ajout_type.Visibility = Visibility.Visible;
                button_remove_type.Visibility = Visibility.Visible;
                

                is_modif = true;
            }
            
        }

    
    }
}
