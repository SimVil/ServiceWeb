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
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using PokemonBusinessLayer;
using PokemonTournamentEntities;

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class XMLSave : Window
    {
        public XMLSave()
        {
            InitializeComponent();
        }

        private void Button_fileDialog_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                text_directoryPath.Text = dialog.SelectedPath;
            }
        }

        private void Button_save_Click(object sender, RoutedEventArgs e)
        {
            String filename = textbox_filename.Text,
                   path = text_directoryPath.Text,
                   fullname;

            if(filename == "")
            {
                filename = "save_xml.xml";
            }
            else
            {
                filename += ".xml";
            }
            
            if (path != "")
            {
                fullname = path + "\\" + filename;
                StreamWriter stream = new StreamWriter(fullname);

                XmlSerializer serializer = new XmlSerializer(typeof(List<Pokemon>));
                serializer.Serialize(stream, new PokemonTournamentManager().GetAllPokemons());

                stream.Close();
                System.Windows.Forms.MessageBox.Show("Sauvegarde effectuée");
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Il faut indiquer un dossier !");
            }
            
        }
    }
}
