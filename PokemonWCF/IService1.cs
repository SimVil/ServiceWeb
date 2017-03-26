using PokemonDataAccessLayer;
using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PokemonWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        List<Pokemon> getAllPokemons();

        [OperationContract]
        List<Stade> getAllStades();

        [OperationContract]
        List<Match> getAllMatchs();

        [OperationContract]
        void addPokemon(Pokemon pokemon);

        [OperationContract]
        void addStade(Stade stade);

        [OperationContract]
        void addMatch(Match match);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.

    [DataContract]
    public class Pokemon
    {

        private String nom;
        private int vie;
        private int force;
        private int defense;
        private int id;
        private List<TypeElement> types;

        [DataMember]
        public List<TypeElement> Types
        {
            get { return types; }
            set { types = value; }
        }

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public String Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        [DataMember]
        public int Vie
        {
            get { return vie; }
            set { vie = value; }
        }

        [DataMember]
        public int Force
        {
            get { return force; }
            set { force = value; }
        }

        [DataMember]
        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }


    }

    [DataContract]
    public class Stade
    {

        private String nom;
        private int nbplaces;
        private List<TypeElement> types;

        [DataMember]
        public List<TypeElement> Types
        {
            get { return types; }
            set { types = value; }
        }

        [DataMember]
        public String Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        [DataMember]
        public int NbPlaces
        {
            get { return nbplaces; }
            set { nbplaces = value; }
        }
    }


    [DataContract]
    public class Match 
    {
        private Pokemon pokemon1;
        private Pokemon pokemon2;
        private Stade stadepokemon;
        private int id;


        [DataMember]
        public Pokemon Pokemon1
        {
            get { return pokemon1; }
            set { pokemon1 = value; }
        }

        [DataMember]
        public Pokemon Pokemon2
        {
            get { return pokemon2; }
            set { pokemon2 = value; }
        }

        [DataMember]
        public Stade StadePokemon
        {
            get { return stadepokemon; }
            set { stadepokemon = value; }
        }
        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
   }
}