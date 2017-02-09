using PokemonBusinessLayer;
using PokemonTournamentEntities;
using PokemonTournamentWPF.ViewModelBase;
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
    /// Interaction logic for MatchControlxaml.xaml
    /// </summary>
    public partial class MatchControl : UserControl
    {
        private PokemonTournamentManager controller;
        private List<Pokemon> pokemonsSelected;
        private List<Pokemon> pokemons;

        private List<ComboBox> comboBoxes;

        public MatchControl()
        {
            InitializeComponent();
            controller = new PokemonTournamentManager();
            pokemons = new List<Pokemon>(controller.GetAllPokemons());

            comboBoxes = new List<ComboBox>()
            {combattant1, combattant2, combattant3, combattant4, combattant5,
            combattant6, combattant7, combattant8};

            foreach(ComboBox cb in comboBoxes)
            {
                cb.ItemsSource = pokemons;
            }
            
            pokemonsSelected = new List<Pokemon>();
        }

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
            ChoixTournoi ct = new ChoixTournoi();
            ct.Show();
        }
    }
}
