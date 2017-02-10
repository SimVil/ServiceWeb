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
using System.Windows.Shapes;

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Logique d'interaction pour ModeJouable.xaml
    /// </summary>
    public partial class ModeJouable : Window
    {
        private Pokemon pokemonJoue;
        private static ModeJouable modeJouable;

        private ModeJouable(List<Pokemon> pokemons)
        {
            InitializeComponent();
            list_box_pokemons.ItemsSource = pokemons;
        }

        public static ModeJouable getInstance(List<Pokemon> pokemons)
        {
            if(modeJouable == null)
            {
                modeJouable = new ModeJouable(pokemons);
            }
            return modeJouable;
        }

        private void confirmation_selection_button_Click(object sender, RoutedEventArgs e)
        {
            pokemonJoue = (Pokemon)list_box_pokemons.SelectedItem;
            if(pokemonJoue != null)
            {
                list_box_pokemons.Visibility = Visibility.Collapsed;
                confirmation_selection_button.Visibility = Visibility.Collapsed;
                attaques.Visibility = Visibility.Visible;
                nom_pokemon_selected.Visibility = Visibility.Visible;
                nom_pokemon_selected.Content = pokemonJoue.Nom;
                image_pokemon_selected.Source = new BitmapImage(new Uri(pokemonJoue.PokeImage));
            }
        }
    }
}
