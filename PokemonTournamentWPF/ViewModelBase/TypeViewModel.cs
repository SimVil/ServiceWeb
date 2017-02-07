using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PokemonTournamentWPF.ViewModelBase
{
    public class TypeViewModel : ViewModelBase
    {
        // Model encapsulé dans le ViewModel
        private PokemonTournamentEntities.TypeElement _type;

        public PokemonTournamentEntities.TypeElement Type
        {
            get { return _type; }
            private set { _type = value; }
        }

        public TypeViewModel(PokemonTournamentEntities.TypeElement typeModel)
        {
            _type = typeModel;
        }

        public override String ToString()
        {
            return Type.ToString();
        }

    }
}
