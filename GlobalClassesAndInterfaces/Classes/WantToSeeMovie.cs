using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.Classes
{
    public class WantToSeeMovie
    {
        public int Id { get; set; }

        public  virtual Account Account { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
