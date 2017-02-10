using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Pokemon pokemonAdverse;
        private static ModeJouable modeJouable;

        private int nombreAdversaireRestants;
        private int nombreTours;

        List<Pokemon> Pokemons;

        private ModeJouable(List<Pokemon> pokemons)
        {
            InitializeComponent();
            list_box_pokemons.ItemsSource = pokemons;

            Pokemons = pokemons;
            nombreAdversaireRestants = pokemons.Count;
            nombreTours = 1;
        }

        /* ouvrir une nouvelle fenetre :
         * pour choisir son pokemon et jouer les combat */
        public static ModeJouable getInstance(List<Pokemon> pokemons)
        {
            if(modeJouable == null)
            {
                modeJouable = new ModeJouable(pokemons);
            }
            return modeJouable;
        }

        // confirmer la selection de son pokemon
        private void confirmation_selection_button_Click(object sender, RoutedEventArgs e)
        {
            pokemonJoue = (Pokemon)list_box_pokemons.SelectedItem;
            if(pokemonJoue != null)
            {
                list_box_pokemons.Visibility = Visibility.Collapsed;
                confirmation_selection_button.Visibility = Visibility.Collapsed;

                textBlock.Visibility = Visibility.Visible;
                attaques.Visibility = Visibility.Visible;
                nom_pokemon_selected.Visibility = Visibility.Visible;
                nom_pokemon_selected.Content = pokemonJoue.Nom;

                if(pokemonJoue.PokeImage != null)
                {
                    image_pokemon_selected.Source = new BitmapImage(new Uri(pokemonJoue.PokeImage));
                }

                //récupérer pokemon adversaire
                int indexjoue = Pokemons.IndexOf(pokemonJoue);
                int indexAdverse = (indexjoue + nombreTours) % Pokemons.Count;

                pokemonAdverse = Pokemons.ElementAt(indexAdverse);
                nom_adversaire.Content = pokemonAdverse.Nom;

                if (pokemonJoue.PokeImage != null)
                {
                    image_adversaire.Source = new BitmapImage(new Uri(pokemonAdverse.PokeImage));
                }
            }
        }

        /* mode jouable de type : feueille, ciseau, roche
         * le gagnant inflige des degats à l'ennemi
         * si le joueur gagne il passe au porchain combat, sinon le tournoi
         * se completera de facon automatique */
        private void attaque_selection_button_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
            List<string> attaquesAdverses = new List<string>()
            {"Tranche Herbe","Déflagration","Hydrocanon"};

            Random attaqueAdv = new Random();
            int valeurAttaqueAdversaire = attaqueAdv.Next(0, 3);

            string content = (sender as Button).Content.ToString();
            if(content == "Tranche Herbe")
            {
                if(attaquesAdverses.ElementAt(valeurAttaqueAdversaire)== "Déflagration")
                {
                    perdreVie(attaquesAdverses.ElementAt(valeurAttaqueAdversaire));                    
                }
                else
                {
                    if (attaquesAdverses.ElementAt(valeurAttaqueAdversaire) == "Hydrocanon")
                    {
                        infligerDegats(attaquesAdverses.ElementAt(valeurAttaqueAdversaire));
                    }
                    else
                    {
                        textBlock.Text = "L'adversaire a contré\n";
                    }
                }
            }
            else
            {
                if(content == "Déflagration")
                {
                    if (attaquesAdverses.ElementAt(valeurAttaqueAdversaire) == "Hydrocanon")
                    {
                        perdreVie(attaquesAdverses.ElementAt(valeurAttaqueAdversaire));
                    }
                    else
                    {
                        if (attaquesAdverses.ElementAt(valeurAttaqueAdversaire) == "Tranche Herbe")
                        {
                            infligerDegats(attaquesAdverses.ElementAt(valeurAttaqueAdversaire));
                        }
                        else
                        {
                            textBlock.Text = "L'adversaire a contré\n";
                        }
                    }
                }
                else
                {
                    if(content == "Hydrocanon")
                    {
                        if (attaquesAdverses.ElementAt(valeurAttaqueAdversaire) == "Tranche Herbe")
                        {
                            perdreVie(attaquesAdverses.ElementAt(valeurAttaqueAdversaire));
                        }
                        else
                        {
                            if (attaquesAdverses.ElementAt(valeurAttaqueAdversaire) == "Déflagration")
                            {
                                infligerDegats(attaquesAdverses.ElementAt(valeurAttaqueAdversaire));
                            }
                            else
                            {
                                textBlock.Text = "L'adversaire a contré\n";
                            }
                        }
                    }
                }
            }
        }

        private void perdreVie(string attaque)
        {
            textBlock.Text += "L'adversaire a attaqué " + attaque +"\n";
            pokemonAdverse.Attaquer(pokemonJoue);
            textBlock.Text += "Votre vie : " + pokemonJoue.Vie+"\n";
            textBlock.Text += "La vie de votre adversaire : " + pokemonAdverse.Vie +"\n";
            if (pokemonJoue.Vie <= 0)
            {
                MessageBox.Show("L'adversaire a attaqué " + attaque + ".Vous avez perdu");
                this.Close();
            }
        }

        private void infligerDegats(string attaque)
        {
            textBlock.Text += "L'adversaire a attaqué " + attaque+ "\n";
            pokemonJoue.Attaquer(pokemonAdverse);
            textBlock.Text += "Votre vie : " + pokemonJoue.Vie+"\n";
            textBlock.Text += "La vie de votre adversaire : " + pokemonAdverse.Vie +"\n";
            if (pokemonAdverse.Vie <= 0)
            {
                MessageBox.Show("L'adversaire a attaqué " + attaque + ".Vous avez gagné");
                nombreAdversaireRestants /= 2;
                if(nombreAdversaireRestants <= 1)
                {
                    MessageBox.Show("L'adversaire a attaqué " + attaque + ".Vous avez gagné le tournoi, félicitations");
                    this.Close();
                }
                else
                {
                    selectionnerNouvelAdversaire();
                }
                
            }
        }

        private void selectionnerNouvelAdversaire()
        {
            textBlock.Text = "";
            pokemonJoue.Heal();

            int indexjoue = Pokemons.IndexOf(pokemonJoue);
            int indexAdverse = (indexjoue + ++nombreTours) % Pokemons.Count;

            pokemonAdverse = Pokemons.ElementAt(indexAdverse);
            nom_adversaire.Content = pokemonAdverse.Nom;
            image_adversaire.Source = new BitmapImage(new Uri(pokemonAdverse.PokeImage));
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            modeJouable = null;

            base.OnClosing(e);
        }
    }
}
