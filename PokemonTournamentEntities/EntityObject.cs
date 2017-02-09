using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public abstract class EntityObject
    {
        public int Id { get; set; }

        static private int ID = 0;

        public EntityObject()
        {
            Id = ID;
            ID++;
        }
        
    }
}
