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
using PokemonBusinessLayer;

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_connexion_Click(object sender, RoutedEventArgs e)
        {
            PokemonTournamentManager controller = new PokemonTournamentManager();
            if(controller.getUtilisateurByLogin(textbox_login.Text) != null)
            {
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login ou mot de passe incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
