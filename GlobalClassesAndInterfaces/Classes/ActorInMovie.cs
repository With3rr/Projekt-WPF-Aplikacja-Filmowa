using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.Classes
{
    public class ActorInMovie
    {
        public int ID { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Actor Actor { get; set; }

        public string Role { get; set; }


    }
}
