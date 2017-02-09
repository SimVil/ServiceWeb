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
    /// Interaction logic for StadeControl.xaml
    /// </summary>
    public partial class StadeControl : UserControl
    {
        private bool is_modif;

        public StadeControl()
        {
            InitializeComponent();

            is_modif = true;
            Button_Click_Modifier(null, null);
        }

        private void Button_Click_Modifier(object sender, RoutedEventArgs e)
        {
            if (is_modif)
            {
                button_modification.Content = "Modifier";
                stade_name.Visibility = Visibility.Collapsed;
                stade_name_label.Visibility = Visibility.Visible;

                stade_places.Visibility = Visibility.Collapsed;
                stade_places_label.Visibility = Visibility.Visible;             

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

                stade_name.Visibility = Visibility.Visible;
                stade_name_label.Visibility = Visibility.Collapsed;

                stade_places.Visibility = Visibility.Visible;
                stade_places_label.Visibility = Visibility.Collapsed;


                label_types_disponibles.Visibility = Visibility.Visible;
                list_types_disponibles.Visibility = Visibility.Visible;

                button_ajout_type.Visibility = Visibility.Visible;
                button_remove_type.Visibility = Visibility.Visible;

                is_modif = true;
            }

        }
    }
}
