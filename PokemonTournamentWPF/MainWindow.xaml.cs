﻿using System;
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
using PokemonTournamentWPF.ViewModelBase;

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
            grid_view_tournoi.Visibility = Visibility.Collapsed;
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
            grid_view_tournoi.Visibility = Visibility.Collapsed;

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
            grid_view_tournoi.Visibility = Visibility.Collapsed;
        }

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            pdlg.PrintVisual(this.list_pokemons, ""); //attention, a modifier : ajouter fenetre de choix d'objet a printer
            pdlg.ShowDialog();
        }

        private void button_filtrage_Click(object sender, RoutedEventArgs e)
        {
            String filtre = combo_filtrage.Text;
            if (filtre == "")
            {
                list_stades.ItemsSource = controller.GetAllPokemons();
            }
            else
            {
                TypeElement type = (TypeElement)Enum.Parse(typeof(TypeElement), filtre);
                list_pokemons.ItemsSource = controller.GetAllPokemonsFromType(type);
            }
        }

        private void MenuItem_save_Click(object sender, RoutedEventArgs e)
        {
            XMLSave fenetre = new XMLSave();
            fenetre.Show();
        }

        private void btn_duel_Click(object sender, RoutedEventArgs e)
        {
            IList<PokemonTournamentEntities.Pokemon> pokemons = controller.GetAllPokemons();
            IList<PokemonTournamentEntities.Stade> stades = controller.GetAllStades();
            
            ViewModelBase.PokemonsViewModel pt1vm = new ViewModelBase.PokemonsViewModel(pokemons);
            list_pokemons1_tournament.DataContext = pt1vm;

            ViewModelBase.PokemonsViewModel pt2vm = new ViewModelBase.PokemonsViewModel(pokemons);
            list_pokemons2_tournament.DataContext = pt2vm;

            ViewModelBase.StadesViewModel svm = new ViewModelBase.StadesViewModel(stades);
            list_stades.DataContext = svm;

            list_pokemons.Visibility = Visibility.Collapsed;
            list_stades.Visibility = Visibility.Visible;
            list_matchs.Visibility = Visibility.Collapsed;

            grid_view_pokemons.Visibility = Visibility.Collapsed;
            grid_view_stades.Visibility = Visibility.Collapsed;
            grid_view_matchs.Visibility = Visibility.Collapsed;
            grid_view_tournoi.Visibility = Visibility.Visible;
        }

        private void lancer_duel_Click(object sender, RoutedEventArgs e)
        {
            PokemonViewModel pokemon1 = (PokemonViewModel)list_pokemons1_tournament.SelectedItem;
                       
            PokemonViewModel pokemon2 = (PokemonViewModel)list_pokemons2_tournament.SelectedItem;

            if (pokemon1 != null && pokemon2 != null)
            {
                if (pokemon1.Vie == 0 || pokemon2.Vie == 0)
                {
                    if (pokemon1.Vie == 0 && pokemon2.Vie == 0)
                    {
                        MessageBox.Show("Both Pokemons has fainted !");
                    }

                    else
                    {
                        if (pokemon1.Vie == 0 && pokemon2.Vie != 0)
                        {
                            MessageBox.Show(pokemon1.Nom + " has fainted !");
                        }
                        else
                        {
                            MessageBox.Show(pokemon2.Nom + " has fainted !");
                        }
                    }
                }

                else
                {
                    Pokemon _pokemon1 = pokemon1.Pokemon;
                    Pokemon _pokemon2 = pokemon2.Pokemon;
                    //determiner qui va commencer
                    Pokemon first = Pokemon.Begins(_pokemon1, _pokemon2);
                    Pokemon second = null;
                    if (_pokemon1 == first)
                    {
                        second = _pokemon2;
                    }
                    else
                    {
                        second = _pokemon1;
                    }

                    //Booster les pokemon selon les stades
                    StadeViewModel chosenstadium = (StadeViewModel)list_stades.SelectedItem;
                    if (chosenstadium != null)
                    {
                        Stade _chosenstadium = chosenstadium.Stade;
                        first.Boost(_chosenstadium);
                        second.Boost(_chosenstadium);
                    }

                    //lancer le combat
                    while (first.Vie > 0 && second.Vie > 0)
                    {
                        first.Attaquer(second);
                        if (second.Vie > 0)
                        {
                            second.Attaquer(first);
                        }
                        MessageBox.Show(first.Nom + " has :" + first.Vie.ToString() + " HP left & " + second.Nom + " has : " + second.Vie.ToString() + " HP left");

                    }

                    //anoncer le vainqueur
                    if (first.Vie == 0 && second.Vie > 0)
                    {
                        MessageBox.Show("The winner is : " + second.Nom);

                    }
                    else
                    {
                        if (second.Vie == 0 && first.Vie > 0)
                        {
                            MessageBox.Show("The winner is : " + first.Nom);
                        }
                        else
                        {
                            MessageBox.Show("Draw !");
                        }

                    }
                }
            }
        }
    }
}
