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
        private IList<PokemonTournamentEntities.TypeElement> types;

        public PokemonControl()
        {
            controller = new PokemonTournamentManager();
            InitializeComponent();
            types = controller.GetAllTypes();
            ViewModelBase.TypesViewModel pvm = new ViewModelBase.TypesViewModel(types);
            //comboBox_types.DataContext = pvm;
        }

        private void button_ajouter_type_Click(object sender, RoutedEventArgs e)
        {
            /* bool fin = false;

             ViewModelBase.TypeViewModel t = (ViewModelBase.TypeViewModel)comboBox_types.SelectedItem;
             if(t != null)
             {
                 int length = types.Count;
                 int i = 0;
                 while (i < length && !fin)
                 {
                     if (types.ElementAt(i).ToString() == t.ToString())
                     {
                         fin = true;
                         //pokemon_type.Items.Add(types.ElementAt(i));
                         List<TypeElement> p = (List<TypeElement>)pokemon_type.ItemsSource;
                         p.Add(types.ElementAt(i));
                         pokemon_type.DataContext = p;                     
                     }
                     ++i;
                 }
             }  */          
       
        }
    }
}
